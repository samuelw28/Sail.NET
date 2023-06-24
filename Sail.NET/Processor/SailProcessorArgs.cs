namespace Sail.NET
{
    public class SailProcessorArgs
    {
        public string ApiKey { get; set; }
        public List<SailSupportedModels> Models { get; set; }

        public SailProcessorArgs()
        {
            ApiKey = "";
            Models = new();
        }
    }
}
