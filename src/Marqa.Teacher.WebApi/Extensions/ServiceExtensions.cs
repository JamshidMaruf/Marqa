using System.Text;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Settings;
using Marqa.Service.Validators.Companies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation;

namespace Marqa.Teacher.WebApi.Extensions;

public static class ServicesExtension
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAuthService, JwtService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();
    }
}
