using Advertisement.Application.QueryParameters;
using Advertisement.Domain.Entities;

namespace Advertisement.Application.Interfaces.Repositories;

public interface IAdRepository
{
    Task<AdEntity?> GetAdByIdAsync(Guid adId, CancellationToken cancellationToken = default);

    Task<IEnumerable<AdEntity>> GetAdsAsync(AdQueryParameters queryParameters,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithVinAsync(string vin, CancellationToken cancellationToken = default);

    Task CreateAdAsync(AdEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAdAsync(AdEntity entity, CancellationToken cancellationToken = default);
}
