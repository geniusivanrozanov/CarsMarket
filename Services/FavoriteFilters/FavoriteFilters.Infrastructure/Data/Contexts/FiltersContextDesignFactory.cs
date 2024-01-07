using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FavoriteFilters.Infrastructure.Data.Contexts;

public class FiltersContextDesignFactory : IDesignTimeDbContextFactory<FiltersContext>
{
    public FiltersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FiltersContext>();
        optionsBuilder.UseNpgsql();

        return new FiltersContext(optionsBuilder.Options);
    }
}
