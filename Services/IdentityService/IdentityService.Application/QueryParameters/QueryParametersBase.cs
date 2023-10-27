using System.Linq.Expressions;

namespace IdentityService.Application.QueryParameters;

public abstract record QueryParametersBase<T>
    where T : class
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrderBy { get; set; }
    public bool? Desc { get; set; }

    public abstract Expression<Func<T, object>> GetOrderByExpression();
    public abstract IEnumerable<Expression<Func<T, bool>>> GetFilterExpressions();
}
