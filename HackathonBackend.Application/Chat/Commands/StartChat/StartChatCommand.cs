using ErrorOr;
using HackathonBackend.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public record StartChatCommand() : IRequest<ErrorOr<string>>;