using System.Linq.Expressions;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Interfaces;

namespace IdentityService.Infrastructure.Data.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
    IQueryable<TEntity> Get(QueryParametersBase<TEntity> queryParameters);
}