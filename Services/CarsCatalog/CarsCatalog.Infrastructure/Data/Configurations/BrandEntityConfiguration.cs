using CarsCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsCatalog.Infrastructure.Data.Configurations;

public class BrandEntityConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    private const int NameMaxLength = 64;
    
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.Property(x => x.Name)
            .HasMaxLength(NameMaxLength)
            .IsRequired();
    }
}
