using Advertisement.Domain.Entities;
using Advertisement.Infrastructure.Data.Interfaces;
using MongoDB.Bson.Serialization;

namespace Advertisement.Infrastructure.Data.Configurations;

public class AdEntityConfiguration : IBsonConfiguration<AdEntity, Guid>
{
    public void Configure()
    {
        BsonClassMap.RegisterClassMap<AdEntity>(map =>
        {
            map.MapIdField(x => x.Id);
        });
    }
}
