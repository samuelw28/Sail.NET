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
                    Name = "GPT 3.5",
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
                    Name = "GPT 3.5 Snapshot",
                    Model = "gpt-3.5-turbo-16k-0613",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.GPT4,
                new()
                {
                    Name = "GPT 4",
                    Model = "gpt-4",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.GPT4o,
                new()
                {
                    Name = "GPT 4o",
                    Model = "gpt-4o",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.GPT4oMini,
                new()
                {
                    Name = "GPT 4o Mini",
                    Model = "gpt-4o-mini",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.GPT4Point1Nano,
                new()
                {
                    Name = "GPT 4.1 Nano",
                    Model = "gpt-4.1-nano",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
            {
                SailModelTypes.DALLE2,
                new()
                {
                    Name = "DALL-E 2",
                    Model = "dall-e-2",
                    Address = "https://api.openai.com/v1/images/generations",
                    Tokens = 100,
                    Temperature = 0.5,
                    Count = 1,
                    ConfigureFunctions = false
                }
            },
                        {
                SailModelTypes.DALLE3,
                new()
                {
                    Name = "DALL-E 3",
                    Model = "dall-e-3",
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
