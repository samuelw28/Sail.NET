# Sail.NET
Sail.NET is a .NET wrapper for OpenAI's API that aims to create a streamlined and intuive way of accessing the API.
## Setup
1. Create an API key at: https://platform.openai.com/
2. Install the Sail.NET NuGet package here: https://www.nuget.org/packages/Sail.NET
## Basic Implementation
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

// Send a request
SailContext<SailMessage> response = await processor.SendRequestAsync(
    "Hello World!",
    SailModelTypes.GPT3Point5,
    count: 1
    );

// Check if response is successful
if (response.Success)
{
    // Output response
    Console.WriteLine("Response: " + response.Result.Output.Text);
}
else
{
    // Output error
    Console.WriteLine("Error: " + response.Exception);
}
```
## Additional Functionality
```C#
// Create additional model arguments
var dalleArgs = new SailModelArgs()
{
    Name = "DALL-E",
    Address = "https://api.openai.com/v1/images/generations",
    Count = 1
};

// Configure model with custom settings
processor.ConfigureModel(SailModelTypes.DALLE, dalleArgs);

// Reconfigure model with updated settings
processor.ReconfigureModel(SailModelTypes.GPT3Point5, temperature: 0.1);

// Clears message history for a model
processor.ClearModelHistory(SailModelTypes.GPT3Point5);

// Set a system message
processor.AddSystemMessage(SailModelTypes.GPT3Point5, "Respond in the style of Yoda");
```
## Function Calling
In order to use function calling, the used model must be the 'GPT3Point5Snapshot'. The functions you wish the API to recognise need to be added to the model manually. The follwoing parameters can be added to the 'SailModelArgs' object when configuring a model, or called directly using the 'ConfigureModelFunctions' method.
- 'ConfigureFunctions': Whether function calling should be used *'SailModelArgs' only
- 'FunctionsLocation': The class that all of the functions are located in
- 'Functions': A dictionary of 'SailFunction' objects that contain the data for the configured functions
```C#
public class FunctionsLocation
{
    public static void ExampleFunction(string example)
    {
        // Functionality goes here
    }
}

public class ConfigureFunctions
{
    public void ConfigureExample(SailProcessor processor)
    {
        // Create location object
        var location = new FunctionsLocation();

        // Configure model functions
        processor.ConfigureModelFunctions(SailModelTypes.GPT3Point5Snapshot, location.GetType(), GetFunctions());
    }

    public Dictionary<string, SailFunction> GetFunctions()
    {
        var functions = new Dictionary<string, SailFunction>
        {
            {
                "ExampleFunction",
                SailFunctionProcessor.BuildFunction(
                    "ExampleFunction",
                    "An example function",
                    new
                    {
                        Example = new SailFunctionProperty()
                        {
                            Type = "string",
                            Description = "An example variable"
                        }
                    },
                    new List<string>()
                    {    
                        "example"
                    }
                )
            }
        };
        return functions;
    }
}
```
## History
## Version 0.4
### Version 0.4.2
- Added GPT4 support
### Version 0.4.1
- Added support for function calling
- Added functionality for setting system messages
- Added 'GPT3Point5Snapshot' model support
- Fixed issue where assistant messages weren't being stored  
### Version 0.3
#### Version 0.3.1
- Added new message input and output objects
- Added 'ClearModelHistory' function
- Added additional error handling
### Version 0.2
#### Version 0.2.4
- Added missing 'Count' variable to 'SailModelArgs'
- Added '_handlers' dictionary for assigning a handler for each model
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
