using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Infrastructure.Data.Contexts;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private IBrandRepository? _brandRepository;
    private IModelRepository? _modelRepository;
    private IGenerationRepository? _generationRepository;
    private readonly CatalogContext _context;

    public RepositoryUnitOfWork(CatalogContext context)
    {
        _context = context;
    }

    public IBrandRepository Brands => _brandRepository ??= new BrandRepository(_context);
    public IModelRepository Models => _modelRepository ??= new ModelRepository(_context);
    public IGenerationRepository Generations => _generationRepository ??= new GenerationRepository(_context);

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
