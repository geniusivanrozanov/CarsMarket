using Chat.Application.DTOs.Message;
using Chat.Application.QueryParameters;

namespace Chat.Application.Interfaces.Services;

public interface IMessageService
{
    Task<IEnumerable<GetMessageDto>> GetMessagesByChatIdAsync(Guid chatId,
        MessageQueryParameters messageQueryParameters,
        CancellationToken cancellationToken = default);

    Task<GetMessageDto> SendMessageAsync(SendMessageDto sendMessageDto, CancellationToken cancellationToken = default);
}
