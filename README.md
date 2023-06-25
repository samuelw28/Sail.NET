# Sail.NET
Sail.NET is a .NET wrapper for OpenAI's API that aims to create a streamlined and intuive way of accessing the API.
## Setup
1. Create an API key at: https://platform.openai.com/
2. Install the Sail.NET NuGet package here: https://www.nuget.org/packages/Sail.NET
## Implementation
```C#
// Create the processor object
var processor = new SailProcessor();

// Create the arguments to pass in
var processorArgs = new SailProcessorArgs()
  {
      ApiKey = "*** API KEY GOES HERE ***",
      Models = new()
      {
          SailModelTypes.GPT3Point5
      }
  };

// Initialize the processor
processor.Initialize(processorArgs);

// Create additional model arguments
var dalleArgs = new SailModelArgs()
{
    Name = "DALL-E",
    Count = 1
};

// Configure model with custom settings
processor.ConfigureModel(SailModelTypes.DALLE, dalleArgs);

// Reconfigure model with updated settings
processor.ReconfigureModel(SailModelTypes.GPT3Point5, temperature: 0.1);

// Send a request
SailContext<SailMessage> response = await processor.SendRequestAsync("Hello World!", SailModelTypes.DALLE, count: 1);

// Check if response is successful
if (response.Success)
{
    // Output response
    Console.WriteLine("Response: " + response.Result.Output);
}
```

## History
### Version 0.2
#### Version 0.2.3
- Added comments to each class
- Removed "GPT4" from "SailModeTypes"
#### Version 0.2.2
- Renamed "SailModelType" enum
#### Version 0.2.1
- Refactored code to allow for multiple endpoints
- Added support for DALL-E image generation
- Fixed issue where ChatGPT messages weren't being stored
- Simplified implementation process
### Version 0.1
#### Version 0.1.1
- Created basic request and response classes
- Added support for ChatGPT messaging
