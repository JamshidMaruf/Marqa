using FluentValidation;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Exams;
using Marqa.Service.Services.Files;
using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.Messages;
using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.Products;
using Marqa.Service.Services.Ratings;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.StudentPointHistories;
using Marqa.Service.Services.Students;
using Marqa.Service.Services.Users;
using Marqa.Service.Validators.Companies;
using Marqa.Shared.Services;

namespace Marqa.Admin.Extensions;

public static class ServiceExtensions
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        // Core infrastructure
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // DI-based services 
        services.AddScoped<IEnvironmentService, EnvironmentService>();
        services.AddScoped<IHttpContextService, HttpContextService>();
        services.AddScoped<IPaginationService, PaginationService>();

        // Auto-register all services using Scrutor
        services.Scan(scan => scan
            .FromAssemblyOf<IScopedService>()
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<ISingletonService>()
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<ITransientService>()
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        // FluentValidation auto-registration
        services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();
    }
}
