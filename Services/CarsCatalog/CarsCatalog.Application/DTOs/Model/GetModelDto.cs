namespace CarsCatalog.Application.DTOs;

public class GetModelDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public Guid BrandId { get; set; }
}
