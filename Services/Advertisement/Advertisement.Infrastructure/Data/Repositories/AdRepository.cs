using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
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

    public async Task<IEnumerable<AdEntity>> GetAdsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Ads
            .Find(_ => true)
            .ToListAsync(cancellationToken);
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
}
