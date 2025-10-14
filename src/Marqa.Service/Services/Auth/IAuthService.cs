using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Auth;

public interface IAuthService
{
    string GenerateToken(int entityId, string entityType, string subject);
}