using IdentityService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity, ICreatedAtAuditable, IUpdatedAtAuditable
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
