namespace IdentityService.Application.QueryParameters;

public abstract record QueryParametersBase
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; }
}