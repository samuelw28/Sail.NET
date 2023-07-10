using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// Represents the parameters that are used to configure a function
    /// </summary>
    public class SailFunctionParameters
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("properties")]
        public object Properties { get; set; }

        [JsonPropertyName("required")]
        public List<string> RequiredParameters { get; set; }
    }
}
