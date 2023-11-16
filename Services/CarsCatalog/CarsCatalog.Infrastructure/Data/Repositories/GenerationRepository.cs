using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class GenerationRepository(CatalogContext context) : IGenerationRepository
{
    public async Task<TProjection?> GetGenerationByIdAsync<TProjection>(Guid generationId, CancellationToken cancellationToken = default)
    {
        var query = context.Generations
            .Where(x => x.Id == generationId)
            .ProjectTo<TProjection>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetGenerationsAsync<TProjection>(CancellationToken cancellationToken = default)
    {
        var query = context.Generations
            .AsNoTracking()
            .ProjectTo<TProjection>();

        return await query.ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAndModelIdAsync(string name, Guid modelId, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name && x.ModelId == modelId);
    }

    public void CreateGeneration(GenerationEntity generation)
    {
        context.Generations
            .Add(generation);
    }

    public void UpdateGeneration(GenerationEntity generation)
    {
        context.Generations
            .Update(generation);
    }

    public void DeleteGeneration(GenerationEntity generation)
    {
        context.Generations
            .Remove(generation);
    }

    private async Task<bool> Exists(Expression<Func<GenerationEntity, bool>> predicate)
    {
        return await context.Generations
            .AnyAsync(predicate);
    }
}
