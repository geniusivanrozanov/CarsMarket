using Advertisement.Domain.Enums;

namespace Advertisement.Application.DTOs.Price;

public class GetPriceDto
{
    public double Value { get; set; }
    public Currency Currency { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
