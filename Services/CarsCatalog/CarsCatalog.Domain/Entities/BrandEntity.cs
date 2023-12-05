namespace CarsCatalog.Domain.Entities;

public class BrandEntity : EntityBase<Guid>
{
    public required string Name { get; set; }

    public ICollection<ModelEntity>? Models { get; set; }
}
