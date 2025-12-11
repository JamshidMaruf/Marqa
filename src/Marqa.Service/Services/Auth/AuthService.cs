using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Auth.Models;
using Marqa.Service.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.Auth;

public class AuthService(
    IUnitOfWork unitOfWork,
    IJwtService jwtService, 
    ISettingService settingService,
    IConfiguration configuration) : IAuthService
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

    public SuperAdminResponseModel LoginAdmin(string phone, string password)
    {
        // temporary admin credentials 
        const string ADMIN_PHONE = "998975777552";
        const string ADMIN_PASSWORD = "root";

        // Validate credentials
        if (phone != ADMIN_PHONE || password != ADMIN_PASSWORD)
        {
            throw new UnauthorizedAccessException("Invalid phone or password");
        }

        // Generate JWT token for admin
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "0"),
            new Claim(ClaimTypes.Name, "Super Admin"),
            new Claim(ClaimTypes.MobilePhone, ADMIN_PHONE),
            new Claim(ClaimTypes.Role, "SuperAdmin"),
            new Claim("Permissions", "all")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresInSeconds = int.Parse(configuration["Jwt:ExpiresInSeconds"] ?? "3600");

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(expiresInSeconds),
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        // Generate refresh token
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        // Return admin login response
        return new SuperAdminResponseModel
        {
            Id = 0L,
            FirstName = "Super",
            LastName = "Admin",
            Phone = ADMIN_PHONE,
            Role = "SuperAdmin",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = expiresInSeconds,
            TokenType = "Bearer"
        };
    }
}