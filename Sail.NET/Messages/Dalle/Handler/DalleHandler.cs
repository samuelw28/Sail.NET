using System.Text.Json;

namespace Sail.NET
{
    /// <summary>
    /// Handles messages being sent to the DALL-E endpoint
    /// </summary>
    internal class DalleHandler : SailApiHandler
    {
        public override string CreateRequest(string input, SailModel model, int tokens, double temperature, int count)
        {
            DalleRequest request = new()
            {
                Prompt = input,
                Count = count == 0 ? model.Count : count,
            };

            return JsonSerializer.Serialize(request);
        }

        public override string CreateResponse(string context)
        {
            DalleResponse response = JsonSerializer.Deserialize<DalleResponse>(context);

            return response.Images[0].Url;
        }
    }
}
