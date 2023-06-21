namespace Sail.NET
{
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
