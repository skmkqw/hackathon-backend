using HackathonBackend.Domain.DoctorAggregate;

namespace HackathonBackend.Application.Authentication.Common;

public record DoctorAuthenticationResult(Doctor Doctor, string Token);