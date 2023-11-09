namespace CarsCatalog.Application.DTOs;

public class UpdateBrandDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
}
