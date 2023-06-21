using System.Text.Json.Serialization;

namespace Sail.NET
{
    public class SailChoice
    {
        [JsonPropertyName("message")]
        public SailChatMessage Message { get; set; }
    }
}
