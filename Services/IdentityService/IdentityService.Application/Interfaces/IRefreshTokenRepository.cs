namespace IdentityService.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task SetRefreshTokenAsync(string token, Guid userId, CancellationToken cancellationToken);
    Task<Guid?> GetUserIdByTokenAsync(string token, CancellationToken cancellationToken);
}
