using System.Text;
using FluentValidation;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Settings;
using Marqa.Service.Validators.Companies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Marqa.Mobile.Teacher.Api.Extensions;

public static class ServicesExtension
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();
    }
}
