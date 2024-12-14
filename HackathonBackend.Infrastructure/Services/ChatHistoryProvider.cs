using HackathonBackend.Application.Common.Interfaces.Services;

namespace HackathonBackend.Infrastructure.Services;

public class ChatHistoryProvider : IChatHistoryProvider
{
    private const string DefaultValue = "This is the very beginning of our dialouge, so there's no need to mention any history. Just follow the instructions i gave you and follow your role";

    private string History { get; set; } = DefaultValue;

    public string GetChatHistory()
    {
        return History;
    }

    public void AppendInputMessage(string message)
    {
        History += $"Me: {message}";
    }

    public void AppendOutputMessage(string message)
    {
        History += $"You: {message}";
    }

    public void ClearHistory()
    {
        History = DefaultValue;
    }
}