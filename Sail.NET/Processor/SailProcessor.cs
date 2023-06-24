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

        private Dictionary<SailModelTypes, SailModel> _models { get; set; }

        /// <summary>
        /// Initializes the processor and creates the required objects for it to function
        /// </summary>
        /// <param name="args">The arguments being used to initialize the processor</param>
        public void Initialize(SailProcessorArgs args)
        {
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", args.ApiKey);

            _models = new();

            foreach (SailModelTypes model in args.Models)
            {
                ConfigureDefaultModel(model);
            }
        }

        /// <summary>
        /// Configures a model using the default parameters
        /// </summary>
        /// <param name="model">The model being configured</param>
        private void ConfigureDefaultModel(SailModelTypes model)
        {
            _models[model] = new(SailModelTemplates.DefaultModels[model]);
        }

        /// <summary>
        /// Configures a model using custom arguments
        /// </summary>
        /// <param name="model">The model being configured</param>
        /// <param name="args">The arguments being used to configure the model</param>
        public void ConfigureModel(SailModelTypes model, SailModelArgs args)
        {
            _models[model] = new(args);
        }

        /// <summary>
        /// Updates an already configured model
        /// </summary>
        /// <param name="model">The model being configured</param>
        /// <param name="tokens">The new tokens value</param>
        /// <param name="temperature">The new temperature value</param>
        public void ReconfigureModel(SailModelTypes model, int tokens = 0, double temperature = 0)
        {
            if (_models.TryGetValue(model, out var sailModel))
            {
                if (tokens > 0)
                {
                    sailModel.Tokens = tokens;
                }

                if (temperature > 0)
                {
                    sailModel.Temperature = temperature;
                }
            }
        }

        /// <summary>
        /// Sends a request asynchronously
        /// </summary>
        /// <param name="input">The input text/prompt</param>
        /// <param name="model">The model being used</param>
        /// <param name="tokens">The tokens value for the message being sent</param>
        /// <param name="temperature">TThe temperature value for the message being sent</param>
        /// <param name="count">The count value for the message being sent</param>
        /// <returns></returns>
        public async Task<SailContext<SailMessage>> SendRequestAsync(string input, SailModelTypes model, int tokens = 0, double temperature = 0, int count = 1)
        {
            SailMessage message = new()
            {
                ID = new SailEvent().ID,
                Model = model,
                Input = input
            };

            if (_models == null)
            {
                return new SailContext<SailMessage>(message, false, SailErrors.ProcessorNotInitialized());
            }

            if (!_models.TryGetValue(model, out SailModel sailModel))
            {
                return new SailContext<SailMessage>(message, false, SailErrors.ModelNotConfigured(model.ToString()));
            }

            try
            {
                string json = sailModel.Handler.CreateRequest(input, sailModel, tokens, temperature, count);

                StringContent content = new(
                    json,
                    Encoding.UTF8,
                    "application/json"
                    );

                HttpResponseMessage httpResponse = await _client.PostAsync(
                    sailModel.Address,
                    content
                    );

                return BuildResponse(message, await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new SailContext<SailMessage>(message, false, ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Builds a response
        /// </summary>
        /// <param name="message">The message that the response is related to</param>
        /// <param name="context">The response text</param>
        /// <returns>The context for the message that has been sent</returns>
        private SailContext<SailMessage> BuildResponse(SailMessage message, string context)
        {
            try
            {
                message.Output = _models[message.Model].Handler.CreateResponse(context);

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
