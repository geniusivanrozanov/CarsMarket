using Advertisement.Domain.Enums;
using MediatR;

namespace Advertisement.Application.DTOs.Ad;

public class GetAdDto : IRequest
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Vin { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string Generation { get; set; } = null!;
    public int Year { get; set; }
    public int Mileage { get; set; }
    public AdStatus Status { get; set; }
    public double Price { get; set; }
    public Currency Currency { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
