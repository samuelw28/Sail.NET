namespace Sail.NET
{
    /// <summary>
    /// Creates a unique ID for each message sent
    /// </summary>
    public class SailEvent
    {
        private static int id = 0;

        public int ID { get; set; }

        public SailEvent()
        {
            ID = Interlocked.Increment(ref id);
        }
    }
}
