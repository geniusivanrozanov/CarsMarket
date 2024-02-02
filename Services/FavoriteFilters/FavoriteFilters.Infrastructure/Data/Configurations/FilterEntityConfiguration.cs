using FavoriteFilters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteFilters.Infrastructure.Data.Configurations;

public class FilterEntityConfiguration : IEntityTypeConfiguration<FilterEntity>
{
    public void Configure(EntityTypeBuilder<FilterEntity> builder)
    {
        builder
            .Property(x => x.UserId)
            .IsRequired();
        
        builder
            .Property(x => x.UserEmail)
            .IsRequired();

        builder
            .HasIndex(x => x.UserId);

        builder
            .OwnsOne(x => x.Cron);
    }
}
