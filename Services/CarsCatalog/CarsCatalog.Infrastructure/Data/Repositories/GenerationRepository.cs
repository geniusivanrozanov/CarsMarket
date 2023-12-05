using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class GenerationRepository : IGenerationRepository
{
    private readonly CatalogContext _context;

    public GenerationRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<TProjection?> GetGenerationByIdAsync<TProjection>(Guid generationId,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Generations
            .Where(x => x.Id == generationId)
            .ProjectTo<IQueryable<TProjection>>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetGenerationsAsync<TProjection>(
        Guid? brandId = default,
        string? brandName = default,
        Guid? modelId = default,
        string? modelName = default,
        int? productionYear = default,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Generations
            .AsNoTracking();

        if (modelId.HasValue)
            query = query.Where(x => x.ModelId == modelId);
        else if (modelName is not null)
            query = query.Where(x => x.Model!.Name.Contains(modelName.ToLower()));

        if (brandId.HasValue)
            query = query.Where(x => x.Model!.BrandId == brandId);
        else if (brandName is not null)
            query = query.Where(x => x.Model!.Brand!.Name.Contains(brandName.ToLower()));

        if (productionYear.HasValue)
            query = query.Where(x =>
                productionYear >= x.StartYear && productionYear <= (x.EndYear ?? DateTimeOffset.UtcNow.Year));

        return await query
            .ProjectTo<IQueryable<TProjection>>()
            .ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAndModelIdAsync(string name, Guid modelId,
        CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name && x.ModelId == modelId);
    }

    public void CreateGeneration(GenerationEntity generation)
    {
        _context.Generations
            .Add(generation);
    }

    public void UpdateGeneration(GenerationEntity generation)
    {
        _context.Generations
            .Update(generation);
    }

    public void DeleteGeneration(GenerationEntity generation)
    {
        _context.Generations
            .Remove(generation);
    }

    private async Task<bool> Exists(Expression<Func<GenerationEntity, bool>> predicate)
    {
        return await _context.Generations
            .AnyAsync(predicate);
    }
}
