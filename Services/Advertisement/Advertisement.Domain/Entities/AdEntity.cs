using Advertisement.Domain.Enums;
using Advertisement.Domain.ValueObjects;

namespace Advertisement.Domain.Entities;

public class AdEntity : EntityBase<Guid>
{
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Vin { get; set; }
    public Guid BrandId { get; set; }
    public Guid ModelId { get; set; }
    public Guid GenerationId { get; set; }
    public string BrandName { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string GenerationName { get; set; } = null!;
    public int Year { get; set; }
    public int Mileage { get; set; }
    public AdStatus Status { get; set; }
    public ICollection<Price>? Prices { get; set; }
    public Price CurrentPrice { get; set; } = null!;
}
