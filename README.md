# Sail.NET
Sail.NET is a .NET wrapper for OpenAI's API that aims to create a streamlined and intuive way of accessing the API.
## Setup
1. Create an API key at: https://platform.openai.com/
2. Install the Sail.NET NuGet package
## Implementation
```C#
// Create the processor object
SailProcessor processor = new();

// Create the arguments to pass in
SailProcessorArgs processorArgs = new()
  {
      ApiKey = "*** API KEY GOES HERE ***",
      Models = new()
      {
          SailSupportedModels.GPT3Point5,
          SailSupportedModels.DALLE
      }
  };

// Initialize the processor
processor.Initialize(processorArgs);

// Send a request
SailContext<SailMessage> response = await processor.SendRequestAsync("Hello World!", SailSupportedModels.DALLE, count: 1);

// Check if response is successful
if (response.Success)
{
    // Output response
    Console.WriteLine("Response: " + response.Result.Output);
}
```

## History
### Version 0.1.1
- First implemtation, added support for ChatGPT requests and responses
### Version 0.2.1
- Added support for multiple endpoints
- Refactored code to make using the library simpler
