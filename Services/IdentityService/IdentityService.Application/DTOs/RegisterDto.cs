namespace IdentityService.Application.DTOs;

public record RegisterDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
