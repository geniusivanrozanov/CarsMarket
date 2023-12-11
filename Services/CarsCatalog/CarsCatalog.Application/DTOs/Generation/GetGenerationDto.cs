namespace CarsCatalog.Application.DTOs;

public class GetGenerationDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int StartYear { get; set; }
    public int? EndYear { get; set; }

    public Guid ModelId { get; set; }
}
