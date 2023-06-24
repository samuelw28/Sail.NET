using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class DalleImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
