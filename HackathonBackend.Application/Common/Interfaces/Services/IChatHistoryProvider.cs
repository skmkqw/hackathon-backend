namespace HackathonBackend.Application.Common.Interfaces.Services;

public interface IChatHistoryProvider
{
    string GetChatHistory();
    
    void AppendInputMessage(string message);
    
    void AppendOutputMessage(string message);
    
    void ClearHistory();
}