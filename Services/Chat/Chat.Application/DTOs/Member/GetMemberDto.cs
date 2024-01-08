namespace Chat.Application.DTOs.Member;

public class GetMemberDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
