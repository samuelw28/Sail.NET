using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class GptResponse
    {
        [JsonPropertyName("choices")]
        public GptChoice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public GptTokens TokenInfo { get; set; }
    }
}
