namespace Chat.Domain.Entities;

public class ChatEntity
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }
    public ICollection<MemberEntity> Members { get; set; } = null!;
}
