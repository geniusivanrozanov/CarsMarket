namespace IdentityService.Application.Options;

public record JwtOptions
{
    public required string Key { get; init; }
    public required int ExpirationMinutes { get; init; }
}