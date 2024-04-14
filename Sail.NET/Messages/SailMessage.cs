namespace Sail.NET
{
    /// <summary>
    /// A message that is sent using the Sail.NET library
    /// </summary>
    public class SailMessage
    {
        public int ID { get; set; }
        public SailModelTypes Model { get; set; }
        public string Source { get; set; }
        public SailMessageInput Input { get; set; }
        public SailMessageOutput Output { get; set; }
    }
}
