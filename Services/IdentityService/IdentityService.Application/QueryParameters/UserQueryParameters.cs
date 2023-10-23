namespace IdentityService.Application.QueryParameters;

public record UserQueryParameters : QueryParametersBase
{
    public string? Status { get; init; }
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}