using Advertisement.Domain.Entities;
using Advertisement.Infrastructure.Data.Configurations;
using Advertisement.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Contexts;

public class AdvertisementContext : MongoContextBase
{
    private static bool _isCreated;

    public AdvertisementContext(MongoClient client, IOptions<DatabaseOptions> options) : base(client, options)
    {
        Ads = Database.GetCollection<AdEntity>(nameof(Ads));

        if (!_isCreated)
        {
            OnConfiguring();
            _isCreated = true;
        }
    }

    public IMongoCollection<AdEntity> Ads { get; }

    private void OnConfiguring()
    {
        var adEntityConfiguration = new AdEntityConfiguration();
        adEntityConfiguration.Configure(Ads);
    }
}
