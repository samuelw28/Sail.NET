namespace Sail.NET
{
    /// <summary>
    /// Errors that can occur when implementing the Sail.NET library
    /// </summary>
    public static class SailErrors
    {
        private const string _processorNotInitialized = "Processor has not been initialized, make sure the \".Initialize()\" function has been called.";

        private readonly static string _modelNotConfigured = $"Model \"{0}\" is not configured";

        /// <summary>
        /// Returns a "ProcessorNotInitialized" error
        /// </summary>
        /// <returns>The error message</returns>
        public static string ProcessorNotInitialized()
        {
            return _processorNotInitialized;
        }

        /// <summary>
        /// Returns a "ModelNotConfigured" error
        /// </summary>
        /// <param name="model">The model that has not been configured</param>
        /// <returns>The error message</returns>
        public static string ModelNotConfigured(string model)
        {
            return string.Format(_modelNotConfigured, model);
        }
    }
}
