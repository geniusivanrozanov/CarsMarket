using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<TProjection?> GetBrandByIdAsync<TProjection>(Guid brandId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TProjection>> GetBrandsAsync<TProjection>(CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
    void CreateBrand(BrandEntity brand);
    void UpdateBrand(BrandEntity brand);
    void DeleteBrand(BrandEntity brand);
}
