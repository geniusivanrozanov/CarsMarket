namespace IdentityService.WebAPI.Options;

public class DistributedCacheOptions
{
    public required int RedisDatabaseNumber { get; set; }
    public required int ExpirationMinutes { get; set; }
}
