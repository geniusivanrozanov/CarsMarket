using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Infrastructure.Data.Contexts;
using CarsCatalog.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsCatalog.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDbContexts(configuration)
            .AddRepositories();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var catalogConnectionString = configuration.GetConnectionString("CatalogPostgres");
        ArgumentException.ThrowIfNullOrEmpty(catalogConnectionString);

        services.AddDbContext<CatalogContext>(builder => { builder.UseNpgsql(catalogConnectionString); });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();

        return services;
    }
}
