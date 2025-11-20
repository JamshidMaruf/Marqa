using Marqa.Admin.Extensions;
using Marqa.DataAccess.Contexts;
using Marqa.Shared.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddLogging();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Permissions}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();