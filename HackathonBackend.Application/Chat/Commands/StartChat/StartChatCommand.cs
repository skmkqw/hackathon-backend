using ErrorOr;
using HackathonBackend.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public record StartChatCommand(string Message, UserId UserId) : IRequest<ErrorOr<string>>;