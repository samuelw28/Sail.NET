using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class GptChoice
    {
        [JsonPropertyName("message")]
        public GptChatMessage Message { get; set; }
    }
}
