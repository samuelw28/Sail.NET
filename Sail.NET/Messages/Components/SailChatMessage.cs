using System.Text.Json.Serialization;

namespace Sail.NET
{
    public class SailChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Data { get; set; }
    }
}
