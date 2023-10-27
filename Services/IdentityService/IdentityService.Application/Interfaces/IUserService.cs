using IdentityService.Application.DTOs;
using IdentityService.Application.QueryParameters;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(RegisterDto register);
    Task<UserDto> RegisterModeratorAsync(RegisterDto register);

    Task<LoginResultDto> LoginUserAsync(LoginDto login);
    Task<IEnumerable<UserDto>> GetUsersAsync(UserQueryParameters userQueryParameters);
    Task<UserDto> GetUserByIdAsync(Guid userId);
}