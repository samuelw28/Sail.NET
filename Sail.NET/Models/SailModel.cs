namespace Sail.NET
{
    public class SailModel
    {
        public SailSupportedModels Name { get; set; }
        public string Model { get; set; }
        public string Address { get; set; }
        public int Tokens { get; set; }
        public double Temperature { get; set; }

        //public IAIMessageProcessor Processor { get; set; }

        public SailModel(SailModelArgs args)
        {
            Model = args.Model;
            Address = args.Address;
        }
    }
}
