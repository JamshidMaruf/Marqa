using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Auth;

public interface IJwtService
{
    ValueTask<(string Token, DateTime ExpiresIn)> GenerateToken(User user, string role);
    string GenerateRefreshToken();
    Task<string> GenerateAppToken(string appId, string secretKey);
    Task<string> GenerateEmployeeTokenAsync(int employeeId, string role);
}