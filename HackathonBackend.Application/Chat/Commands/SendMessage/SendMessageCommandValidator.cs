using FluentValidation;

namespace HackathonBackend.Application.Chat.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(c => c.Message).NotEmpty().WithMessage("Message is required");
    }
}