using System.Reflection;
using FavoriteFilters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FavoriteFilters.Infrastructure.Data.Contexts;

public class FiltersContext : DbContext
{
    public FiltersContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<FilterEntity> Filters { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
