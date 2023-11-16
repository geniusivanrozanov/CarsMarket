using System.Reflection;
using CarsCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Contexts;

public class CatalogContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BrandEntity> Brands { get; set; } = default!;
    public DbSet<ModelEntity> Models { get; set; } = default!;
    public DbSet<GenerationEntity> Generations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
