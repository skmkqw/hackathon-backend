using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.UserAggregate;

namespace HackathonBackend.Application.Common.Interfaces.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
    
    string GenerateDoctorJwtToken(Doctor doctor);
}