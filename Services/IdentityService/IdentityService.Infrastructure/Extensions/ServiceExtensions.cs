using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Data.Contexts;
using IdentityService.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContexts(configuration)
            .AddRepositories()
            .AddIdentity()
            .MigrateDatabase<IdentityContext>();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var identityConnectionString = configuration.GetConnectionString("IdentityPostgres");
        ArgumentException.ThrowIfNullOrEmpty(identityConnectionString);
        
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseNpgsql(identityConnectionString);
        });
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<UserEntity>()
            .AddEntityFrameworkStores<IdentityContext>();

        return services;
    }
    
    private static IServiceCollection MigrateDatabase<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<TContext>();

        try
        {
            context.Database.Migrate();
        }
        catch (Exception)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
            logger.LogError("Failed to apply {Context} migrations", typeof(TContext).Name);
            throw;
        }
        
        return services;
    }
}