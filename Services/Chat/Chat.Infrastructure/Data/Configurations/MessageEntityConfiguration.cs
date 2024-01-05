using Chat.Domain.Entities;
using Chat.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Configurations;

public class MessageEntityConfiguration : IMongoCollectionConfiguration<MessageEntity>
{
    public void Configure(IMongoCollection<MessageEntity> collection)
    {
        var createdAtIndex = new CreateIndexModel<MessageEntity>(Builders<MessageEntity>.IndexKeys
            .Descending(x => x.CreatedAt));

        var chatIdIndex = new CreateIndexModel<MessageEntity>(Builders<MessageEntity>.IndexKeys
            .Ascending(x => x.ChatId));

        var textIndex = new CreateIndexModel<MessageEntity>(Builders<MessageEntity>.IndexKeys
            .Descending(x => x.Text));

        collection.Indexes.DropAll();

        collection
            .Indexes
            .CreateMany(new[] { createdAtIndex, chatIdIndex, textIndex });
    }
}
