using Advertisement.Domain.Entities;
using Advertisement.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Contexts;

public class AdvertisementContext
{
    public AdvertisementContext(MongoClient client, IOptions<DatabaseOptions> options)
    {
        var database = client.GetDatabase(options.Value.DatabaseName);

        Ads = database.GetCollection<AdEntity>(nameof(Ads));
    }
    
    public IMongoCollection<AdEntity> Ads { get; }
}
