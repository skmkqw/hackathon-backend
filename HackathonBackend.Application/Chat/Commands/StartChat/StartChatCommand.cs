using ErrorOr;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public record StartChatCommand() : IRequest<ErrorOr<string>>;