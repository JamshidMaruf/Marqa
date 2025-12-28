using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<int> GetAllUsersCount()
    {
        var users = await unitOfWork.Users.SelectAllAsQueryable()
            .Select(u => u.Id)
            .ToListAsync();

        return users.Count();
    }

    public async Task<string> GetUserPhoneByChatIdAsync(long chatId)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.TelegramChatId == chatId);

        return user.Phone;
    }
}
