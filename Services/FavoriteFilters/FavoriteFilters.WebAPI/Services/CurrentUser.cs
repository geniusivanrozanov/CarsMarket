using System.Security.Claims;
using FavoriteFilters.Application.Interfaces.Services;

namespace FavoriteFilters.WebAPI.Services;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _user;

    public Guid Id => Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string Email => _user.FindFirstValue(ClaimTypes.Email)!;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext!.User;
    }
}
