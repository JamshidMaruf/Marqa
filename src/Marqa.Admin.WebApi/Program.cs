using Hangfire;
using Hangfire.PostgreSql;
using Marqa.Admin.WebApi.Extensions;
using Marqa.Admin.WebApi.Handlers;
using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Helpers;
using Marqa.Shared.Extensions;
using Marqa.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresSQLConnection")); 
});

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddMarqaServices();

builder.Services.AddHttpContextAccessor();

await builder.Services.AddJWTServiceAsync();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 2);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddExceptionHandler<AlreadyExitExceptionHandler>();
builder.Services.AddExceptionHandler<ValidateExceptionHandler>();
builder.Services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<NotMatchedExceptionHandler>();
builder.Services.AddExceptionHandler<RequestRefusedExceptionHandler>();
builder.Services.AddExceptionHandler<InternalServerErrorHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
        new LowerCaseControllerName()));
});

builder.Services.AddAuthorization();

// Cross-Origin-Resource-Sharing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;
HttpContextHelper.HttpContextAccessor = builder.Services.BuildServiceProvider().GetService<IHttpContextAccessor>();

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("AllowFrontend");

app.UseSwagger();

app.UseSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();