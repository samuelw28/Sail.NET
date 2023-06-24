using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A response sent from the ChatGPT endpoint
    /// </summary>
    internal class GptResponse
    {
        [JsonPropertyName("choices")]
        public GptChoice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public GptTokens TokenInfo { get; set; }
    }
}
