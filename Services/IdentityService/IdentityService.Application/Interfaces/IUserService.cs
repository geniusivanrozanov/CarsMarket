using IdentityService.Application.DTOs;
using IdentityService.Application.QueryParameters;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(RegisterDto register, CancellationToken cancellationToken);
    Task<UserDto> RegisterModeratorAsync(RegisterDto register, CancellationToken cancellationToken);
    Task<LoginResultDto> LoginUserAsync(LoginDto login, CancellationToken cancellationToken);
    Task<LoginResultDto> LoginUserByRefreshTokenAsync(RefreshTokenDto refreshToken, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetUsersAsync(UserQueryParameters userQueryParameters, CancellationToken cancellationToken);
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}
