namespace Sail.NET
{
    public static class SailModelTemplates
    {
        private static readonly Dictionary<SailModelTypes, SailModelArgs> _defaultModels = new()
        {
            {
                SailModelTypes.GPT3Point5,
                new()
                {
                    Name = "ChatGPT 3.5",
                    Model = "gpt-3.5-turbo",
                    Address = "https://api.openai.com/v1/chat/completions",
                    Tokens = 100,
                    Temperature = 0.5,
                    Handler = new GptHandler()
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
                    Handler = new DalleHandler()
                }
            }
        };

        public static Dictionary<SailModelTypes, SailModelArgs> DefaultModels = _defaultModels;
    }
}
