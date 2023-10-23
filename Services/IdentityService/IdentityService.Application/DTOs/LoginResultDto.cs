namespace IdentityService.Application.DTOs;

public record LoginResultDto
{
    public required string AccessToken { get; init; }
}