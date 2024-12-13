using ErrorOr;

namespace HackathonBackend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCredentials", 
            description: "Invalid credentials.");
    }
}