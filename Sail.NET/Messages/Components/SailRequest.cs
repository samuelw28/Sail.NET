using System.Text.Json.Serialization;

namespace Sail.NET
{
    public class SailRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("messages")]
        public List<SailChatMessage> Messages { get; set; }

        [JsonPropertyName("max_tokens")]
        public int Tokens { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
    }
}
