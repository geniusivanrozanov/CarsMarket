﻿using FavoriteFilters.Domain.Enums;

namespace FavoriteFilters.Application.DTOs.Filter;

public class GetFilterDto
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
    
    public string Cron { get; set; } = null!;
}
