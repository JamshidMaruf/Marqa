﻿﻿using System.Text;
using Asp.Versioning;
using FluentValidation;
using Marqa.Admin.WebApi.Handlers;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Services;
using Marqa.Service.Services.Settings;
using Marqa.Service.Validators.Companies;
using Marqa.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Marqa.Admin.WebApi.Extensions;

public static class ServicesExtensions
{
    public static void AddMarqaServices(this IServiceCollection services)
    {
        // Core infrastructure
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // DI-based services (replacing static helpers)
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

        // Exception handlers
        services.AddExceptionHandler<AlreadyExitExceptionHandler>();
        services.AddExceptionHandler<ValidateExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<NotMatchedExceptionHandler>();
        services.AddExceptionHandler<RequestRefusedExceptionHandler>();
        services.AddExceptionHandler<InternalServerErrorHandler>();
        services.AddProblemDetails();
    }

    public static void AddApiVersioningService(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"),
                new QueryStringApiVersionReader("api-version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    public static async Task AddJWTServiceAsync(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        var repository = serviceProvider.GetService<IRepository<User>>();
        if (repository == null)
            return;

        bool check = await repository.CanConnectAsync();

        if (check)
        {
            var settingService = serviceProvider.GetService<ISettingService>();
            if (settingService == null)
                return;

            var jwtSettings = await settingService.GetByCategoryAsync("JWT");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["JWT.Issuer"],
                        ValidAudience = jwtSettings["JWT.Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JWT.Key"]))
                    };
                });
        }
    }
    
    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = "Bearer",
                Description =
                    "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] { }
                }
            });
        });
    }
}
