using ErrorOr;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.SendMessage;

public record SendMessageCommand(string Message, string History) : IRequest<ErrorOr<string>>;