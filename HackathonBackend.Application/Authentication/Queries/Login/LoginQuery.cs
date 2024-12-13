using ErrorOr;
using HackathonBackend.Application.Authentication.Common;
using MediatR;

namespace HackathonBackend.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;