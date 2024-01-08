using Chat.Domain.Entities;
using Chat.Infrastructure.Data.Configurations;
using Chat.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Contexts;

public class ChatContext : MongoContextBase
{
    private static bool _isCreated;

    public IMongoCollection<ChatEntity> Chats { get; }
    public IMongoCollection<MessageEntity> Messages { get; }

    public ChatContext(MongoClient client, IOptions<DatabaseOptions> options) : base(client, options)
    {
        Chats = Database.GetCollection<ChatEntity>(nameof(Chats));
        Messages = Database.GetCollection<MessageEntity>(nameof(Messages));

        if (!_isCreated)
        {
            OnConfiguring();
            _isCreated = true;
        }
    }

    private void OnConfiguring()
    {
        var chatEntityConfiguration = new ChatEntityConfiguration();
        var messageEntityConfiguration = new MessageEntityConfiguration();

        chatEntityConfiguration.Configure(Chats);
        messageEntityConfiguration.Configure(Messages);
    }
}
