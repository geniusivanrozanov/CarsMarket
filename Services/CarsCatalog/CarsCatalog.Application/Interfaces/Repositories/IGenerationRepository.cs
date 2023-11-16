using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IGenerationRepository
{
    Task<IEnumerable<TProjection>> GetGenerationsAsync<TProjection>(
        Guid? brandId = default,
        string? brandName = default,
        Guid? modelId = default,
        string? modelName = default,
        int? productionYear = default,
        CancellationToken cancellationToken = default);
    Task<TProjection?> GetGenerationByIdAsync<TProjection>(Guid generationId, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithNameAndModelIdAsync(string name, Guid modelId, CancellationToken cancellationToken = default);
    void CreateGeneration(GenerationEntity generation);
    void UpdateGeneration(GenerationEntity generation);
    void DeleteGeneration(GenerationEntity generation);
}
