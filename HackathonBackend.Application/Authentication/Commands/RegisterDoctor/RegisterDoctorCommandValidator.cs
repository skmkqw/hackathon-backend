using FluentValidation;

namespace HackathonBackend.Application.Authentication.Commands.RegisterDoctor;

public class RegisterDoctorCommandValidator : AbstractValidator<RegisterDoctorCommand>
{
    public RegisterDoctorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address");
        
        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters");
        
        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("License number is required")
            .Length(2, 100).WithMessage("License number must be between 2 and 100 characters");
        
        RuleFor(x => x.Experience)
            .NotEmpty().WithMessage("Experience is required")
            .GreaterThan(0).WithMessage("Experience must be greater than 0")
            .LessThan(60).WithMessage("Experience must be less than 60");
        
        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating is required")
            .GreaterThan(0).WithMessage("Rating must be greater than 0")
            .LessThan(5).WithMessage("Rating must be less than 60");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Length(12).WithMessage("Phone number must be 12 characters");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
    }
}