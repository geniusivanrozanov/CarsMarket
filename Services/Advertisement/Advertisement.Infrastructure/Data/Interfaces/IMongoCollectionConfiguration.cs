using Advertisement.Domain.Entities;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Interfaces;

public interface IMongoCollectionConfiguration<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    void Configure(IMongoCollection<TEntity> collection);
}
