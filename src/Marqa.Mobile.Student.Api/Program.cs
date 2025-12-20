using Marqa.DataAccess.Contexts;
using Marqa.Mobile.Student.Api.Extensions;
using Marqa.Mobile.Student.Api.Filters;
using Marqa.Mobile.Student.Api.Helpers;
using Marqa.Mobile.Student.Api.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
        new LowerCaseControllerName()));
    options.Filters.Add(new PermissionAuthorizationFilter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(option
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

builder.Services.AddMarqaServices();

await builder.Services.AddJWTServiceAsync();

//builder.Services.AddSwaggerService();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
