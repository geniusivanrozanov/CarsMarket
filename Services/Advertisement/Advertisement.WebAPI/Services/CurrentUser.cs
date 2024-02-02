using System.Security.Claims;
using Advertisement.Application.Interfaces.Services;
using Advertisement.Domain.Constants;

namespace Advertisement.WebAPI.Services;

public class CurrentUser : IUser
{
    private readonly ClaimsPrincipal _user;
    
    public Guid Id => Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext!.User;
    }
    
    public bool IsInRole(string role)
    {
        return _user.IsInRole(role);
    }
}
