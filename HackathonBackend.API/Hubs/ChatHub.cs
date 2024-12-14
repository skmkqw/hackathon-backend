using HackathonBackend.Application.Chat.Commands.SendMessage;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HackathonBackend.API.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string message);
    
    Task SendMessage(string message);
}

public class ChatHub : Hub<IChatClient>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChatHub(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task SendMessage(string message)
    {
        // var command = _mapper.Map<SendMessageCommand>(message);
        var command = new SendMessageCommand(message);
        await Clients.Caller.SendMessage(message);
        
        var sendMessageResult = await _mediator.Send(command);

        if (sendMessageResult.IsError)
        {
            await Clients.Caller.ReceiveMessage("Something went wrong");
            
            return;
        }
        
        await Clients.All.ReceiveMessage(sendMessageResult.Value);
    }
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.ReceiveMessage($"{Context.ConnectionId} has connected successfully");
    }
}