using CarsCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsCatalog.Infrastructure.Data.Configurations;

public class GenerationEntityConfiguration : IEntityTypeConfiguration<GenerationEntity>
{
    private const int NameMaxLength = 64;
    
    public void Configure(EntityTypeBuilder<GenerationEntity> builder)
    {
        builder.HasIndex(x => new { x.Name, x.ModelId})
            .IsUnique();
        
        builder.Property(x => x.Name)
            .HasMaxLength(NameMaxLength)
            .IsRequired();

        builder.Property(x => x.StartYear)
            .HasAnnotation("Range", new[] { DateTimeOffset.MinValue.Year, DateTimeOffset.MaxValue.Year });
        
        builder.Property(x => x.EndYear)
            .HasAnnotation("Range", new[] { DateTimeOffset.MinValue.Year, DateTimeOffset.MaxValue.Year });
    }
}
