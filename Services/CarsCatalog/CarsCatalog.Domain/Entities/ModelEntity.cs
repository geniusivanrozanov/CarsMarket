using CarsCatalog.Domain.Interfaces;

namespace CarsCatalog.Domain.Entities;

public class ModelEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public Guid BrandId { get; set; }
    public BrandEntity? Brand { get; set; }
    public ICollection<GenerationEntity>? Generations { get; set; }
}
