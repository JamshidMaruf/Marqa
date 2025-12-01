using Marqa.Admin.WebApi.Extensions;
using Marqa.Admin.WebApi.Handlers;
using Marqa.DataAccess.Contexts;
using Marqa.Service.Helpers;
using Marqa.Shared.Extensions;
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

builder.Services.AddDbContext<AppDbContext>(option 
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

builder.Services.AddMarqaServices();

builder.Services.AddJWTService();

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

var app = builder.Build();

EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();