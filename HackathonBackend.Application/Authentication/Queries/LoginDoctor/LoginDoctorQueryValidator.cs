using FluentValidation;
using HackathonBackend.Application.Authentication.Queries.Login;

namespace HackathonBackend.Application.Authentication.Queries.LoginDoctor;

public class LoginDoctorQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginDoctorQueryValidator()
    {        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}