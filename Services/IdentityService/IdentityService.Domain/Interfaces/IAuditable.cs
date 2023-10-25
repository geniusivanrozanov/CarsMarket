namespace IdentityService.Domain.Interfaces;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset LastModifiedAt { get; set; }
}