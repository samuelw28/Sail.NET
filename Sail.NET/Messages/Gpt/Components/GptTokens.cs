
using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class GptTokens
    {
        [JsonPropertyName("prompt_tokens")]
        public int RequestTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int ResponseTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
