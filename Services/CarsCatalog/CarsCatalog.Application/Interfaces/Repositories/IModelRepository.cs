using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IModelRepository : IRepositoryBase<ModelEntity, Guid>
{
    Task<IEnumerable<TProjection>> GetModelsAsync<TProjection>(
        Guid? brandId = default,
        string? brandName = default,
        CancellationToken cancellationToken = default);

    Task<TProjection?> GetModelByIdAsync<TProjection>(Guid modelId, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithNameAndBrandIdAsync(string name, Guid brandId, CancellationToken cancellationToken = default);
}
