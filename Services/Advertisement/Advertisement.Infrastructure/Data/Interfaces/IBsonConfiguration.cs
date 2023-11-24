using Advertisement.Domain.Entities;

namespace Advertisement.Infrastructure.Data.Interfaces;

public interface IBsonConfiguration<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    void Configure();
}
