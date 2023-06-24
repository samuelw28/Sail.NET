namespace Sail.NET
{
    /// <summary>
    /// A model used in the Sail.NET library
    /// </summary>
    public class SailModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Address { get; set; }
        public int Tokens { get; set; }
        public double Temperature { get; set; }
        public SailApiHandler Handler { get; set; }

        public SailModel(SailModelArgs args)
        {
            Name = args.Name;
            Model = args.Model;
            Address = args.Address;
            Tokens = args.Tokens;
            Temperature = args.Temperature;
            Handler = args.Handler;
        }
    }
}
