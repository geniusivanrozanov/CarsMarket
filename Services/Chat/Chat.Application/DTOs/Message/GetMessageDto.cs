namespace Chat.Application.DTOs.Message;

public class GetMessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
