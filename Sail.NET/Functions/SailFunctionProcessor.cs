namespace Sail.NET
{
    public static class SailFunctionProcessor
    {
        private const string _functionType = "object";

        /// <summary>
        /// Builds a function to be used by the api
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="description">A description of what the function does</param>
        /// <param name="properties">The properties the function has</param>
        /// <param name="requiredParameters">Which properties are required</param>
        /// <returns>The build function</returns>
        public static SailFunction BuildFunction(string name, string description, object properties, List<string> requiredParameters)
        {          
            return new SailFunction()
            {
                Name = name,
                Description = description,
                Parameters = new SailFunctionParameters()
                {
                    Type = _functionType,
                    Properties = properties,
                    RequiredParameters = requiredParameters
                }
            };
        }
    }
}
