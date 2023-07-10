using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// A property defined in a function
    /// </summary>
    public class SailFunctionProperty
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
