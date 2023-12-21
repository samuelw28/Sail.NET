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
        /// <param name="isFunctionResponse">Whether the message is being created by function call</param>
        /// <returns>A serialized version of the request</returns>
        public abstract string CreateRequest(string input, SailModel model, int tokens = 0, double temperature = 0, int count = 0, bool isFunctionResponse = false);

        /// <summary>
        /// Creates a response from the API
        /// </summary>
        /// <param name="context">The response that has been recieved</param>
        /// <returns>The response text</returns>
        public abstract SailMessageOutput GetMessageOutput(string context);

        /// <summary>
        /// Configure functions for a model
        /// </summary>
        /// <param name="location">The class the functions are located in/param>
        /// <param name="functions">The functions being called</param>
        public abstract void ConfigureFunctions(Type location, Dictionary<string, SailFunction> functions);

        /// <summary>
        /// Locate the functions for a model
        /// </summary>
        /// <param name="name">The name of the specified functiont</param>
        /// <param name="args">The arguments to be passed in to the functiond</param>
        public abstract string LocateFunctions(string name, string args);

        /// <summary>
        /// Clears any stored messages, if the model has that functionality
        /// </summary>
        /// <param name="clearSystemHistory">Whether to clear configured system messages</param>
        public abstract void ClearHistory(bool clearSystemHistory);

        /// <summary>
        /// Adds a system message to a model, if the model has that functionality
        /// </summary>
        /// <param name="message">The message to be added</param>
        public abstract bool AddSystemMessage(string message);

        /// <summary>
        /// Removes a system message from a model, if the model has that functionality
        /// </summary>
        /// <param name="message">The message to be removed</param>
        public abstract bool RemoveSystemMessage(string message);
    }
}
