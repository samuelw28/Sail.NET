using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class GptChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Data { get; set; }
    }
}
