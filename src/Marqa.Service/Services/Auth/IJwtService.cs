using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Auth;

public interface IJwtService
{
    ValueTask<(string Token, DateTime ExpiresIn)> GenerateJwtToken(User user, string role);
    string GenerateRefreshToken();
}