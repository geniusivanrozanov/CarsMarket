using MongoDB.Driver.Linq;

namespace Chat.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IMongoQueryable<TProjection> AsMongoQueryable<TProjection>(this IQueryable<TProjection> queryable)
    {
        return queryable as IMongoQueryable<TProjection> ?? throw new InvalidOperationException();
    }
}
