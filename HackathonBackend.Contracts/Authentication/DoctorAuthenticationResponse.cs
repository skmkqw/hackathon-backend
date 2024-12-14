namespace HackathonBackend.Contracts.Authentication;

public record Clinic(string Name, string Address, float Rating);

public record Doctor(string Id, string FirstName, string LastName, string Email, string Specialization, float Rating, float Experience, Clinic? Clinic);

public record DoctorAuthenticationResponse(Doctor Doctor, string Token);