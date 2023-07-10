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

        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string FunctionName { get; set; }

        [JsonPropertyName("function_call")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object FunctionCall { get; set; }
    }
}
