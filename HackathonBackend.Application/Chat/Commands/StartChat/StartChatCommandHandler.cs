using System.Text.Json;
using ErrorOr;
using HackathonBackend.Application.Chat.Common;
using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Application.Common.Interfaces.Services;
using HackathonBackend.Domain.Common.Errors;
using MediatR;
using RestSharp;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public class StartChatCommandHandler : IRequestHandler<StartChatCommand, ErrorOr<string>>
{
    private readonly string _endpoint = "https://artem-m4nhbbfy-eastus2.cognitiveservices.azure.com/openai/deployments/gpt-35-turbo-16k/chat/completions?api-version=2024-08-01-preview";
    private readonly IChatKeyProvider _chatKeyProvider;
    private readonly RestClient _client;
    private readonly IUserRepository _userRepository;
    
    public StartChatCommandHandler(IChatKeyProvider chatKeyProvider, IUserRepository userRepository)
    {
        _chatKeyProvider = chatKeyProvider;
        _userRepository = userRepository;
        var options = new RestClientOptions(_endpoint);
        _client = new RestClient(options);
    }

    public async Task<ErrorOr<string>> Handle(StartChatCommand request, CancellationToken cancellationToken)
    {
        var apiRequest = new RestRequest();

        apiRequest.AddHeader("api-key", _chatKeyProvider.GetApiKey());
        apiRequest.AddHeader("Content-Type", "application/json");
        
        var requestBody = new
        {
            messages = new[]
            {
                new { role = "system", content = "You are a medical assistant designed to bridge the gap between the client and healthcare professionals. Your task is to gather information about the client's symptoms and concerns, recommend the most appropriate medical category based on their input, and help them view available doctors within that category. There are no real doctors i can tell you about for now, but you can imagine some. You should never recommend any medication! As soon as client is over with telling about his symptoms, your goal is to ask about his location and suggest some imaginary doctors." },
                new { role = "user", content = "I want you to greet me with something like 'Hello, how can i help you today?'" }
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