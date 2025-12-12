namespace Marqa.Service.Services.Users;

public interface IUserService
{
    Task<int> GetAllUsersCount();
}
