namespace FavoriteFilters.Application.Interfaces.Repositories;

public interface IRepositoryBase<in TEntity> where TEntity : class
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
