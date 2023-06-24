using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class DalleResponse
    {
        [JsonPropertyName("data")]
        public DalleImage[] Images { get; set; }
    }
}
