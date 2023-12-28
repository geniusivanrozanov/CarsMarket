using Chat.Application.DTOs.Chat;

namespace Chat.Application.Interfaces.Services;

public interface IChatService
{
    Task<IEnumerable<GetChatDto>> GetChatsAsync(CancellationToken cancellationToken = default);
    
    Task<GetChatDto> CreateChatAsync(CreateChatDto createChatDto, CancellationToken cancellationToken = default);
}
