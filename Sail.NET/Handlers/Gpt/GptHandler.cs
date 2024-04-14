using System.Reflection;
using System.Text.Json;

namespace Sail.NET
{
    /// <summary>
    /// Handles messages being sent to the ChatGPT endpoint
    /// </summary>
    internal class GptHandler : SailApiHandler
    {
        private List<GptChatMessage> _messages { get; set; }
        private Dictionary<string, SailFunction> _functions { get; set; }
        private Type _functionLocation { get; set; }

        private int _usedTokens { get; set; }

        public GptHandler()
        {
            _messages = new();
            _functions = new();

            _usedTokens = 0;
        }

        public override string CreateRequest(string input, SailModel model, int tokens, double temperature, int count, bool isFunctionResponse)
        {
            if (!isFunctionResponse)
            {
                _messages.Add(new()
                {
                    Data = input,
                    Role = "user"
                });
            }

            GptRequest request = new()
            {
                Model = model.Model,
                Messages = _messages,
                Tokens = tokens == 0
                        ? model.Tokens
                        : tokens,
                Temperature = temperature == 0
                        ? model.Temperature 
                        : temperature,
                Functions = _functions.Count != 0 
                        ? _functions.Values.ToList() 
                        : null
            };

            return JsonSerializer.Serialize(request);
        }

        public override SailMessageOutput GetMessageOutput(string context)
        {
            GptResponse response = JsonSerializer.Deserialize<GptResponse>(context);

            if (response.Choices != null)
            {
                _messages.Add(response.Choices[0].Message);

                _usedTokens += response.TokenInfo.TotalTokens;

                if (response.Choices[0].Message.FunctionCall != null)
                {
                    SailFunctionCall functionResponse = JsonSerializer.Deserialize<SailFunctionCall>(response.Choices[0].Message.FunctionCall.ToString());

                    string functionOutput = LocateFunctions(functionResponse.Name, functionResponse.Arguments);

                    _messages.Add(new()
                    {
                        Data = functionOutput,
                        Role = "function",
                        FunctionName = functionResponse.Name
                    });
                }

                return new()
                {
                    Text = response.Choices[0].Message.Data,
                    Tokens = response.TokenInfo.TotalTokens
                };
            }

            throw new Exception();
        }

        public override void ConfigureFunctions(Type location, Dictionary<string, SailFunction> functions)
        {
            _functionLocation = location;

            _functions = functions;
        }

        public override string LocateFunctions(string name, string args)
        {
            try
            {
                MethodInfo commandMethod = _functionLocation.GetMethod(name);

                var functionResponse = commandMethod.Invoke(_functionLocation, new[] { args });

                if (functionResponse != null)
                {
                    return functionResponse.ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.ToString() : ex.Message;
            }
        }

        public override bool AddSystemMessage(string message)
        {
            try
            {
                _messages.Add(new()
                {
                    Data = message,
                    Role = "system"
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool RemoveSystemMessage(string message)
        {
            try
            {
                var msg = _messages.Where(m => m.Role == "system").ToList().Find(m => m.Data == message);

                if (msg != null)
                {
                    _messages.Remove(msg);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public override void ClearHistory(bool clearSystemHistory)
        {
            _usedTokens = 0;

            if (clearSystemHistory)
            {
                _messages.Clear();
            }
            else
            {
                _messages.RemoveAll(m => m.Role != "system");
            }
        }
    }
}
