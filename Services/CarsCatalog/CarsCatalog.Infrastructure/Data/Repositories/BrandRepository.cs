﻿using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalog.Infrastructure.Data.Repositories;

public class BrandRepository(CatalogContext context) : IBrandRepository
{
    public async Task<TProjection?> GetBrandByIdAsync<TProjection>(Guid brandId, CancellationToken cancellationToken = default)
    {
        var query = context.Brands
            .Where(x => x.Id == brandId)
            .ProjectTo<TProjection>();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> GetBrandsAsync<TProjection>(CancellationToken cancellationToken = default)
    {
        var query = context.Brands
            .AsNoTracking()
            .ProjectTo<TProjection>();

        return await query.ToArrayAsync(cancellationToken);
    }

    public void CreateBrand(BrandEntity brand)
    {
        context.Brands
            .Add(brand);
    }

    public void UpdateBrand(BrandEntity brand)
    {
        context.Brands
            .Update(brand);
    }

    public void DeleteBrand(BrandEntity brand)
    {
        context.Brands
            .Remove(brand);
    }
}