using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HackathonBackend.Application.Common.Interfaces.Services.Authentication;
using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZBank.Application.Common.Interfaces.Services;

namespace HackathonBackend.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateJwtToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)), 
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, "User")
        };

        var securityToken = new JwtSecurityToken(
            claims: claims, 
            signingCredentials: signingCredentials, 
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddDays(_jwtSettings.ExpiryDays));
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    
    public string GenerateDoctorJwtToken(Doctor doctor)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)), 
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, doctor.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, doctor.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, doctor.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, "Doctor")
        };

        var securityToken = new JwtSecurityToken(
            claims: claims, 
            signingCredentials: signingCredentials, 
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddDays(_jwtSettings.ExpiryDays));
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}