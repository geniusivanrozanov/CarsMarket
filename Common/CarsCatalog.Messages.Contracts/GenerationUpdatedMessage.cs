namespace CarsCatalog.Messages.Contracts;

public class GenerationUpdatedMessage
{
    public Guid GenerationId { get; set; }
    public string UpdatedGenerationName { get; set; } = null!;
}
