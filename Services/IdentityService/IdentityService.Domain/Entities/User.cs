using IdentityService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity, IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}