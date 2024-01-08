using System.Reflection;
using FluentValidation;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Mappers;
using IdentityService.Application.Options;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddIdentity()
            .AddMappers()
            .AddValidators()
            .ConfigureOptions(configuration)
            .ConfigureMassTransit(configuration);

        return services;
    }

    private static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMQConfiguration:Host"];
        var username = configuration["RabbitMQConfiguration:Username"];
        var password = configuration["RabbitMQConfiguration:Password"];
        
        ArgumentException.ThrowIfNullOrEmpty(host);
        ArgumentException.ThrowIfNullOrEmpty(username);
        ArgumentException.ThrowIfNullOrEmpty(password);
        
        
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(host, hostConfigurator =>
                {
                    hostConfigurator.Username(username);
                    hostConfigurator.Password(password);
                });
            });
        });

        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<UserEntity>()
            .AddRoles<IdentityRole<Guid>>();

        return services;
    }
    
    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<RefreshTokenOptions>(configuration.GetSection(nameof(RefreshTokenOptions)));

        return services;
    }
}
