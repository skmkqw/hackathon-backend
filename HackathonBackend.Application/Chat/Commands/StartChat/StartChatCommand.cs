using ErrorOr;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public record StartChatCommand(string Message) : IRequest<ErrorOr<string>>;