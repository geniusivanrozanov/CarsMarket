namespace CarsCatalog.Domain.Entities;

public class ModelEntity : EntityBase<Guid>
{
    public required string Name { get; set; }

    public Guid BrandId { get; set; }
    public BrandEntity? Brand { get; set; }
    public ICollection<GenerationEntity>? Generations { get; set; }
}
