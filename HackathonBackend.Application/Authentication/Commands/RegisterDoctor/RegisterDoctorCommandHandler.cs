using ErrorOr;
using HackathonBackend.Application.Authentication.Commands.Register;
using HackathonBackend.Application.Authentication.Common;
using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Application.Common.Interfaces.Services.Authentication;
using HackathonBackend.Domain.Common.Errors;
using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.DoctorAggregate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HackathonBackend.Application.Authentication.Commands.RegisterDoctor;

public class RegisterDoctorCommandHandler : IRequestHandler<RegisterDoctorCommand, ErrorOr<DoctorAuthenticationResult>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ILogger<RegisterCommandHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ErrorOr<DoctorAuthenticationResult>> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling doctor registration for email: {Email}", request.Email);

        if (await _doctorRepository.FindByEmailAsync(request.Email) is not null)
        {
            _logger.LogInformation("Duplicate email found: {Email}", request.Email);
            return Errors.User.DuplicateEmail;
        }

        string hashedPassword = _passwordHasher.HashPassword(request.Password);
        var doctor = Doctor.Create(request.FirstName, request.LastName, request.Email, hashedPassword, request.Specialization, request.Rating, request.Experience, request.LicenseNumber, request.PhoneNumber);

        if (request.Clinic is not null)
        {
            doctor.SetClinic(Clinic.Create(request.Clinic.Name, request.Clinic.Address, request.Clinic.Rating));
        }

        string token = _jwtTokenGenerator.GenerateDoctorJwtToken(doctor);
        _doctorRepository.Add(doctor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User registered successfully: {Email}", request.Email);
        return new DoctorAuthenticationResult(doctor, token);
    }
}