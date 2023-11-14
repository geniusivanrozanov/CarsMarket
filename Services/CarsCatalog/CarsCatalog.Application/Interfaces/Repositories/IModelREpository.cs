using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IModelRepository
{
    Task<TProjection?> GetModelByIdAsync<TProjection>(Guid modelId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TProjection>> GetModelsAsync<TProjection>(CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
    void CreateModel(ModelEntity model);
    void UpdateModel(ModelEntity model);
    void DeleteModel(ModelEntity model);
}
