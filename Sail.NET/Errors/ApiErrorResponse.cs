using System.Text.Json.Serialization;

namespace Sail.NET
{
    internal class ApiErrorResponse
    {
        [JsonPropertyName("error")]
        public ApiError Error { get; set; }
    }
}
