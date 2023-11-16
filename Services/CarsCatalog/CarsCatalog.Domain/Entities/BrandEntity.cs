using CarsCatalog.Domain.Interfaces;

namespace CarsCatalog.Domain.Entities;

public class BrandEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<ModelEntity>? Models { get; set; }
}
