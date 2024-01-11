namespace Chat.Application.DTOs.Message;

public class SendMessageDto
{
    public Guid ChatId { get; set; }
    public string Text { get; set; } = null!;
}
