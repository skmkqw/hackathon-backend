using ErrorOr;
using HackathonBackend.Application.Authentication.Common;
using HackathonBackend.Application.Authentication.Queries.Login;
using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Application.Common.Interfaces.Services.Authentication;
using HackathonBackend.Domain.Common.Errors;
using HackathonBackend.Domain.DoctorAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HackathonBackend.Application.Authentication.Queries.LoginDoctor;

public class LoginDoctorQueryHandler : IRequestHandler<LoginDoctorQuery, ErrorOr<DoctorAuthenticationResult>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LoginQueryHandler> _logger;

    public LoginDoctorQueryHandler(
        IDoctorRepository doctorRepository, 
        IJwtTokenGenerator tokenGenerator, 
        IPasswordHasher passwordHasher, 
        ILogger<LoginQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<ErrorOr<DoctorAuthenticationResult>> Handle(LoginDoctorQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt started for email: {Email}", request.Email);

        if (await _doctorRepository.FindByEmailAsync(request.Email) is not Doctor doctor)
        {
            _logger.LogInformation("Login failed: User not found for email: {Email}", request.Email);
            return Errors.Authentication.InvalidCredentials;
        }

        if (!_passwordHasher.VerifyHashedPassword(doctor.Password, request.Password))
        {
            _logger.LogInformation("Login failed: Invalid password for email: {Email}", request.Email);
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _tokenGenerator.GenerateDoctorJwtToken(doctor);

        _logger.LogInformation("Login successful for email: {Email}", request.Email);
        return new DoctorAuthenticationResult(doctor, token);
    }
}