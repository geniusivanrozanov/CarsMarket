using System.Security.Claims;
using Advertisement.Application.Interfaces.Services;
using Advertisement.Domain.Constants;

namespace Advertisement.WebAPI.Services;

public class CurrentUser : IUser
{
    private readonly ClaimsPrincipal _user;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext!.User;
    }

    public Guid Id => Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public bool IsUser => _user.IsInRole(Roles.User);
    public bool IsAdmin => _user.IsInRole(Roles.Admin);
    public bool IsModerator => _user.IsInRole(Roles.Moderator);
}
