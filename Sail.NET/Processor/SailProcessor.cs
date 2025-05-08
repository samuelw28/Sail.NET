using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Sail.NET
{
    /// <summary>
    /// The main processing class, where requests and responses are handled
    /// </summary>
    public class SailProcessor
    {
        private HttpClient _client { get; set; }

        private Dictionary<SailModelTypes, SailApiHandler> _handlers { get; set; }

        private Dictionary<SailModelTypes, Dictionary<string, SailModel>> _models { get; set; }

        private Dictionary<int, SailMessage> _messages { get; set; }

        /// <summary>
        /// Initializes the processor and creates the required objects for it to function
        /// </summary>
        /// <param name="args">The arguments being used to initialize the processor</param>
        public void Initialize(SailProcessorArgs args)
        {
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", args.ApiKey);

            _handlers = new()
            {
                {
                    SailModelTypes.GPT3Point5,
                    new GptHandler()
                },
                {
                    SailModelTypes.GPT3Point5Snapshot,
                    new GptHandler()
                },
                {
                    SailModelTypes.GPT4,
                    new GptHandler()
                },
                {
                    SailModelTypes.GPT4o,
                    new GptHandler()
                },
                {
                    SailModelTypes.GPT4oMini,
                    new GptHandler()
                },
                {
                    SailModelTypes.GPT4Point1Nano,
                    new GptHandler()
                },
                {
                    SailModelTypes.DALLE2,
                    new DalleHandler()
                },
                {
                    SailModelTypes.DALLE3,
                    new DalleHandler()
                }
            };

            _models = new();

            _messages = new();

            foreach (var model in args.Models)
            {
                foreach (var conf in model.Value)
                {
                    ConfigureModel(model.Key, conf, SailModelTemplates.DefaultModels[model.Key]);
                }
            }
        }

        /// <summary>
        /// Configures a model using custom arguments
        /// </summary>
        /// <param name="model">The model being configured</param>
        /// <param name="args">The arguments being used to configure the model</param>
        public void ConfigureModel(SailModelTypes model, string name, SailModelArgs args)
        {
            if (!_models.ContainsKey(model))
            {
                _models[model] = new Dictionary<string, SailModel>();
            }

            _models[model][name] = new(args, _handlers[model]);

            if (args.ConfigureFunctions)
            {
                _handlers[model].ConfigureFunctions(args.FunctionsLocation, args.Functions);
            }
        }

        /// <summary>
        /// Updates an already configured model
        /// </summary>
        /// <param name="model">The model being configured</param>
        /// <param name="tokens">The new tokens value</param>
        /// <param name="temperature">The new temperature value</param>
        /// <param name="count">The new count value</param>
        public void ReconfigureModel(SailModelTypes model, string name, int tokens = 0, double temperature = 0, int count = 0)
        {
            if (_models.TryGetValue(model, out var sailModel))
            {
                if (sailModel.TryGetValue(name, out var confSailModel))
                {
                    if (tokens > 0)
                    {
                        confSailModel.Tokens = tokens;
                    }

                    if (temperature > 0)
                    {
                        confSailModel.Temperature = temperature;
                    }

                    if (count > 0)
                    {
                        confSailModel.Count = count;
                    }
                }
            }
        }

        /// <summary>
        /// Clears the model's message history
        /// </summary>
        /// <param name="model">The model being cleared</param>
        /// <param name="clearSystemHistory">Whether to clear system messages as well</param>
        public void ClearModelHistory(SailModelTypes model, string name, bool clearSystemHistory = false)
        {
            _models[model][name].Handler.ClearHistory(clearSystemHistory);
        }

        /// <summary>
        /// Adds a system message to the model
        /// </summary>
        /// <param name="model">The model the message is being added to</param>
        /// <param name="message">The message to be added</param>
        public bool AddSystemMessage(SailModelTypes model, string name, string message)
        {
            return _models[model][name].Handler.AddSystemMessage(message);
        }

        /// <summary>
        /// Removes a system message from the model
        /// </summary>
        /// <param name="model">The model the message is being removed from</param>
        /// <param name="message">The message to be removed</param>
        public bool RemoveSystemMessage(SailModelTypes model, string name, string message)
        {
            return _models[model][name].Handler.RemoveSystemMessage(message);
        }

        /// <summary>
        /// Configures function handling for a model
        /// </summary>
        /// <param name="model">The model being configured</param>
        /// <param name="location">The location of the functions</param>
        /// <param name="functions">The functions being configured</param>
        public void ConfigureModelFunctions(SailModelTypes model, Type location, Dictionary<string, SailFunction> functions)
        {
            _handlers[model].ConfigureFunctions(location, functions);
        }

        public List<SailMessage> GetModelMessages(SailModelTypes model, string name)
        {
            List<SailMessage> messages = new();

            foreach (var msg in _messages)
            {
                if (msg.Value.Model == model && msg.Value.Source == name)
                {
                    messages.Add(msg.Value);
                }
            }

            return messages;
        }

        /// <summary>
        /// Sends a request asynchronously
        /// </summary>
        /// <param name="prompt">The input prompt</param>
        /// <param name="model">The model being used</param>
        /// <param name="tokens">The tokens value for the message being sent</param>
        /// <param name="temperature">TThe temperature value for the message being sent</param>
        /// <param name="count">The count value for the message being sent</param>
        /// <param name="isFunctionResponse">Whether the message is being created by function call</param>
        /// <returns>The context for the message that has been sent</returns>
        public async Task<SailContext<SailMessage>> SendRequestAsync(string prompt, SailModelTypes model, string name, int tokens = 0, double temperature = 0, int count = 1, bool isFunctionResponse = false)
        {
            SailMessage message = new()
            {
                ID = new SailEvent().ID,
                Model = model,
                Source = name,
                Input = new()
                {
                    Prompt = prompt
                }
            };

            if (_models == null)
            {
                return new SailContext<SailMessage>(message, false, SailErrors.ProcessorNotInitialized());
            }

            if (!_models.TryGetValue(model, out Dictionary<string, SailModel> sailModel))
            {
                return new SailContext<SailMessage>(message, false, SailErrors.ModelNotConfigured(model.ToString()));
            }

            try
            {
                string json = sailModel[name].Handler.CreateRequest(prompt, sailModel[name], tokens, temperature, count, isFunctionResponse);

                StringContent content = new(
                    json,
                    Encoding.UTF8,
                    "application/json"
                    );

                HttpResponseMessage httpResponse = await _client.PostAsync(
                    sailModel[name].Address,
                    content
                    );

                return await BuildResponse(name, message, await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new SailContext<SailMessage>(message, false, ex.InnerException != null ? ex.InnerException.ToString() : ex.Message);
            }
        }

        /// <summary>
        /// Builds a response
        /// </summary>
        /// <param name="message">The message that the response is related to</param>
        /// <param name="context">The response text</param>
        /// <returns>The context for the message that has been sent</returns>
        private async Task<SailContext<SailMessage>> BuildResponse(string name, SailMessage message, string context)
        {
            try
            {
                message.Output = _models[message.Model][name].Handler.GetMessageOutput(context);

                if (string.IsNullOrEmpty(message.Output.Text))
                {
                    SailContext<SailMessage> functionMessage = await SendRequestAsync(string.Empty, SailModelTypes.GPT3Point5Snapshot, name, isFunctionResponse: true);

                    message.Output = functionMessage.Result.Output;
                }

                _messages[message.ID] = message;

                return new SailContext<SailMessage>(message, true);
            }
            catch
            {
                ApiErrorResponse response = JsonSerializer.Deserialize<ApiErrorResponse>(context);

                return new SailContext<SailMessage>(message, false, response.Error.Message);
            }
        }
    }
}
