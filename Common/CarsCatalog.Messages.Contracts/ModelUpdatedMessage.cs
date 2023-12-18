namespace CarsCatalog.Messages.Contracts;

public class ModelUpdatedMessage
{
    public Guid ModelId { get; set; }
    public string UpdatedModelName { get; set; } = null!;
}
