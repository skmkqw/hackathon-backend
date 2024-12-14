using HackathonBackend.Application.Common;
using HackathonBackend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace HackathonBackend.Infrastructure.Services;

public class ChatKeyProvider : IChatKeyProvider
{
    private readonly ChatSettings _chatSettings;

    public ChatKeyProvider(IConfiguration _configuration)
    {
        var settings = _configuration.GetSection("ChatSettings");
        _chatSettings = new ChatSettings()
        {
            ApiKey = settings["API_KEY"]!
        };
    }

    public string GetApiKey()
    {
        return _chatSettings.ApiKey;
    }
}