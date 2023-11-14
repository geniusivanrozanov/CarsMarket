namespace CarsCatalog.Application.DTOs;

public class CreateModelDto
{
    public required string Name { get; set; }
    
    public Guid BrandId { get; set; }
}
