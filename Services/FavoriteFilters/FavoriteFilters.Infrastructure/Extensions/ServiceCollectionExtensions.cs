using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Infrastructure.Data.Contexts;
using FavoriteFilters.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteFilters.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var filtersConnectionString = configuration.GetConnectionString("FiltersPostgres");
        ArgumentException.ThrowIfNullOrEmpty(filtersConnectionString);

        services.AddDbContext<FiltersContext>(builder => { builder.UseNpgsql(filtersConnectionString); });

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
        services.AddScoped<IFilterRepository, FilterRepository>();

        return services;
    }
}
