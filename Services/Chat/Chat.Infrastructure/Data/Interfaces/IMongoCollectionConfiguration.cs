using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Interfaces;

public interface IMongoCollectionConfiguration<TEntity>
{
    void Configure(IMongoCollection<TEntity> collection);
}
