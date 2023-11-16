using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Infrastructure.Data.Contexts;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class RepositoryUnitOfWork(CatalogContext context) : IRepositoryUnitOfWork
{
    private IBrandRepository? _brandRepository;
    private IModelRepository? _modelRepository;
    private IGenerationRepository? _generationRepository;

    public IBrandRepository Brands => _brandRepository ??= new BrandRepository(context);
    public IModelRepository Models => _modelRepository ??= new ModelRepository(context);
    public IGenerationRepository Generations => _generationRepository ??= new GenerationRepository(context);

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
