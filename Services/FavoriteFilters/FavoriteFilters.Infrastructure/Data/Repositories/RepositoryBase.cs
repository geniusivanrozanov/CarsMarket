using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FavoriteFilters.Infrastructure.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly FiltersContext Context;
    
    protected RepositoryBase(FiltersContext context)
    {
        Context = context;
    }

    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected IQueryable<TEntity> Query => Set.AsQueryable();

    public void Create(TEntity entity)
    {
        Set.Add(entity);
    }

    public void Update(TEntity entity)
    {
        Set.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Set.Remove(entity);
    }
}
