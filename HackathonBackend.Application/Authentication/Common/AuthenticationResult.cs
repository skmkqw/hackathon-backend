using HackathonBackend.Domain.UserAggregate;

namespace HackathonBackend.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);