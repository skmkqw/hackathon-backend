using System.Text;
using System.Text.Json;
using ErrorOr;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public class StartChatCommandHandler : IRequestHandler<StartChatCommand, ErrorOr<string>>
{
    private readonly string _endpoint = "https://artem-m4nhbbfy-eastus2.cognitiveservices.azure.com/openai/deployments/gpt-35-turbo-16k/chat/completions?api-version=2024-08-01-preview";
    private readonly string _apiKey = "4LkliS39TdSLMzpOZczsSM6F0ZNV30x5iJBd2XwdFEc2jSC8HLQgJQQJ99ALACHYHv6XJ3w3AAAAACOGassM";
    
    public StartChatCommandHandler()
    {
    }

    public async Task<ErrorOr<string>> Handle(StartChatCommand request, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Add("api-key", _apiKey);

        var requestBody = new
        {
            messages = new[]
            {
                new { role = "system", content = "You are an AI assistant designed to provide helpful and detailed responses to user queries." },
                new { role = "user", content = request.Message }
            },
            max_tokens = 1000,
            temperature = 0.7,
            top_p = 0.95
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(_endpoint, content, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            return responseString;
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}