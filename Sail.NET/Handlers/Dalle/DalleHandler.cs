using System.Text.Json;

namespace Sail.NET
{
    /// <summary>
    /// Handles messages being sent to the DALL-E endpoint
    /// </summary>
    internal class DalleHandler : SailApiHandler
    {
        public override string CreateRequest(string input, SailModel model, int tokens, double temperature, int count, bool isFunctionResponse)
        {
            DalleRequest request = new()
            {
                Prompt = input,
                Count = count == 0 ? model.Count : count,
            };

            return JsonSerializer.Serialize(request);
        }

        public override SailMessageOutput GetMessageOutput(string context)
        {
            DalleResponse response = JsonSerializer.Deserialize<DalleResponse>(context);

            if (response.Images != null)
            {
                return new()
                {
                    Text = response.Images[0].Url
                };
            }

            throw new Exception();
        }

        public override void ConfigureFunctions(Type location, Dictionary<string, SailFunction> functions)
        {
            throw new NotImplementedException(SailErrors.FunctionalityNotSupported("DALL-E"));
        }

        public override string LocateFunctions(string name, string args)
        {
            throw new NotImplementedException(SailErrors.FunctionalityNotSupported("DALL-E"));
        }

        public override bool AddSystemMessage(string message)
        {
            throw new NotImplementedException(SailErrors.FunctionalityNotSupported("DALL-E"));
        }

        public override bool RemoveSystemMessage(string message)
        {
            throw new NotImplementedException(SailErrors.FunctionalityNotSupported("DALL-E"));
        }

        public override void ClearHistory(bool clearSystemHistory)
        {
            throw new NotImplementedException(SailErrors.FunctionalityNotSupported("DALL-E"));
        }
    }
}
