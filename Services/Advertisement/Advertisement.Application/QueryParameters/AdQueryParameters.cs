using Advertisement.Domain.Entities;
using Advertisement.Domain.Enums;

namespace Advertisement.Application.QueryParameters;

public class AdQueryParameters : QueryParametersBase<AdEntity, Guid>
{
    public string? Description { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? ModelId { get; set; }
    public Guid? GenerationId { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinMileage { get; set; }
    public int? MaxMileage { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public Currency? Currency { get; set; }
    public DateTimeOffset? MinCreatedAt { get; set; }
    public DateTimeOffset? MaxCreatedAt { get; set; }
}
