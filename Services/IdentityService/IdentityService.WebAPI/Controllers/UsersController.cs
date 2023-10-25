using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery]UserQueryParameters queryParameters)
    {
        var users = await userService.GetUsersAsync(queryParameters);

        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userService.GetUserByIdAsync(id);

        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
    {
        var user = await userService.RegisterUserAsync(registerDto);

        return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
    {
        var result = await userService.LoginUserAsync(loginDto);

        return Ok(result);
    }
}