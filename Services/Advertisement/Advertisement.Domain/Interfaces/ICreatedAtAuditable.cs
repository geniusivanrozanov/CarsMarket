namespace Advertisement.Domain.Interfaces;

public interface ICreatedAtAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
}
