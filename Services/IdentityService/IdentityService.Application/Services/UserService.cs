using IdentityService.Application.DTOs;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Constants;
using IdentityService.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application.Services;

public class UserService(
        ILogger<UserService> logger,
        ITokenService tokenService,
        IUserRepository userRepository,
        IMapper mapper,
        UserManager<UserEntity> userManager,
        ICurrentUser currentUser,
        IPublishEndpoint publishEndpoint)
    : IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterDto register, CancellationToken cancellationToken)
    {
        return RegisterUserAsync(register, Roles.User, cancellationToken);
    }

    public Task<UserDto> RegisterModeratorAsync(RegisterDto register, CancellationToken cancellationToken)
    {
        return RegisterUserAsync(register, Roles.Moderator, cancellationToken);
    }

    public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            logger.LogInformation("Failed to find user with '{UserId}'", userId);
            throw new NotExistsException("User doesn't exist");
        }

        if (currentUser.Id != userId)
        {
            throw new ForbiddenActionException($"Current user cannot update user with id '{userId}'");
        }

        mapper.ToUserEntity(updateUserDto, user);

        await userManager.UpdateAsync(user);

        var message = mapper.ToUserUpdatedMessage(user);
        await publishEndpoint.Publish(message, cancellationToken);

        var dto = mapper.ToUserDto(user);

        return dto;
    }

    public async Task<LoginResultDto> LoginUserAsync(LoginDto login, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(login.Email);
        if (userEntity is null || !await userManager.CheckPasswordAsync(userEntity, login.Password))
        {
            logger.LogInformation("User with email {Email} failed to login", login.Email);
            throw new LoginFailedException("Invalid username or password");
        }

        return await LoginUserAsync(userEntity, cancellationToken);
    }

    public async Task<LoginResultDto> LoginUserByRefreshTokenAsync(RefreshTokenDto refreshToken, CancellationToken cancellationToken)
    {
        var userId = await tokenService.GetUserIdByRefreshToken(refreshToken.RefreshToken, cancellationToken);

        if (userId is null)
        {
            logger.LogInformation("User with refresh token {RefreshToken} failed to login", refreshToken.RefreshToken);
            throw new LoginFailedException("Invalid refresh token");
        }

        var userEntity = await userManager.FindByIdAsync(userId.ToString()!);

        return await LoginUserAsync(userEntity!, cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(UserQueryParameters userQueryParameters, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(userQueryParameters, mapper.ProjectToUserDto, cancellationToken);

        return users;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(userId, mapper.ProjectToUserDto, cancellationToken);
        if (user is null)
        {
            logger.LogInformation("Failed to find user with '{UserId}'", userId);
            throw new NotExistsException("User doesn't exist");
        }

        return user;
    }

    private async Task<UserDto> RegisterUserAsync(RegisterDto register, string role, CancellationToken cancellationToken)
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

    private async Task<LoginResultDto> LoginUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var loginResults = await Task.WhenAll(
            tokenService.GenerateAccessTokenAsync(userEntity, cancellationToken),
            tokenService.GenerateAndSaveRefreshTokenAsync(userEntity.Id, cancellationToken));
        var loginResultDto = new LoginResultDto
        {
            AccessToken = loginResults[0],
            RefreshToken = loginResults[1]
        };

        return loginResultDto;
    }
}
