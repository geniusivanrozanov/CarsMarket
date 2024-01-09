using FavoriteFilters.Domain.Enums;

namespace FavoriteFilters.Application.DTOs.Filter;

public class CreateFilterDto
{
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

    public int? CronMinute { get; set; }
    public int? CronHour { get; set; }
    public int? CronDayOfMonth { get; set; }
    public int? CronMonth { get; set; }
    public int? CronDayOfWeek { get; set; }
}
