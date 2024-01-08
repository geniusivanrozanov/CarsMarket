using Chat.Domain.Entities;
using Chat.Infrastructure.Data.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Configurations;

public class ChatEntityConfiguration : IMongoCollectionConfiguration<ChatEntity>
{
    public void Configure(IMongoCollection<ChatEntity> collection)
    {
    }
}
