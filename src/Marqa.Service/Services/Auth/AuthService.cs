using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Auth.Models;
using Marqa.Service.Services.Settings;
using Marqa.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Auth;

public class AuthService(
    IUnitOfWork unitOfWork,
    IJwtService jwtService, 
    ISettingService settingService) : IAuthService
{
    public async ValueTask<LoginResponseModel> LoginAsync(LoginModel model, string ipAddress)
    {
        var trimedPhoneResult = model.Phone.TrimPhoneNumber();
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Phone == trimedPhoneResult.Phone)
            ?? throw new NotFoundException("User not found with this phone");
        
        if(!PasswordHelper.Verify(model.Password, existUser.PasswordHash))
            throw new NotMatchedException("Password does not match");
        
        if(!existUser.IsActive)
            throw new ArgumentIsNotValidException("User is not active");
        
        if(!existUser.IsUseSystem)
            throw new ArgumentIsNotValidException("User is not use system");
        
        (string name, int id) role = ("student", 0);
        
        (string name, int id) company = ("", 0);
        
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

            var employeeCompany = await unitOfWork.Employees
                .SelectAllAsQueryable(e => e.UserId == existUser.Id, includes: "Company")
                .Select(c => new { c.Company.Name, c.Company.Id })
                .FirstOrDefaultAsync();
            
            company = (employeeCompany.Name, employeeCompany.Id);
        }
        
        var accessToken = await jwtService.GenerateJwtToken(existUser, role.name);

        // refresh token
        var refreshToken = jwtService.GenerateRefreshToken();

        var configuration = await settingService.GetByCategoryAsync("RefreshToken");

        var refreshTokenExpiresIn = model.RememberMe 
            ? DateTime.UtcNow.AddDays(Convert.ToInt16(configuration["RefreshToken.Expires.RememberMe"])) 
            : DateTime.UtcNow.AddDays(Convert.ToInt16(configuration["RefreshToken.Expires.Standard"]));
    
        var refreshTokenEntity = new RefreshToken
        {
            UserId = existUser.Id,
            Token = refreshToken, 
            ExpiresAt = refreshTokenExpiresIn, 
            CreatedByIp = ipAddress,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = DateTime.UtcNow
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
                Permissions = permissions,
                CompanyId = company.id,
                CompanyName = company.name
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

    public async ValueTask<LoginResponseModel> RefreshTokenAsync(RefreshTokenModel model)
    {
        var refreshToken = await unitOfWork.RefreshTokens
            .SelectAsync(r => r.Token == model.Token)
            ?? throw new NotFoundException("Refresh token not found");
        
        if(refreshToken.IsExpired)
            throw new ArgumentIsNotValidException("Refresh token is expired");
        
        if(refreshToken.IsRevoked)
            throw new ArgumentIsNotValidException("Refresh is revoked");
        
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == refreshToken.UserId)
             ?? throw new NotFoundException("User not found");
        
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
        
        var permissions = await unitOfWork.RolePermissions
            .SelectAllAsQueryable(predicate: r => r.RoleId == role.id, includes: "Permission")
            .Select(r => r.Permission.Name)
            .ToListAsync();
        
        var accessToken = await jwtService.GenerateJwtToken(existUser, role.name);
        
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
                RefreshToken = refreshToken.Token,
                ExpiresIn = accessToken.ExpiresIn,
                TokenType = "Bearer"
            }
        };
    }

    public async ValueTask<bool> LogoutAsync(LogoutModel model, string ipAddress)
    {
        var refreshToken = await unitOfWork.RefreshTokens
            .SelectAsync(r => r.Token == model.Token)
            ?? throw new NotFoundException("Refresh token not found");
        
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        
        unitOfWork.RefreshTokens.Update(refreshToken);
        await unitOfWork.SaveAsync();
        
        return true;
    }

    public async ValueTask<LoginResponseModel.UserData> GetCurrentUser(RefreshTokenModel model)
    {
        var refreshToken = await unitOfWork.RefreshTokens
            .SelectAsync(r => r.Token == model.Token)
            ?? throw new NotFoundException("Refresh token not found");

        if (refreshToken.IsExpired)
            throw new ArgumentIsNotValidException("Refresh token is expired");

        if (refreshToken.IsRevoked)
            throw new ArgumentIsNotValidException("Refresh is revoked");

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == refreshToken.UserId)
             ?? throw new NotFoundException("User not found");

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
                .FirstOrDefaultAsync();

            role.name = employeeRole.Name;
        }

        var permissions = await unitOfWork.RolePermissions
            .SelectAllAsQueryable(predicate: r => r.RoleId == role.id, includes: "Permission")
            .Select(r => r.Permission.Name)
            .ToListAsync();

        return new LoginResponseModel.UserData
        {
            Id = existUser.Id,
            FirstName = existUser.FirstName,
            LastName = existUser.LastName,
            Phone = existUser.Phone,
            Email = existUser.Email,
            Role = role.name,
            Permissions = permissions
        };
    }

    public async Task<LoginResponseModel> LoginAdminAsync(LoginModel model, string ipAddress)
    {
        //    persist this command on postgresql (temporary data), password -> root
        //INSERT INTO users(first_name, last_name, phone, is_active, is_use_system, password_hash, role, created_at, is_deleted)
        //VALUES('Super', 'Admin', '998777777777', true, true, '$2a$12$o83E6Kk.p1Z7oZwMhspsruLxcNZuicrvSBAgP05wfJASrDR0AXAUm', 4, '2026-01-07 11:49:00+05', true);

        var phone = model.Phone.TrimPhoneNumber().Phone;
        var user = await unitOfWork.Users.SelectAsync(u => u.Phone == phone)
            ?? throw new UnauthorizedAccessException("Invalid phone or password");

        // Validate password
        if (!PasswordHelper.Verify(model.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid phone or password");
        
        var accessToken = await jwtService.GenerateJwtToken(user,"SuperAdmin");

        // refresh token
        var refreshToken = jwtService.GenerateRefreshToken();

        var configuration = await settingService.GetByCategoryAsync("RefreshToken");

        var refreshTokenExpiresIn = model.RememberMe 
            ? DateTime.UtcNow.AddDays(Convert.ToInt16(configuration["RefreshToken.Expires.RememberMe"])) 
            : DateTime.UtcNow.AddDays(Convert.ToInt16(configuration["RefreshToken.Expires.Standard"]));
    
        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken, 
            ExpiresAt = refreshTokenExpiresIn, 
            CreatedByIp = ipAddress
        };
        
        unitOfWork.RefreshTokens.Insert(refreshTokenEntity);
        await unitOfWork.SaveAsync();
        
        // Return admin login response
        return new LoginResponseModel
        {
            User = new LoginResponseModel.UserData
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = "SuperAdmin",
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
}