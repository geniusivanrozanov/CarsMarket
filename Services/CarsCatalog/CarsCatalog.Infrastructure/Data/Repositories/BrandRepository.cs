using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class BrandRepository : RepositoryBase<BrandEntity, Guid>, IBrandRepository
{
    public BrandRepository(CatalogContext context) : base(context)
    {
    }

    public async Task<TProjection?> GetBrandByIdAsync<TProjection>(Guid brandId,
        CancellationToken cancellationToken = default)
    {
        var query = Query
            .Where(x => x.Id == brandId)
            .ProjectTo<TProjection>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetBrandsAsync<TProjection>(
        CancellationToken cancellationToken = default)
    {
        var query = Query
            .AsNoTracking()
            .ProjectTo<TProjection>();

        return await query.ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name);
    }

    private async Task<bool> Exists(Expression<Func<BrandEntity, bool>> predicate)
    {
        return await Query
            .AnyAsync(predicate);
    }
}
