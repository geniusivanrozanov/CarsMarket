namespace IdentityService.Application.Options;

public class RefreshTokenOptions
{
    public required int RedisDatabaseNumber { get; set; }
    public required int ExpirationHours { get; set; }
}
