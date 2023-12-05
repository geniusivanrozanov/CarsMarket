using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    protected readonly CatalogContext Context;
    
    protected RepositoryBase(CatalogContext context)
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
