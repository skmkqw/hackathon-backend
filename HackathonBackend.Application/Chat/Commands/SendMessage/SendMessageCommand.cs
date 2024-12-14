using ErrorOr;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.SendMessage;

public record SendMessageCommand(string Message) : IRequest<ErrorOr<string>>;