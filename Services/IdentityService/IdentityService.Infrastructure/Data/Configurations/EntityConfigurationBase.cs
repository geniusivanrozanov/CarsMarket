using IdentityService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Data.Configurations;

public abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        if (typeof(IAuditable).IsAssignableFrom(typeof(TEntity)))
        {
            ConfigureAuditableEntity(builder);
        }
    }

    protected virtual void ConfigureAuditableEntity(EntityTypeBuilder<TEntity> builder)
    {
    }
}