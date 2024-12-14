using System.Text.Json;
using ErrorOr;
using HackathonBackend.Application.Chat.Common;
using HackathonBackend.Application.Common.Interfaces.Services;
using MediatR;
using RestSharp;

namespace HackathonBackend.Application.Chat.Commands.SendMessage;

public class SendMessageCommandHandler  : IRequestHandler<SendMessageCommand, ErrorOr<string>>
{
    private readonly string _endpoint = "https://artem-m4nhbbfy-eastus2.cognitiveservices.azure.com/openai/deployments/gpt-35-turbo-16k/chat/completions?api-version=2024-08-01-preview";
    private readonly IChatKeyProvider _chatKeyProvider;
    private readonly RestClient _client;
    
    public SendMessageCommandHandler(IChatKeyProvider chatKeyProvider)
    {
        _chatKeyProvider = chatKeyProvider;
        var options = new RestClientOptions(_endpoint);
        _client = new RestClient(options);
    }

    public async Task<ErrorOr<string>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var apiRequest = new RestRequest();

        apiRequest.AddHeader("api-key", _chatKeyProvider.GetApiKey());
        apiRequest.AddHeader("Content-Type", "application/json");
        
        var requestBody = new
        {
            messages = new[]
            {
                new { role = "system", content = request.History },
                new { role = "user", content = request.Message }
            },
            max_tokens = 300,
            temperature = 0.7,
            top_p = 0.95
        };

        apiRequest.AddJsonBody(JsonSerializer.Serialize(requestBody));

        try
        {
            var response = await _client.PostAsync(apiRequest, cancellationToken);

            if (!response.IsSuccessful)
            {
                return Error.Failure($"Error from OpenAI: {response.StatusCode} - {response.Content}");
            }

            var responseData = JsonSerializer.Deserialize<ChatResponse>(response.Content!);

            return responseData?.Choices.FirstOrDefault()?.Message.Content ?? string.Empty;
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}