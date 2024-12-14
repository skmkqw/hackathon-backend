using HackathonBackend.Application.Authentication.Commands.Register;
using HackathonBackend.Application.Authentication.Commands.RegisterDoctor;
using HackathonBackend.Application.Authentication.Queries.Login;
using HackathonBackend.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HackathonBackend.API.Controllers;

[AllowAnonymous]
[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<AuthenticationController> _logger;

    private readonly CookieOptions _cookieOptions = new()
    {
        HttpOnly = false,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = DateTime.UtcNow.AddDays(1)
    };

    public AuthenticationController(IMapper mapper, IMediator mediator, ILogger<AuthenticationController> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        _logger.LogInformation("Register request received for email: {Email}", request.Email);
        
        var command = _mapper.Map<RegisterCommand>(request);
        var registerUserResult = await _mediator.Send(command);

        if (registerUserResult.IsError)
        {
            _logger.LogInformation("Registration failed for email: {Email}. Errors: {Errors}", request.Email, registerUserResult.Errors);
            return Problem(registerUserResult.Errors);
        }

        _logger.LogInformation("Registration successful for email: {Email}", request.Email);
        HttpContext.Response.Cookies.Append("AuthToken", registerUserResult.Value.Token, _cookieOptions);

        return Ok(_mapper.Map<AuthenticationResponse>(registerUserResult.Value));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Login request received for email: {Email}", request.Email);
        
        var query = _mapper.Map<LoginQuery>(request);
        var loginQueryResult = await _mediator.Send(query);

        if (loginQueryResult.IsError)
        {
            _logger.LogInformation("Login failed for email: {Email}. Errors: {Errors}", request.Email, loginQueryResult.Errors);
            return Problem(loginQueryResult.Errors);
        }

        _logger.LogInformation("Login successful for email: {Email}", request.Email);
        HttpContext.Response.Cookies.Append("AuthToken", loginQueryResult.Value.Token, _cookieOptions);

        return Ok(_mapper.Map<AuthenticationResponse>(loginQueryResult.Value));
    }
    
    
    [HttpPost("doctors/register")]
    public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorRequest request)
    {
        _logger.LogInformation("Register doctor request received for email: {Email}", request.Email);
        
        var command = _mapper.Map<RegisterDoctorCommand>(request);
        var registerUserResult = await _mediator.Send(command);

        if (registerUserResult.IsError)
        {
            _logger.LogInformation("Registration failed for email: {Email}. Errors: {Errors}", request.Email, registerUserResult.Errors);
            return Problem(registerUserResult.Errors);
        }

        _logger.LogInformation("Registration successful for email: {Email}", request.Email);
        HttpContext.Response.Cookies.Append("AuthToken", registerUserResult.Value.Token, _cookieOptions);

        return Ok(_mapper.Map<DoctorAuthenticationResponse>(registerUserResult.Value));
    }
}