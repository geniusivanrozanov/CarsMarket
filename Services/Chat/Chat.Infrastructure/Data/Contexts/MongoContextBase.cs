using Chat.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Contexts;

public class MongoContextBase
{
    protected readonly IMongoDatabase Database;

    protected MongoContextBase(MongoClient client, IOptions<DatabaseOptions> options)
    {
        Database = client.GetDatabase(options.Value.DatabaseName);
    }
}
