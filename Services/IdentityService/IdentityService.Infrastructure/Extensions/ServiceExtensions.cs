using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Data.Contexts;
using IdentityService.Infrastructure.Data.Interceptors;
using IdentityService.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDbContexts(configuration)
            .AddRepositories()
            .AddRedisConnectionMultiplexer(configuration)
            .AddIdentityEntityFrameworkStores()
            .AddTimeProvider();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var identityConnectionString = configuration.GetConnectionString("IdentityPostgres");
        ArgumentException.ThrowIfNullOrEmpty(identityConnectionString);

        services.AddDbContext<IdentityContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(identityConnectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    private static IServiceCollection AddIdentityEntityFrameworkStores(this IServiceCollection services)
    {
        var identityBuilder = new IdentityBuilder(typeof(UserEntity), typeof(IdentityRole<Guid>), services);
        
        identityBuilder.AddEntityFrameworkStores<IdentityContext>();

        return services;
    }

    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }

    private static IServiceCollection AddRedisConnectionMultiplexer(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));

        return services;
    }
}
