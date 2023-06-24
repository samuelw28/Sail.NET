using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class ApiError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("param")]
        public string Parameter { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
