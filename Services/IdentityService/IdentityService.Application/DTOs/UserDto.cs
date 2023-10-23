namespace IdentityService.Application.DTOs;

public record UserDto
{
    public required string Email { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}