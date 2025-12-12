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
}
