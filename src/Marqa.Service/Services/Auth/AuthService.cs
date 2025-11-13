using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.Auth;

public class AuthService(ISettingService settingService, IEncryptionService encryptionService) : IAuthService
{
    public async Task<string> GenerateToken(string app, int entityId, string entityType)
    {
        var configuration = await settingService.GetByCategoryAsync("JWT");
        var claims = new[]
        {
            new Claim("EntityId", entityId.ToString()),
            new Claim("EntityType", entityType),
            new Claim(ClaimTypes.Role, app),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT.Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT.Issuer"],
            audience: configuration["JWT.Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT.ExpiresInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<string> GenerateAppToken(string appId, string secretKey)
    {
        var configuration = await settingService.GetByCategoryAsync("JWT");
        var appSettings = await settingService.GetByCategoryAsync("App");
        
        if(appSettings.ContainsValue(appId) && appSettings.ContainsValue(secretKey))
        {
            var decryptedApp = encryptionService.Decrypt(secretKey);
            
            var claims = new[]
            {
                new Claim("Name", "AppToken"),
                new Claim("App", decryptedApp),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT.Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT.Issuer"],
                audience: configuration["JWT.Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT.Expires"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        throw new NotFoundException("AppId or SecretKey is incorrect!");
    }

    public async Task<string> GenerateEmployeeTokenAsync(int employeeId, string role)
    {
        var configuration = await settingService.GetByCategoryAsync("JWT");
        var claims = new[]
        {
            new Claim("Id", employeeId.ToString()),
            new Claim("Role", role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT.Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT.Issuer"],
            audience: configuration["JWT.Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT.ExpiresInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}