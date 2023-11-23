using Advertisement.Domain.Enums;
using Advertisement.Domain.Interfaces;
using Advertisement.Domain.ValueObjects;

namespace Advertisement.Domain.Entities;

public class AdEntity : EntityBase<Guid>, ICreatedAtAuditable, IUpdatedAtAuditable
{
    public Guid OwnerId { get; set; }
    public string? Description { get; set; }
    public string? Vin { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int GenerationId { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public AdStatus Status { get; set; }
    public ICollection<Price>? Prices { get; set; }
    public Price CurrentPrice { get; set; } = null!;
}
