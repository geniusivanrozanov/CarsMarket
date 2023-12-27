using Chat.Domain.Entities;

namespace Chat.Application.Interfaces.Repositories;

public interface IChatRepository
{
    Task<IEnumerable<TProjection>> GetChatsByMemberIdAsync<TProjection>(Guid memberId,
        CancellationToken cancellationToken = default);

    Task CreateChatAsync(ChatEntity entity, CancellationToken cancellationToken = default);
}
