using ErrorOr;
using HackathonBackend.Application.Authentication.Common;
using MediatR;

namespace HackathonBackend.Application.Authentication.Queries.LoginDoctor;

public record LoginDoctorQuery(string Email, string Password) : IRequest<ErrorOr<DoctorAuthenticationResult>>;