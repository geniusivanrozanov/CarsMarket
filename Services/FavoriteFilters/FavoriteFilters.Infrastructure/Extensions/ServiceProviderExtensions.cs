using FavoriteFilters.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Infrastructure.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task ApplyInfrastructureLayerAsync(this IServiceProvider services)
    {
        await services.MigrateDatabaseAsync<FiltersContext>();
    }

    private static async Task MigrateDatabaseAsync<TContext>(this IServiceProvider services)
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
