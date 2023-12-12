namespace IdentityService.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TProjection> ApplyProjector<TProjection, TEntity>(this IQueryable<TEntity> query,
        Func<IQueryable<TEntity>, IQueryable<TProjection>> projector)
    {
        return projector(query);
    }
}
