using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A chat message that is sent from the ChatGPT endpoint
    /// </summary>
    internal class GptChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Data { get; set; }
    }
}
