using Chat.Application.DTOs.Member;

namespace Chat.Application.DTOs.Chat;

public class GetChatDto
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }
    public ICollection<GetMemberDto> Members { get; set; } = null!;
}
