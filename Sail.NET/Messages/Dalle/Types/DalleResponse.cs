using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A response sent from the DALL-E endpoint
    /// </summary>
    internal class DalleResponse
    {
        [JsonPropertyName("data")]
        public DalleImage[] Images { get; set; }
    }
}
