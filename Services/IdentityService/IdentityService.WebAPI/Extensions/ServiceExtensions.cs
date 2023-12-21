using System.Text;
using FluentValidation.AspNetCore;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Options;
using IdentityService.WebAPI.Middlewares;
using IdentityService.WebAPI.Options;
using IdentityService.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using StackExchange.Redis;

namespace IdentityService.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddControllers().Services
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddAuthorization()
            .ConfigureAuthentication()
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddValidators()
            .AddMiddlewares()
            .AddSwagger()
            .AddDistributedCache(configuration)
            .AddGrpc()
            .ConfigureOptions(configuration);
    }

    private static IServiceCollection AddGrpc(this IServiceCollection services)
    {
        services.AddCodeFirstGrpc();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddSingleton<ExceptionHandlerMiddleware>();

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        var jwtOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<JwtOptions>>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Value.Issuer,
                    ValidAudience = jwtOptions.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    private static IServiceCollection AddDistributedCache(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");
        var databaseNumber = configuration["DistributedCacheOptions:RedisDatabaseNumber"];
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        ArgumentException.ThrowIfNullOrEmpty(databaseNumber);
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                DefaultDatabase = int.Parse(databaseNumber),
                EndPoints = { { connectionString } }
            };
        });

        return services;
    }


    private static IServiceCollection ConfigureOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DistributedCacheOptions>(configuration.GetSection(nameof(DistributedCacheOptions)));

        return services;
    }
}
