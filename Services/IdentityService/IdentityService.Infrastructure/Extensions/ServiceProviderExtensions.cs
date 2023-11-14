using IdentityService.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task ApplyInfrastructureLayer(this IServiceProvider services)
    {
        await services.MigrateDatabase<IdentityContext>();
    }
    
    private static async Task MigrateDatabase<TContext>(this IServiceProvider services)
        where TContext : DbContext
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
            logger.LogError("Failed to apply {Context} migrations", typeof(TContext).Name);
            throw;
        }
    }
}
