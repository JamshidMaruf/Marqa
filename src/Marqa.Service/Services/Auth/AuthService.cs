using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.Auth;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public string GenerateToken(int entityId, string entityType, string subject)
    {
        var claims = new[]
        {
            new Claim("EntityId", entityId.ToString()),
            new Claim("EntityType", entityType),
            new Claim("Subject", subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT:ExpiresInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}