using System.Text.Json.Serialization;

namespace Sail.NET
{
    /// <summary>
    /// The response that is recieved when an error occurs during a request
    /// </summary>
    internal class ApiErrorResponse
    {
        [JsonPropertyName("error")]
        public ApiError Error { get; set; }
    }
}
