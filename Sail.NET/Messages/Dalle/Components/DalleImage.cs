using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// An image that is sent from the DALL-E endpoint
    /// </summary>
    internal class DalleImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
