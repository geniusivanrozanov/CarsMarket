using Advertisement.Domain.Entities;

namespace Advertisement.Application.QueryParameters;

public abstract class QueryParametersBase<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrderBy { get; set; }
    public bool? Desc { get; set; }
}
