using System.Text;
using FluentValidation;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Servcies.Products;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Exams;
using Marqa.Service.Services.Files;
using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.Products;
using Marqa.Service.Services.Ratings;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.StudentPointHistories;
using Marqa.Service.Services.Students;
using Marqa.Service.Services.Subjects;
using Marqa.Service.Validators.Companies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Admin.WebApi.Extensions;

public static class ServicesExtensions
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
        services.AddScoped<IHomeTaskService, HomeTaskService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IPointSettingService, PointSettingService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStudentPointHistoryService, StudentPointHistoryService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();
    }

    public static void AddJWTService(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        var settingService = serviceProvider.GetService<ISettingService>();

        var jwtSettings = settingService.GetByCategoryAsync("JWT").GetAwaiter().GetResult();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["JWT:Issuer"],
                    ValidAudience = jwtSettings["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JWT:Key"]))
                };
            });
    }
}
