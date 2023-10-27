using System.Reflection;
using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Mappers;
using IdentityService.Application.Options;
using IdentityService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddMappers()
            .AddValidators()
            .ConfigureOptions(configuration)
            .AddDefaultUsers(configuration).Wait();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

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

    private static async Task<IServiceCollection> AddDefaultUsers(this IServiceCollection services,
        IConfiguration configuration)
    {
        var defaultUsers = configuration
            .GetSection("DefaultUsers")
            .GetChildren();
        var serviceProvider = services.BuildServiceProvider();
        var userService = ActivatorUtilities.CreateInstance<UserService>(serviceProvider);

        foreach (var user in defaultUsers)
        {
            var register = new RegisterDto
            {
                Email = user[nameof(RegisterDto.Email)]!,
                Password = user[nameof(RegisterDto.Password)]!,
                FirstName = user[nameof(RegisterDto.FirstName)]!,
                LastName = user[nameof(RegisterDto.LastName)]!
            };

            var role = user["Role"];

            await userService.EnsureUserCreatedAsync(register, role!);
        }

        return services;
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        return services;
    }
}
