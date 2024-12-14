namespace HackathonBackend.Contracts.Authentication;

public record User(string Id, string FirstName, string LastName, string Email, DateOnly BirthDate, string PhoneNumber);

public record AuthenticationResponse(
    User User,
    string Token);