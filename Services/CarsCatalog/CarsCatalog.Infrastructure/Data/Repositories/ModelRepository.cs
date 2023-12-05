using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class ModelRepository : RepositoryBase<ModelEntity, Guid>, IModelRepository
{
    public ModelRepository(CatalogContext context) : base(context)
    {
    }

    public async Task<TProjection?> GetModelByIdAsync<TProjection>(Guid modelId,
        CancellationToken cancellationToken = default)
    {
        var query = Query
            .Where(x => x.Id == modelId)
            .ProjectTo<IQueryable<TProjection>>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetModelsAsync<TProjection>(
        Guid? brandId = default,
        string? brandName = default,
        CancellationToken cancellationToken = default)
    {
        var query = Query
            .AsNoTracking();

        if (brandId.HasValue)
            query = query.Where(x => x.BrandId == brandId);
        else if (brandName is not null)
            query = query.Where(x => x.Brand!.Name.ToLower().Contains(brandName.ToLower()));

        return await query
            .ProjectTo<IQueryable<TProjection>>()
            .ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAndBrandIdAsync(string name, Guid brandId,
        CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name && x.BrandId == brandId);
    }

    private async Task<bool> Exists(Expression<Func<ModelEntity, bool>> predicate)
    {
        return await Query
            .AnyAsync(predicate);
    }
}
