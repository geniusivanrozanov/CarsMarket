using IdentityService.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Data.Configurations;

public class IdentityRolesConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        var roles = Roles
            .GetAllRoles()
            .Select(r => new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = r,
                NormalizedName = r.ToUpperInvariant()
            });

        builder.HasData(roles);
    }
}
