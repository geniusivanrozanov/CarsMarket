using CarsCatalog.Domain.Interfaces;

namespace CarsCatalog.Domain.Entities;

public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; } = default!;
}
