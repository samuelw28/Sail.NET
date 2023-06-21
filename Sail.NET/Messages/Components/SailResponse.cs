using System.Text.Json.Serialization;

namespace Sail.NET
{
    public class SailResponse
    {
        [JsonPropertyName("choices")]
        public SailChoice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public SailTokens TokenInfo { get; set; }
    }
}
