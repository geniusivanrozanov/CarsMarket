using Chat.Application.Interfaces.Repositories;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Chat.Infrastructure.Data.Contexts;
using Chat.Infrastructure.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Infrastructure.Data.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public MessageRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TProjection>> GetMessagesByChatIdAsync<TProjection>(Guid chatId, CancellationToken cancellationToken = default)
    {
        var query = _context.Messages.AsQueryable();
        
        return await query
            .Where(x => x.ChatId == chatId)
            .ProjectTo<TProjection>()
            .AsMongoQueryable()
            .ToListAsync(cancellationToken);
    }

    public async Task CreateMessageAsync(MessageEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Messages
            .InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
    }
}
