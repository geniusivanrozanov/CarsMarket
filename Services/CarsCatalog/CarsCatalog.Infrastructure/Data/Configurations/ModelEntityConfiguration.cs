using CarsCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsCatalog.Infrastructure.Data.Configurations;

public class ModelEntityConfiguration : IEntityTypeConfiguration<ModelEntity>
{
    private const int NameMaxLength = 64;
    
    public void Configure(EntityTypeBuilder<ModelEntity> builder)
    {
        builder.HasIndex(x => new { x.Name, x.BrandId })
            .IsUnique();
        
        builder.Property(x => x.Name)
            .HasMaxLength(NameMaxLength)
            .IsRequired();
    }
}
