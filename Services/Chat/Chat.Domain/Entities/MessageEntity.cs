namespace Chat.Domain.Entities;

public class MessageEntity
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
    public string Text { get; set; } = null!;
}
