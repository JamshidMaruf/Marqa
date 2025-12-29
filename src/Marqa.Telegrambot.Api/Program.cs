using FluentValidation;
using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services;
using Marqa.Service.Validators.Companies;
using Marqa.Shared.Services;
using Marqa.Telegrambot.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHostedService<ConfigureWebhook>();

builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient => 
    new TelegramBotClient(builder.Configuration["BotConfiguration:Token"], httpClient));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresSQLConnection"));
});

builder.Services.AddScoped<BotHandler>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IEnvironmentService, EnvironmentService>();
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();

builder.Services.Scan(scan => scan
            .FromAssemblyOf<IScopedService>()
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<ISingletonService>()
    .AddClasses(classes => classes.AssignableTo<ISingletonService>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<ITransientService>()
    .AddClasses(classes => classes.AssignableTo<ITransientService>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.AddValidatorsFromAssemblyContaining<CompanyCreateModelValidator>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.MapGet("/", () => "NGROK OK");
app.MapControllers();
app.Run();
