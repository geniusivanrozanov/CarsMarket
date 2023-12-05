using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IRepositoryBase<in TEntity, TKey> where TEntity : EntityBase<TKey>
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
