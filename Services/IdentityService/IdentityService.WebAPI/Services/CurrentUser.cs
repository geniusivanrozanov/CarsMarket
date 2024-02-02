using System.Security.Claims;
using IdentityService.Application.Interfaces;

namespace IdentityService.WebAPI.Services;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _user;

    public Guid Id => Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext!.User;
    }
}
