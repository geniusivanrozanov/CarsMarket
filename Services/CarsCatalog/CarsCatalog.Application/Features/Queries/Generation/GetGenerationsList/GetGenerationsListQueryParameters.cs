namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationsListQueryParameters
{
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public Guid? ModelId { get; set; }
    public string? ModelName { get; set; }
    public int? ProductionYear { get; set; }
}
