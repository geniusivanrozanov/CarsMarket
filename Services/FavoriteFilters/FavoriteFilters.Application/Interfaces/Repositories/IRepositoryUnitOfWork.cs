namespace FavoriteFilters.Application.Interfaces.Repositories;

public interface IRepositoryUnitOfWork
{
    IFilterRepository Filters { get; }
    
    Task SaveAsync(CancellationToken cancellationToken);
}
