using Advertisement.Domain.Enums;

namespace Advertisement.Application.DTOs.Ad;

public class CreateAdDto
{
    public string? Description { get; set; }
    public string? Vin { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public Guid BrandId { get; set; }
    public Guid ModelId { get; set; }
    public Guid GenerationId { get; set; }
    public double Price { get; set; }
    public Currency Currency { get; set; }
}
