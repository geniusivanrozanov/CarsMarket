namespace Chat.Application.QueryParameters;

public abstract class QueryParametersBase
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}
