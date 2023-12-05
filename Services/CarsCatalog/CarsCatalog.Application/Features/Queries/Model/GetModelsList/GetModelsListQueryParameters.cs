namespace CarsCatalog.Application.Features.Queries;

public class GetModelsListQueryParameters
{
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
}
