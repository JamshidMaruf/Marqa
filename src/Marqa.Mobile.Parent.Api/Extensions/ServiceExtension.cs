using System.Text;
using FluentValidation;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Settings;
using Marqa.Service.Validators.Companies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Marqa.Mobile.Parent.Api.Extensions;

public static class ServicesExtension
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();
    }

    public async static Task AddJWTServiceAsync(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        bool check = await serviceProvider
            .GetService<IRepository<User>>()
            .CanConnectAsync();

        if (check)
        {
            var settingService = serviceProvider.GetService<ISettingService>();

            var jwtSettings = await settingService.GetByCategoryAsync("JWT");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["JWT.Issuer"],
                        ValidAudience = jwtSettings["JWT.Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JWT.Key"]))
                    };
                });
        }
    }
}
