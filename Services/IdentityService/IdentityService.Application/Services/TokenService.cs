using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Options;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Application.Services;

public class TokenService(
        IOptions<JwtOptions> tokenOptions,
        UserManager<UserEntity> userManager)
    : ITokenService
{
    public async Task<string> GenerateAccessTokenAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var key = Encoding.UTF8.GetBytes(tokenOptions.Value.Key);
        var securityKey = new SymmetricSecurityKey(key);

        var rolesList = await userManager.GetRolesAsync(userEntity);
        var roles = string.Join(", ", rolesList);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
                new Claim(ClaimTypes.Role, roles),
                new Claim(ClaimTypes.Email, userEntity.Email!)
            }),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddMinutes(tokenOptions.Value.ExpirationMinutes),
            Issuer = tokenOptions.Value.Issuer,
            Audience = tokenOptions.Value.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
