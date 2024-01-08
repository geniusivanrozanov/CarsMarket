using System.Linq.Expressions;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Domain.Entities;
using FavoriteFilters.Infrastructure.Data.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FavoriteFilters.Infrastructure.Data.Repositories;

public class FilterRepository : RepositoryBase<FilterEntity>, IFilterRepository
{
    public FilterRepository(FiltersContext context) : base(context)
    {
    }

    public async Task<TProjection?> GetFilterByIdAsync<TProjection>(Guid filterId, CancellationToken cancellationToken = default)
    {
        var query = Query
            .Where(entity => entity.Id == filterId)
            .ProjectToType<TProjection>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetFiltersAsync<TProjection>(CancellationToken cancellationToken = default)
    {
        var query = Query
            .AsNoTracking()
            .ProjectToType<TProjection>();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Exists(entity => entity.Id == id);
    }
    
    private async Task<bool> Exists(Expression<Func<FilterEntity, bool>> predicate)
    {
        return await Query
            .AnyAsync(predicate);
    }
}
