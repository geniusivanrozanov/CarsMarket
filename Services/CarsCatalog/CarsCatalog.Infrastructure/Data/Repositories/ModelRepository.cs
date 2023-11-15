using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class ModelRepository(CatalogContext context) : IModelRepository
{
    public async Task<TProjection?> GetModelByIdAsync<TProjection>(Guid modelId, CancellationToken cancellationToken = default)
    {
        var query = context.Models
            .Where(x => x.Id == modelId)
            .ProjectTo<TProjection>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetModelsAsync<TProjection>(CancellationToken cancellationToken = default)
    {
        var query = context.Models
            .AsNoTracking()
            .ProjectTo<TProjection>();

        return await query.ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAndBrandIdAsync(string name, Guid brandId, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name && x.BrandId == brandId);
    }

    public void CreateModel(ModelEntity model)
    {
        context.Models
            .Add(model);
    }

    public void UpdateModel(ModelEntity model)
    {
        context.Models
            .Update(model);
    }

    public void DeleteModel(ModelEntity model)
    {
        context.Models
            .Remove(model);
    }

    private async Task<bool> Exists(Expression<Func<ModelEntity, bool>> predicate)
    {
        return await context.Models
            .AnyAsync(predicate);
    }
}
