using Advertisement.Domain.Interfaces;

namespace Advertisement.Domain.Entities;

public abstract class EntityBase<TKey> : IEntity<TKey>, ICreatedAtAuditable, IUpdatedAtAuditable
{
    public TKey Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
