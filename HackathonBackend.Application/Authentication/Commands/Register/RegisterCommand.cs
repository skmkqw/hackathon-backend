using ErrorOr;
using HackathonBackend.Application.Authentication.Common;
using MediatR;

namespace HackathonBackend.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    DateOnly BirthDate) : IRequest<ErrorOr<AuthenticationResult>>;