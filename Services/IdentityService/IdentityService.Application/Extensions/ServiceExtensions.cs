using System.Reflection;
using FluentValidation;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Mappers;
using IdentityService.Application.Options;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
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
            .ConfigureOptions(configuration);

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

        return services;
    }
}
