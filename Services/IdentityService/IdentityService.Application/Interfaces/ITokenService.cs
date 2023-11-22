using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(UserEntity userEntity, CancellationToken cancellationToken);
    Task<string> GenerateAndSaveRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);
    Task<Guid?> GetUserIdByRefreshToken(string token, CancellationToken cancellationToken);
}
