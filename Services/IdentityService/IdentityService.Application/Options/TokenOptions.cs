namespace IdentityService.Application.Options;

public record TokenOptions
{
    public required string Key { get; init; }
    public required int ExpirationMinutes { get; init; }
}