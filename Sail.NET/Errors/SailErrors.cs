namespace Sail.NET
{
    public static class SailErrors
    {
        private const string _processorNotInitialized = "Processor has not been initialized, make sure the \".Initialize()\" function has been called.";

        private readonly static string _modelNotConfigured = $"Model \"{0}\" is not configured";

        public static string ProcessorNotInitialized()
        {
            return _processorNotInitialized;
        }

        public static string ModelNotConfigured(string model)
        {
            return string.Format(_modelNotConfigured, model);
        }
    }
}
