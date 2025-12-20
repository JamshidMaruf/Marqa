﻿namespace Marqa.Service.Services.Users;

public interface IUserService : IScopedService
{
    Task<int> GetAllUsersCount();
}
