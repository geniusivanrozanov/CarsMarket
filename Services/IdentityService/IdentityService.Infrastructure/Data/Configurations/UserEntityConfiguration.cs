using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : EntityConfigurationBase<UserEntity>
{
    private const int FirstNameMaxLength = 64;
    private const int LastNameMaxLength = 64;
    
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(u => u.FirstName)
            .HasMaxLength(FirstNameMaxLength);

        builder.Property(u => u.LastName)
            .HasMaxLength(LastNameMaxLength);

        builder.Property(u => u.Email)
            .IsRequired();
    }
}