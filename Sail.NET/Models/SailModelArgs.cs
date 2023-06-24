namespace Sail.NET
{
    public class SailModelArgs
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Address { get; set; }
        public int Tokens { get; set; }
        public double Temperature { get; set; }
        public SailApiHandler Handler { get; set; }
    }
}
