﻿using System.Text.Json;

namespace Sail.NET
{
    /// <summary>
    /// Handles messages being sent to the ChatGPT endpoint
    /// </summary>
    internal class GptHandler : SailApiHandler
    {
        private List<GptChatMessage> _messages { get; set; }

        public GptHandler()
        {
            _messages = new();
        }

        public override string CreateRequest(string input, SailModel model, int tokens, double temperature, int count)
        {
            _messages.Add(new()
            {
                Role = "user",
                Data = input
            });

            GptRequest request = new()
            {
                Messages = _messages,
                Tokens = tokens == 0 ? model.Tokens : tokens,
                Temperature = temperature == 0 ? model.Temperature : temperature,
                Model = model.Model
            };

            return JsonSerializer.Serialize(request);
        }

        public override string CreateResponse(string context)
        {
            GptResponse response = JsonSerializer.Deserialize<GptResponse>(context);

            return response.Choices[0].Message.Data;
        }

        public override void ClearHistory()
        {
            _messages.Clear();
        }
    }
}