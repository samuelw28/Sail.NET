namespace Sail.NET
{
    /// <summary>
    /// A message that is sent using the Sail.NET library
    /// </summary>
    public class SailMessage
    {
        public int ID { get; set; }
        public SailModelTypes Model { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
