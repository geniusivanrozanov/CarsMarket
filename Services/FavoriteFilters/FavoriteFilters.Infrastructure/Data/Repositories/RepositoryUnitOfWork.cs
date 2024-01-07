using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Infrastructure.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteFilters.Infrastructure.Data.Repositories;

public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly FiltersContext _context;
    private readonly IServiceProvider _serviceProvider;

    public RepositoryUnitOfWork(FiltersContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IFilterRepository Filters => _serviceProvider.GetRequiredService<FilterRepository>();
    
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
