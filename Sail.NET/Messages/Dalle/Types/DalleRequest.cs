using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A request sent to the DALL-E endpoint
    /// </summary>
    internal class DalleRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("n")]
        public int Count { get; set; }
    }
}
