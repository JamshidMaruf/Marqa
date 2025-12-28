using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Users;

public interface IUserService : IScopedService
{
    Task<int> GetAllUsersCount();
    Task<string> GetUserPhoneByChatIdAsync(long chatId);
}
