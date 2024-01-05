using Chat.Application.DTOs.Message;

namespace Chat.Application.Interfaces.Services;

public interface IMessageService
{
    Task<IEnumerable<GetMessageDto>> GetMessagesByChatIdAsync(Guid chatId,
        CancellationToken cancellationToken = default);

    Task<GetMessageDto> SendMessageAsync(SendMessageDto sendMessageDto, CancellationToken cancellationToken = default);
}
