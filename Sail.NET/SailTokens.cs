using System.Runtime.Serialization;

namespace Sail.NET
{
    [DataContract]
    public class SailTokens
    {
        [DataMember(Name = "prompt_tokens")]
        public int RequestTokens { get; set; }

        [DataMember(Name = "completion_tokens")]
        public int ResponseTokens { get; set; }

        [DataMember(Name = "total_tokens")]
        public int TotalTokens { get; set; }
    }
}
