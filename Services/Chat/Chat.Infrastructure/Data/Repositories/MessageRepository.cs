using Chat.Application.Interfaces.Repositories;
using Chat.Application.Mappers;
using Chat.Application.QueryParameters;
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

    public async Task<IEnumerable<TProjection>> GetMessagesByChatIdAsync<TProjection>(Guid chatId,
        MessageQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Messages.AsQueryable();

        query = query.Where(x => x.ChatId == chatId);
        
        if (queryParameters.Page is not null && queryParameters.PageSize is not null)
        {
            query = query.Skip(queryParameters.Page.Value * queryParameters.PageSize.Value);
        }
        
        if (queryParameters.PageSize is not null)
        {
            query = query.Take(queryParameters.PageSize.Value);
        }

        return await query
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
