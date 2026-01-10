using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Users.Models;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Users;

public class UserService(
    IUnitOfWork unitOfWork,
    IPaginationService paginationService,
    IEnumService enumService) : IUserService
{
    public async Task<int> GetAllUsersCount()
    {
        return await unitOfWork.Users.SelectAllAsQueryable().CountAsync();
    }

    public async Task<string> GetUserPhoneByTelegramChatIdAsync(long chatId)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.TelegramChatId == chatId);

        return user is not null ? user.Phone : null;
    }

    public async Task<bool> SetBlockStatusAsync(int id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException("User was not found!");

        user.IsBlocked = user.IsBlocked == true ? false : true;
        user.IsActive = user.IsBlocked == true ? false : true;

        unitOfWork.Users.Update(user);

        await unitOfWork.SaveAsync();

        return user.IsBlocked;
    }

    public async Task ChangePasswordAsync(int id, UserPasswordModel model)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException("User was not found");

    }

    public async Task<UserViewModel> GetAsync(int id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException("User was not found!");

        return new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Status = user.IsActive ? "Faol" : "Nofaol",
            IsUseSystem = user.IsUseSystem,
            IsBlocked = user.IsBlocked,
            Role = enumService.GetEnumDescription(user.Role)
        };
    }

    public async Task<List<UserTableViewModel>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var query = unitOfWork.Users.SelectAllAsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchText = search.ToLower().Trim();

            query = query.Where(u =>
                u.FirstName.Contains(searchText) ||
                u.LastName.Contains(searchText) ||
                u.Phone.Contains(searchText) ||
                u.Email.Contains(searchText));
        }

        query = paginationService.Paginate(query, @params);

        return await query.Select(u => new UserTableViewModel
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Phone = u.Phone,
            Email = u.Email,
            Status = u.IsActive ? "Faol" : "Nofaol",
            IsUseSystem = u.IsUseSystem,
            IsBlocked = u.IsBlocked,
            Role = enumService.GetEnumDescription(u.Role)
        }).ToListAsync();
    }
}
