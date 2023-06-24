namespace Sail.NET
{
    /// <summary>
    /// Contains functionality to handle different types of API endpoints
    /// </summary>
    public abstract class SailApiHandler
    {
        /// <summary>
        /// Creates a request to be sent to the API
        /// </summary>
        /// <param name="input">The input text/prompt</param>
        /// <param name="model">The model that is being used</param>
        /// <param name="tokens">The number of tokens to be used, if needed</param>
        /// <param name="temperature">The temperature to be used, if needed</param>
        /// <param name="count">The number of responses to generate, if needed</param>
        /// <returns>A serialized version of the request</returns>
        public abstract string CreateRequest(string input, SailModel model, int tokens = 0, double temperature = 0, int count = 0);

        /// <summary>
        /// Creates a response from the API
        /// </summary>
        /// <param name="context">The response that has been recieved</param>
        /// <returns>The response text</returns>
        public abstract string CreateResponse(string context);

        /// <summary>
        /// Clears any stored messages, if the model has that functionality
        /// </summary>
        public virtual void ClearHistory()
        {
            throw new NotImplementedException();
        }
    }
}
