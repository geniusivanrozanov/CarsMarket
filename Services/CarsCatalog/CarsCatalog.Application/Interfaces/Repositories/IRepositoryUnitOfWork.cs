namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IRepositoryUnitOfWork
{
    IBrandRepository Brands { get; }
    IModelRepository Models { get; }
    IGenerationRepository Generations { get; }

    Task SaveAsync(CancellationToken cancellationToken);
}
