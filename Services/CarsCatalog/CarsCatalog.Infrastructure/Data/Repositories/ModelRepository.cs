using System.Linq.Expressions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly CatalogContext _context;

    public ModelRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<TProjection?> GetModelByIdAsync<TProjection>(Guid modelId,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Models
            .Where(x => x.Id == modelId)
            .ProjectTo<IQueryable<TProjection>>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetModelsAsync<TProjection>(
        Guid? brandId = default,
        string? brandName = default,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Models
            .AsNoTracking();

        if (brandId.HasValue)
            query = query.Where(x => x.BrandId == brandId);
        else if (brandName is not null)
            query = query.Where(x => x.Brand!.Name.ToLower().Contains(brandName.ToLower()));

        return await query
            .ProjectTo<IQueryable<TProjection>>()
            .ToArrayAsync(cancellationToken);
    }

    public Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Id == id);
    }

    public Task<bool> ExistsWithNameAndBrandIdAsync(string name, Guid brandId,
        CancellationToken cancellationToken = default)
    {
        return Exists(x => x.Name == name && x.BrandId == brandId);
    }

    public void CreateModel(ModelEntity model)
    {
        _context.Models
            .Add(model);
    }

    public void UpdateModel(ModelEntity model)
    {
        _context.Models
            .Update(model);
    }

    public void DeleteModel(ModelEntity model)
    {
        _context.Models
            .Remove(model);
    }

    private async Task<bool> Exists(Expression<Func<ModelEntity, bool>> predicate)
    {
        return await _context.Models
            .AnyAsync(predicate);
    }
}
