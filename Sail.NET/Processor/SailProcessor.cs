using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Sail.NET
{
    public class SailProcessor
    {
        private HttpClient _client { get; set; }

        private Dictionary<SailModelTypes, SailModel> _models { get; set; }

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

        private void ConfigureDefaultModel(SailModelTypes model)
        {
            _models[model] = new(SailModelTemplates.DefaultModels[model]);
        }

        public void ConfigureModel(SailModelTypes model, SailModelArgs args)
        {
            _models[model] = new(args);
        }

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
    }
}
