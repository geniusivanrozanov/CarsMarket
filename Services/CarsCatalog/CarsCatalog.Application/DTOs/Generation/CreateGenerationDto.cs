namespace CarsCatalog.Application.DTOs;

public class CreateGenerationDto
{
    public required string Name { get; set; }
    public int StartYear { get; set; }
    public int? EndYear { get; set; }
    
    public Guid ModelId { get; set; }
}
