using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(UserEntity userEntity);
    Task<string> GenerateAndSaveRefreshTokenAsync(Guid userId);
    Task<Guid?> GetUserIdByRefreshToken(string token);
}
