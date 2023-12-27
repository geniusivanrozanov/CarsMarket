namespace CarsCatalog.Application.DTOs;

public class UpdateGenerationDto
{
    public required string Name { get; set; }
    public int StartYear { get; set; }
    public int? EndYear { get; set; }
}
