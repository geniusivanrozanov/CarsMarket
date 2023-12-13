using System.Linq.Expressions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using Advertisement.Application.QueryParameters;
using Advertisement.Domain.Entities;
using Advertisement.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Advertisement.Infrastructure.Data.Repositories;

public class AdRepository : IAdRepository
{
    private readonly AdvertisementContext _context;

    public AdRepository(AdvertisementContext context)
    {
        _context = context;
    }

    public async Task<AdEntity?> GetAdByIdAsync(Guid adId, CancellationToken cancellationToken = default)
    {
        var projection = Builders<AdEntity>
            .Projection
            .Include(x => x.CurrentPrice);

        return await _context.Ads
            .Find(x => x.Id == adId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AdEntity>> GetAdsAsync(AdQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var filter = GenerateFilter(queryParameters);

        var findFluent = _context.Ads.Find(filter);

        if (queryParameters.OrderBy is not null)
        {
            Expression<Func<AdEntity, object>>? sortExpression = queryParameters.OrderBy.ToLower() switch
            {
                "year" => x => x.Year,
                "mileage" => x => x.Mileage,
                "price" => x => x.CurrentPrice.Value,
                _ => null
            };

            if (sortExpression is not null)
            {
                var sort = queryParameters.Desc != null && queryParameters.Desc.Value
                    ? Builders<AdEntity>.Sort.Descending(sortExpression)
                    : Builders<AdEntity>.Sort.Ascending(sortExpression);

                findFluent = findFluent.Sort(sort);
            }
        }

        if (queryParameters.Page is not null && queryParameters.PageSize is not null)
            findFluent = findFluent.Skip(queryParameters.Page.Value * queryParameters.PageSize.Value);

        if (queryParameters.PageSize is not null) findFluent = findFluent.Limit(queryParameters.PageSize);

        return await findFluent.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Ads
            .Find(x => x.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithVinAsync(string vin, CancellationToken cancellationToken = default)
    {
        return await _context.Ads
            .Find(x => x.Vin == vin)
            .AnyAsync(cancellationToken);
    }

    public async Task CreateAd(AdEntity entity)
    {
        await _context.Ads
            .InsertOneAsync(entity);
    }

    public async Task UpdateAd(AdEntity entity)
    {
        await _context.Ads
            .ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    private static FilterDefinition<AdEntity> GenerateFilter(AdQueryParameters queryParameters)
    {
        var builder = Builders<AdEntity>.Filter;
        var filter = builder.Empty;

        if (queryParameters.Description is not null) filter &= builder.Text(queryParameters.Description);

        if (queryParameters.BrandId is not null) filter &= builder.Eq(x => x.BrandId, queryParameters.BrandId);

        if (queryParameters.ModelId is not null) filter &= builder.Eq(x => x.ModelId, queryParameters.ModelId);

        if (queryParameters.GenerationId is not null)
            filter &= builder.Eq(x => x.GenerationId, queryParameters.GenerationId);

        if (queryParameters.MinMileage is not null) filter &= builder.Gte(x => x.Mileage, queryParameters.MinMileage);

        if (queryParameters.MaxMileage is not null) filter &= builder.Lte(x => x.Mileage, queryParameters.MaxMileage);

        if (queryParameters.MinYear is not null) filter &= builder.Gte(x => x.Year, queryParameters.MinYear);

        if (queryParameters.MaxYear is not null) filter &= builder.Lte(x => x.Year, queryParameters.MaxYear);

        if (queryParameters.Currency is not null)
        {
            var currencyFilter = builder.Eq(x => x.CurrentPrice.Currency, queryParameters.Currency);

            if (queryParameters.MinPrice is not null)
                filter &= currencyFilter & builder.Gte(x => x.CurrentPrice.Value, queryParameters.MinPrice);

            if (queryParameters.MaxPrice is not null)
                filter &= currencyFilter & builder.Lte(x => x.CurrentPrice.Value, queryParameters.MaxPrice);
        }

        return filter;
    }
}
