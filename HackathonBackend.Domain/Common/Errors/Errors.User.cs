using ErrorOr;
using HackathonBackend.Domain.UserAggregate.ValueObjects;

namespace HackathonBackend.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail", 
            description: "A user with given email already exists");
        
        
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found or doesn't exist");
        
        public static Error EmailNotFound(string email) => Error.NotFound(
            code: "User.NotFound",
            description: $"User with email '{email}' not found or doesn't exist");
        
        public static Error IdNotFound(UserId id) => Error.NotFound(
            code: "User.NotFound",
            description: $"User with ID: '{id.Value.ToString()}' not found or doesn't exist");
        
        public static Error Unauthorized => Error.Unauthorized(
            code: "User.Unauthorized",
            description: "You are not authorized to modify this account");
    }
}