using IdentityService.Application.Interfaces;
using IdentityService.Application.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace IdentityService.Infrastructure.Data.Repositories;

public class RefreshTokenRepository(
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<RefreshTokenOptions> refreshTokenOptions)
    : IRefreshTokenRepository
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase(refreshTokenOptions.Value.RedisDatabaseNumber);

    public async Task SetRefreshTokenAsync(string token, Guid userId)
    {
        await _database.StringSetAsync(
            token, 
            userId.ToString(), 
            TimeSpan.FromHours(refreshTokenOptions.Value.ExpirationHours));
    }

    public async Task<Guid?> GetUserIdByTokenAsync(string token)
    {
        var userId = await _database.StringGetAsync(token);
        if (userId.IsNull)
        {
            return null;
        }

        return Guid.Parse(userId.ToString());
    }

    public async Task DeleteTokenAsync(string token)
    {
        await _database.KeyDeleteAsync(token);
    }
}
