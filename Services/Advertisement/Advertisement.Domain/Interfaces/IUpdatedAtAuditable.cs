namespace Advertisement.Domain.Interfaces;

public interface IUpdatedAtAuditable
{
    public DateTimeOffset UpdatedAt { get; set; }
}