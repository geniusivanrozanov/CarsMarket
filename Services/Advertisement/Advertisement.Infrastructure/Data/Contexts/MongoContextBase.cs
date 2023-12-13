using Advertisement.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Contexts;

public abstract class MongoContextBase
{
    protected readonly IMongoDatabase Database;

    protected MongoContextBase(MongoClient client, IOptions<DatabaseOptions> options)
    {
        Database = client.GetDatabase(options.Value.DatabaseName);
    }
}
