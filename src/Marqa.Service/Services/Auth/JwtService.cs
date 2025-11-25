using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Marqa.Domain.Entities;
using Marqa.Service.Services.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.Auth;

public class JwtService(ISettingService settingService, IEncryptionService encryptionService) : IJwtService
{
    public async ValueTask<(string Token, DateTime ExpiresIn)> GenerateJwtToken(User user, string role)
    {
        var configuration = await settingService.GetByCategoryAsync("JWT");
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT.Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(configuration["JWT.Expires"]));
        
        var token = new JwtSecurityToken(
            issuer: configuration["JWT.Issuer"],
            audience: configuration["JWT.Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return (Token: new JwtSecurityTokenHandler().WriteToken(token), ExpiresIn: expires);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32]; // 32 bytes for a 256-bit token
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}