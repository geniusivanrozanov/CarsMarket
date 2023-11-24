namespace IdentityService.Application.DTOs;

public record LoginResultDto
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
