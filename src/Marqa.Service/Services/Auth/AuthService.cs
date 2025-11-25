using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Auth;

public class AuthService(IUnitOfWork unitOfWork, IJwtService jwtService) : IAuthService
{
    public async ValueTask<LoginResponseModel> LoginAsync(LoginModel model, string ipAddress)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Phone == model.Phone)
            ?? throw new NotFoundException("User not found with this phone");
        
        if(!PasswordHelper.Verify(model.Password, existUser.PasswordHash))
            throw new NotMatchedException("Password does not match");
        
        if(!existUser.IsActive)
            throw new ArgumentIsNotValidException("User is not active");
        
        if(!existUser.IsUseSystem)
            throw new ArgumentIsNotValidException("User is not use system");
        
        (string name, int id) role = ("student", 0);
        
        if (existUser.Role == UserRole.Employee)
        {
            var employeeRole = await unitOfWork.Employees
                .SelectAllAsQueryable(
                    predicate: e => e.UserId == existUser.Id,
                    includes: ["Role"])
                .Select(e => new
                {
                    Id = e.RoleId,
                    Name = e.Role.Name
                })
                .FirstOrDefaultAsync() ;
            
            role.name = employeeRole.Name;
        }
        
        var accessToken = await jwtService.GenerateToken(existUser, role.name);

        // refresh token
        var refreshToken = jwtService.GenerateRefreshToken();
        
        var refreshTokenExpiresIn = model.RememberMe 
            ? DateTime.UtcNow.AddDays(30) 
            : DateTime.UtcNow.AddDays(7);

        var refreshTokenEntity = new RefreshToken
        {
            UserId = existUser.Id,
            Token = refreshToken, 
            ExpiresAt = refreshTokenExpiresIn, 
            CreatedByIP = ipAddress,
            CreatedAt = DateTime.UtcNow
        };
        
        unitOfWork.RefreshTokens.Insert(refreshTokenEntity);
        await unitOfWork.SaveAsync();

        var permissions = await unitOfWork.RolePermissions
            .SelectAllAsQueryable(predicate: r => r.RoleId == role.id, includes: "Permission")
            .Select(r => r.Permission.Name)
            .ToListAsync();

        return new LoginResponseModel
        {
            User = new LoginResponseModel.UserData
            {
                Id = existUser.Id,
                FirstName = existUser.FirstName,
                LastName = existUser.LastName,
                Phone = existUser.Phone,
                Email = existUser.Email,
                Role = role.name,
                Permissions = permissions
            },
            Token = new LoginResponseModel.TokenData
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken,
                ExpiresIn = accessToken.ExpiresIn,
                TokenType = "Bearer"
            }
        };
    }

    public ValueTask<LoginResponseModel> RefreshTokenAsync(RefreshTokenModel model)
    {
        throw new NotImplementedException();
    }
}