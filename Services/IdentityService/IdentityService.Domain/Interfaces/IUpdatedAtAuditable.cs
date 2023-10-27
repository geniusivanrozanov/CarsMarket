namespace IdentityService.Domain.Interfaces;

public interface IUpdatedAtAuditable
{
    public DateTimeOffset UpdatedAt { get; set; }
}
