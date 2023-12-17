namespace CarsCatalog.Domain.Entities;

public class GenerationEntity : EntityBase<Guid>
{
    public required string Name { get; set; }

    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public Guid ModelId { get; set; }
    public ModelEntity? Model { get; set; }
}
