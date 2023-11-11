using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Infrastructure.Data.Contexts;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class RepositoryUnitOfWork(CatalogContext context) : IRepositoryUnitOfWork
{
    private IBrandRepository? _brandRepository;

    public IBrandRepository Brands => _brandRepository ??= new BrandRepository(context);
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
