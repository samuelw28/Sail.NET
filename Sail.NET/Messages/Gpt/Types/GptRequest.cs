using System.Text.Json.Serialization;

namespace Sail.NET
{    /// <summary>
     /// A request sent to the ChatGPT endpoint
     /// </summary>
    internal class GptRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("messages")]
        public List<GptChatMessage> Messages { get; set; }

        [JsonPropertyName("max_tokens")]
        public int Tokens { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
    }
}
