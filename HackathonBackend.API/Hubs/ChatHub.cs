using HackathonBackend.Application.Chat.Commands.SendMessage;
using HackathonBackend.Application.Chat.Commands.StartChat;
using HackathonBackend.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HackathonBackend.API.Hubs;

public interface IChatClient
{
    Task StartChat();
    Task ReceiveMessage(string message);
    
    Task SendMessage(string message);
}

public class ChatHub : Hub<IChatClient>
{
    private readonly IMediator _mediator;

    private readonly IChatHistoryProvider _historyProvider;
    public ChatHub(IMediator mediator, IChatHistoryProvider historyProvider)
    {
        _mediator = mediator;
        _historyProvider = historyProvider;
    }

    public async Task StartChat()
    {
        var command = new StartChatCommand();
    
        var startChatResult = await _mediator.Send(command);
        
        if (startChatResult.IsError)
        {
            await Clients.Caller.ReceiveMessage("Something went wrong");
            
            return;
        }
        
        _historyProvider.AppendInputMessage("You are a medical assistant designed to bridge the gap between the client and healthcare professionals. Your task is to gather information about the client's symptoms and concerns, recommend the most appropriate medical category based on their input, and help them view available doctors within that category. There are no real doctors i can tell you about for now, but you can imagine some. You should never recommend any medication! As soon as client is over with telling about his symptoms, your goal is to ask about his location and suggest some imaginary doctors. Keep in mind that this is may be the continuation of our dialog. Here's what we talked about before, so please dont forget it: ");
        
        await Clients.All.ReceiveMessage(startChatResult.Value);
    }

    public async Task SendMessage(string message)
    {
        var command = new SendMessageCommand(message, _historyProvider.GetChatHistory());
        await Clients.Caller.SendMessage(message);
        
        var sendMessageResult = await _mediator.Send(command);

        if (sendMessageResult.IsError)
        {
            await Clients.Caller.ReceiveMessage("Something went wrong");
            
            return;
        }
        
        _historyProvider.AppendInputMessage(message);
        
        _historyProvider.AppendOutputMessage(sendMessageResult.Value);
        
        await Clients.All.ReceiveMessage(sendMessageResult.Value);
    }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.ReceiveMessage($"{Context.ConnectionId} has connected successfully");
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _historyProvider.ClearHistory();
        return base.OnDisconnectedAsync(exception);
    }
}