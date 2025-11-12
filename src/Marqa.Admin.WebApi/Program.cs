using Marqa.Admin.WebApi.Extensions;
using Marqa.Admin.WebApi.Middlewares;
using Marqa.DataAccess.Contexts;
using Marqa.Service.Helpers;
using Marqa.Shared.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService();

builder.Services.AddDbContext<AppDbContext>(option 
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

builder.Services.AddMarqaServices();

builder.Services.AddJWTService();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
        new LowerCaseControllerName()));
});

builder.Services.AddAuthorization();

var app = builder.Build();

EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();