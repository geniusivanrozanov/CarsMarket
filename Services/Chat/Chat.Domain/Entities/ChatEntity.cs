namespace Chat.Domain.Entities;

public class ChatEntity
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }
    public MessageEntity LastMessage { get; set; } = null!;
    public ICollection<MemberEntity> Members { get; set; } = null!;
}
