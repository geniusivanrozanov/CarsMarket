using System.Linq.Expressions;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Interfaces;
using IdentityService.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Data.Repositories;

public abstract class GenericRepository<TEntity>(DbContext context) : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();

    public IQueryable<TEntity> Get()
    {
        return _set.AsQueryable();
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
    {
        return _set.Where(filter);
    }

    public IQueryable<TEntity> Get(QueryParametersBase<TEntity> queryParameters)
    {
        var query = _set.AsQueryable();

        query = queryParameters.GetFilterExpressions()
            .Aggregate(query, (current, filterExpression) => current.Where(filterExpression));

        if (queryParameters.OrderBy is not null)
        {
            var orderByExpression = queryParameters.GetOrderByExpression();
            query = queryParameters.Desc is true
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        if (queryParameters.Page is not null && queryParameters.PageSize is not null)
            query = query.Skip(queryParameters.Page.Value * queryParameters.PageSize.Value);

        if (queryParameters.PageSize is not null) query = query.Take(queryParameters.PageSize.Value);

        return query;
    }
}