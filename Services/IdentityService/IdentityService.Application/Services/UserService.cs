using IdentityService.Application.DTOs;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Constants;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application.Services;

public class UserService(
        ILogger<UserService> logger,
        ITokenService tokenService,
        IUserRepository userRepository,
        IMapper mapper,
        UserManager<UserEntity> userManager)
    : IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterDto register)
    {
        return RegisterUserAsync(register, Roles.User);
    }

    public Task<UserDto> RegisterModeratorAsync(RegisterDto register)
    {
        return RegisterUserAsync(register, Roles.Moderator);
    }

    public async Task<LoginResultDto> LoginUserAsync(LoginDto login)
    {
        var userEntity = await userManager.FindByEmailAsync(login.Email);
        if (userEntity is null || !await userManager.CheckPasswordAsync(userEntity, login.Password))
        {
            logger.LogInformation("User with email {Email} failed to login", login.Email);
            throw new LoginFailedException("Invalid username or password");
        }

        var accessToken = await tokenService.GenerateAccessTokenAsync(userEntity);
        var loginResultDto = new LoginResultDto
        {
            AccessToken = accessToken
        };

        return loginResultDto;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(UserQueryParameters userQueryParameters)
    {
        var users = await userRepository.GetUsersAsync(userQueryParameters, mapper.ProjectToUserDto);

        return users;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId, mapper.ProjectToUserDto);
        if (user is null)
        {
            logger.LogInformation("Failed to find user with '{UserId}'", userId);
            throw new NotExistsException("User doesn't exist");
        }

        return user;
    }

    internal async Task EnsureUserCreatedAsync(RegisterDto register, string role)
    {
        var user = await userManager.FindByEmailAsync(register.Email);
        if (user is null)
        {
            await RegisterUserAsync(register, role);
        }
        else
        {
            if (!await userManager.IsInRoleAsync(user, role)) await userManager.AddToRoleAsync(user, role);

            if (!await userManager.CheckPasswordAsync(user, register.Password))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                await userManager.ResetPasswordAsync(user, token, register.Password);
            }
        }
    }

    private async Task<UserDto> RegisterUserAsync(RegisterDto register, string role)
    {
        var userEntity = mapper.ToUserEntity(register);
        var registrationResult = await userManager.CreateAsync(userEntity, register.Password);
        if (!registrationResult.Succeeded)
        {
            var errors = string.Join("\n", registrationResult.Errors.Select(e => e.Description));
            logger.LogInformation("User with email {Email} failed to register with errors: {@Errors}", register.Email,
                errors);
            throw new RegistrationFailedException(errors);
        }

        await userManager.AddToRoleAsync(userEntity, role);

        return mapper.ToUserDto(userEntity);
    }
}