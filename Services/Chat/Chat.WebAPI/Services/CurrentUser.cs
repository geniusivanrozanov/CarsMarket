using System.Security.Claims;
using Chat.Application.Interfaces.Services;

namespace Chat.WebAPI.Services;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _user;
    
    public Guid Id => Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext!.User;
    }
}
