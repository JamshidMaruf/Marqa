using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateToken(string app);
    Task<string> GenerateAppToken(string appId, string secretKey);
}