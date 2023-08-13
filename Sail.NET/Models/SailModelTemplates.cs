namespace Sail.NET
{
    /// <summary>
    /// Default templates that can be used in place of manually configuring each model
    /// </summary>
    public static class SailModelTemplates
    {
        private static readonly Dictionary<SailModelTypes, SailModelArgs> _defaultModels = new()
        {
            {
                SailModelTypes.GPT3Point5,
                new()
                {
                    Name = "ChatGPT 3.5",
                    Model = "gpt-3.5-turbo-16k",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.GPT3Point5Snapshot,
                new()
                {
                    Name = "ChatGPT 3.5 Snapshot",
                    Model = "gpt-3.5-turbo-16k-0613",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },            {
                SailModelTypes.GPT4,
                new()
                {
                    Name = "ChatGPT 4",
                    Model = "gpt-4",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.DALLE,
                new()
                {
                    Name = "DALL-E",
                    Model = "",
                    Address = "https://api.openai.com/v1/images/generations",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            }
        };

        public static Dictionary<SailModelTypes, SailModelArgs> DefaultModels = _defaultModels;
    }
}
