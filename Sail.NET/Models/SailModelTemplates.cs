namespace Sail.NET
{
    public static class SailModelTemplates
    {
        public static SailModel ChatGPT { get; set; } = new(
            new()
            {
                Model = "gpt-3.5-turbo",
                Address = "https://api.openai.com/v1/chat/completions"
            })
        {
            Name = SailSupportedModels.GPT3Point5,
            Tokens = 100,
            Temperature = 0.5
        };
    }
}
