namespace Sail.NET
{
    /// <summary>
    /// The arguments used to create a processor
    /// </summary>
    public class SailProcessorArgs
    {
        public string ApiKey { get; set; }
        public Dictionary<SailModelTypes, List<string>> Models { get; set; }

        public SailProcessorArgs()
        {
            ApiKey = "";
            Models = new();
        }
    }
}
