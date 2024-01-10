using Chat.Application.QueryParameters;
using Chat.Domain.Entities;

namespace Chat.Application.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<TProjection>> GetMessagesByChatIdAsync<TProjection>(Guid chatId,
        MessageQueryParameters queryParameters,
        CancellationToken cancellationToken = default);

    Task CreateMessageAsync(MessageEntity entity, CancellationToken cancellationToken = default);
}
