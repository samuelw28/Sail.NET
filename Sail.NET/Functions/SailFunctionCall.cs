using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// Represents the function call that is returned by the api
    /// </summary>
    internal class SailFunctionCall
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }
    }
}
