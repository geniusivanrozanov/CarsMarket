using Chat.Application.Interfaces.Repositories;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Chat.Infrastructure.Data.Contexts;
using Chat.Infrastructure.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Infrastructure.Data.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ChatContext _context;

    public ChatRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TProjection>> GetChatsByMemberIdAsync<TProjection>(Guid memberId, CancellationToken cancellationToken = default)
    {
        var query = _context.Chats.AsQueryable();

        return await query
            .Where(x => x.Members.Any(m => m.Id == memberId))
            .ProjectTo<TProjection>()
            .AsMongoQueryable()
            .ToListAsync(cancellationToken);
    }

    public async Task CreateChatAsync(ChatEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Chats
            .InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
    }

    public async Task UpdateLastMessageAsync(MessageEntity lastMessage, CancellationToken cancellationToken = default)
    {
        var update = Builders<ChatEntity>.Update
            .Set(x => x.LastMessage, lastMessage);

        await _context.Chats
            .UpdateOneAsync(x => x.Id == lastMessage.ChatId && x.LastMessage.CreatedAt < lastMessage.CreatedAt,
                update,
                cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Chats
            .Find(x => x.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithIdAndMemberIdAsync(Guid id, Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats
            .Find(x => x.Id == id && x.Members.Any(m => m.Id == memberId))
            .AnyAsync(cancellationToken);
    }
}
