using HackathonBackend.Application.Chat.Commands.StartChat;
using HackathonBackend.Contracts.Chat;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackathonBackend.API.Controllers;

[Route("api/chat")]
public class ChatController : ApiController
{
    private readonly IMapper _mapper;
    
    private readonly IMediator _mediator;
    
    private readonly ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("start")]
    public async Task<IActionResult> StartChat(StartChatRequest request)
    {
        _logger.LogInformation($"Starting chat: {request.Message}");
        var command = _mapper.Map<StartChatCommand>(request);
        
        var startChatResult = await _mediator.Send(command);

        return startChatResult.Match(
            onValue: value => Ok(value),
            onError: errors => Problem(errors)
        );
    }
}