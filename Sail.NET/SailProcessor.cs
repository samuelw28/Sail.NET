using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Sail.NET
{
    public class SailProcessor
    {
        private HttpClient _client { get; set; }

        private Dictionary<SailSupportedModels, SailModel> _models { get; set; }

        public void Initialize(SailProcessorArgs args)
        {
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", args.ApiKey);

            _models = new();
        }

        public void ConfigureModel(SailSupportedModels model, int defaultTokens = 0, int defaultTemperature = 0)
        {
            _models[model] = SailModelTemplates.ChatGPT;

            if (defaultTokens > 0) { _models[model].Tokens = defaultTokens; }
            if (defaultTemperature > 0) { _models[model].Temperature = defaultTemperature; }
        }

        public SailData<SailMessage> CreateRequest(string input, SailSupportedModels model)
        {
            if (_models.TryGetValue(model, out SailModel sailModel))
            {
                SailMessage message = new()
                {
                    ID = new SailEvent().ID,
                    Input = input,
                    Request = new()
                    {
                        Messages = new()
                    {
                        new()
                        {
                            Role = "user",
                            Data = input
                        }
                    },
                        Tokens = sailModel.Tokens,
                        Temperature = sailModel.Temperature,
                        Model = sailModel.Model
                    }
                };

                return new SailData<SailMessage>(message, true);
            }
            else
            {
                return new SailData<SailMessage>(new(), false, $"Model \"{model}\" is not configured");
            }
        }

        public SailData<string> CreateResponse(string context)
        {
            try
            {
                SailResponse response = JsonSerializer.Deserialize<SailResponse>(context);

                return new SailData<string>(response.Choices[0].Message.Data, true);
            }
            catch (Exception ex)
            {
                return new SailData<string>(string.Empty, false, ex.Message);
            }
        }

        public async Task<SailData<string>> SendRequestAsync(string json, SailSupportedModels model)
        {
            if (_models.TryGetValue(model, out SailModel sailModel))
            {
                try
                {
                    StringContent content = new(
                        json,
                        Encoding.UTF8,
                        "application/json"
                        );

                    HttpResponseMessage httpResponse = await _client.PostAsync(
                        sailModel.Address,
                        content
                        );

                    return new SailData<string>(await httpResponse.Content.ReadAsStringAsync(), true);
                }
                catch (Exception ex)
                {
                    return new SailData<string>(string.Empty, false, ex.Message);
                }

            }

            return new SailData<string>(string.Empty, false, $"Model \"{model}\" is not configured");
        }
    }
}
