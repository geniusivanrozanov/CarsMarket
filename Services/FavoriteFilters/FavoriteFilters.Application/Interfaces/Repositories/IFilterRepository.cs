using FavoriteFilters.Domain.Entities;

namespace FavoriteFilters.Application.Interfaces.Repositories;

public interface IFilterRepository : IRepositoryBase<FilterEntity>
{
    Task<TProjection?> GetFilterByIdAsync<TProjection>(Guid filterId,
        CancellationToken cancellationToken = default);
    
    Task<TProjection?> GetFilterByIdAndUserIdAsync<TProjection>(Guid filterId,
        Guid userId,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TProjection>> GetFiltersByUserIdAsync<TProjection>(Guid userId,
        CancellationToken cancellationToken = default);
    
    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
}
