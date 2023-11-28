using Advertisement.Domain.Entities;
using Advertisement.Infrastructure.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Configurations;

public class AdEntityConfiguration : IMongoCollectionConfiguration<AdEntity, Guid>
{
    public void Configure(IMongoCollection<AdEntity> collection)
    {
        var vinIndex = new CreateIndexModel<AdEntity>(
            Builders<AdEntity>.IndexKeys.Ascending(x => x.Vin),
            new CreateIndexOptions<AdEntity>
            {
                Unique = true,
                PartialFilterExpression = Builders<AdEntity>.Filter.Type(x => x.Vin, BsonType.String)
            });
        
        var brandIdIndex = new CreateIndexModel<AdEntity>(Builders<AdEntity>.IndexKeys
            .Ascending(x => x.BrandId));
        
        var modelIdIndex = new CreateIndexModel<AdEntity>(Builders<AdEntity>.IndexKeys
            .Ascending(x => x.ModelId));
        
        var generationIdIndex = new CreateIndexModel<AdEntity>(Builders<AdEntity>.IndexKeys
            .Ascending(x => x.GenerationId));
        
        var descriptionIndex = new CreateIndexModel<AdEntity>(Builders<AdEntity>.IndexKeys
            .Text(x => x.Description));

        collection.Indexes.DropAll();
        
        collection
            .Indexes
            .CreateMany(new[] { vinIndex, brandIdIndex, modelIdIndex, generationIdIndex, descriptionIndex });
    }
}
