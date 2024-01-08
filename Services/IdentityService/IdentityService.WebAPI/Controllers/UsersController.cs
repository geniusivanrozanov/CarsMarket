using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Constants;
using IdentityService.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [DistributedCache]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var users = await userService.GetUsersAsync(queryParameters, cancellationToken);

        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Admin)]
    [DistributedCache]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByIdAsync(id, cancellationToken);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var user = await userService.RegisterUserAsync(registerDto, cancellationToken);

        return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("moderators")]
    public async Task<IActionResult> RegisterModerator([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var user = await userService.RegisterModeratorAsync(registerDto, cancellationToken);

        return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
    }
    
    [Authorize(Roles = Roles.User)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await userService.UpdateUserAsync(id, updateUserDto, cancellationToken);

        return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var result = await userService.LoginUserAsync(loginDto, cancellationToken);

        return Ok(result);
    }

    [HttpPost("login/refresh")]
    public async Task<IActionResult> LoginRefresh([FromBody] RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        var result = await userService.LoginUserByRefreshTokenAsync(refreshTokenDto, cancellationToken);

        return Ok(result);
    }
}
