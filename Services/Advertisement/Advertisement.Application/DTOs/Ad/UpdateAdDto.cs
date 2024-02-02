using Advertisement.Domain.Enums;

namespace Advertisement.Application.DTOs.Ad;

public class UpdateAdDto
{
    public string? Description { get; set; }
    public string? Vin { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public double Price { get; set; }
    public Currency Currency { get; set; }
}
