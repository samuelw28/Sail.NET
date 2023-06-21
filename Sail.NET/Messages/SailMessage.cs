namespace Sail.NET
{
    public class SailMessage
    {
        public int ID { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string HTTPContext { get; set; }
        public bool Success { get; set; }

        public SailRequest Request { get; set; }
    }
}
