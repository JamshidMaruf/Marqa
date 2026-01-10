using Marqa.Service.Services.Users.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Users;

public interface IUserService : IScopedService
{
    Task<int> GetAllUsersCount();
    Task<string> GetUserPhoneByTelegramChatIdAsync(long chatId);
    Task<List<UserTableViewModel>> GetAllAsync(PaginationParams @params, string search = null);
    Task<UserViewModel> GetAsync(int id);
    Task<bool> SetBlockStatusAsync(int id);
    Task ChangePasswordAsync(int id, UserPasswordModel model);
}
