using FluentValidation;

namespace HackathonBackend.Application.Chat.Commands.StartChat;

public class StartChatCommandValidator : AbstractValidator<StartChatCommand>
{
    public StartChatCommandValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message cannot be empty");
    }
}