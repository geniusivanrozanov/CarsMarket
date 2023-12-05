using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly CatalogContext _context;
    private readonly IServiceProvider _serviceProvider;

    public RepositoryUnitOfWork(CatalogContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IBrandRepository Brands => _serviceProvider.GetRequiredService<IBrandRepository>();
    public IModelRepository Models => _serviceProvider.GetRequiredService<IModelRepository>();
    public IGenerationRepository Generations => _serviceProvider.GetRequiredService<IGenerationRepository>();

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
