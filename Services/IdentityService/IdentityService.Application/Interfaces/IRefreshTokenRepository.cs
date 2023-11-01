namespace IdentityService.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task SetRefreshTokenAsync(string token, Guid userId);
    Task<Guid?> GetUserIdByTokenAsync(string token);
    Task DeleteTokenAsync(string token);
}
