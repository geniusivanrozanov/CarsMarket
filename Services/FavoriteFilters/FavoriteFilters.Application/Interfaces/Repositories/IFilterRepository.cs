using FavoriteFilters.Domain.Entities;

namespace FavoriteFilters.Application.Interfaces.Repositories;

public interface IFilterRepository : IRepositoryBase<FilterEntity>
{
    Task<TProjection?> GetFilterByIdAsync<TProjection>(Guid filterId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TProjection>> GetFiltersAsync<TProjection>(CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
}
