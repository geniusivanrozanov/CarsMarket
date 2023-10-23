using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(User user);
}