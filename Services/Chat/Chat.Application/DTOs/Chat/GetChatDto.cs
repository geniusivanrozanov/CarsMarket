using Chat.Application.DTOs.Member;
using Chat.Application.DTOs.Message;

namespace Chat.Application.DTOs.Chat;

public class GetChatDto
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }
    public GetMessageDto? LastMessage { get; set; }
    public ICollection<GetMemberDto> Members { get; set; } = null!;
}
