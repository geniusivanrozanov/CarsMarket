using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int FirstNameMaxLength = 64;
    private const int LastNameMaxLength = 64;
    
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(FirstNameMaxLength);

        builder.Property(u => u.LastName)
            .HasMaxLength(LastNameMaxLength);

        builder.Property(u => u.Email)
            .IsRequired();
    }
}