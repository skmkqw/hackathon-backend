using ErrorOr;
using HackathonBackend.Application.Authentication.Common;
using MediatR;

namespace HackathonBackend.Application.Authentication.Commands.RegisterDoctor;

public record RegisterDoctorCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Specialization,
    float Rating,
    string LicenseNumber,
    float Experience,
    ClinicCommand? Clinic) : IRequest<ErrorOr<DoctorAuthenticationResult>>;
    
public record ClinicCommand(string Name, string Address, float Rating);