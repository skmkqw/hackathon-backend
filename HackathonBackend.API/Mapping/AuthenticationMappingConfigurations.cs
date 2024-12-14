using HackathonBackend.Application.Authentication.Commands.Register;
using HackathonBackend.Application.Authentication.Common;
using HackathonBackend.Application.Authentication.Queries.Login;
using HackathonBackend.Application.Authentication.Queries.LoginDoctor;
using HackathonBackend.Contracts.Authentication;
using Mapster;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = Microsoft.AspNetCore.Identity.Data.RegisterRequest;

namespace HackathonBackend.API.Mapping;

public class AuthenticationMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<RegisterDoctorRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<LoginRequest, LoginDoctorQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.User.Id, src => src.User.Id.Value);

        config.NewConfig<DoctorAuthenticationResult, DoctorAuthenticationResponse>()
            .Map(dest => dest.Doctor.Id, src => src.Doctor.Id.Value);
    }
}