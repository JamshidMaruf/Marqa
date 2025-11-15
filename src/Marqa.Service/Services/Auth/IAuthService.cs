using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateToken(string app, int entityId, string entityType);
    Task<string> GenerateAppToken(string appId, string secretKey);
    Task<string> GenerateEmployeeTokenAsync(int employeeId, string role);
}