using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class DalleRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("n")]
        public int Count { get; set; }
    }
}
