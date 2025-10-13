using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.Students;
using Marqa.Service.Services.Subjects;
using Marqa.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option 
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
builder.Services.AddScoped<IHomeTaskService, HomeTaskService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IPointSettingService,  PointSettingService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();