using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// Represents a function that can be called by the api
    /// </summary>
    public class SailFunction
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }
    }
}
