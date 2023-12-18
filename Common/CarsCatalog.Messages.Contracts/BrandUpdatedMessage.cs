namespace CarsCatalog.Messages.Contracts;

public class BrandUpdatedMessage
{
    public Guid BrandId { get; set; }
    public string UpdatedBrandName { get; set; } = null!;
}
