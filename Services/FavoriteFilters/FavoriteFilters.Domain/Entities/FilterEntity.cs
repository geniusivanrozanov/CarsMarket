using FavoriteFilters.Domain.Enums;
using FavoriteFilters.Domain.ValueObjects;

namespace FavoriteFilters.Domain.Entities;

public class FilterEntity
{
    public Guid Id { get; set; }
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
    
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = null!;

    public Cron Cron { get; set; } = null!;
}
