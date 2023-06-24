using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A choice that is sent from the ChatGPT endpoint
    /// </summary>
    internal class GptChoice
    {
        [JsonPropertyName("message")]
        public GptChatMessage Message { get; set; }
    }
}
