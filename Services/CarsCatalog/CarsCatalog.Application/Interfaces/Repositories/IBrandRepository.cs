using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<TProjection?> GetBrandByIdAsync<TProjection>(Guid brandId,
        Func<IQueryable<BrandEntity>, IQueryable<TProjection>> project,
        CancellationToken cancellationToken);
    
    Task<IEnumerable<TProjection>> GetBrandsAsync<TProjection>(
        Func<IQueryable<BrandEntity>, IQueryable<TProjection>> project,
        CancellationToken cancellationToken);

    void CreateBrand(BrandEntity brand);
    void UpdateBrand(BrandEntity brand);
    void DeleteBrand(BrandEntity brand);
}
