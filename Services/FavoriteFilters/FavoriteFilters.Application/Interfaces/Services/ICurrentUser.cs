namespace FavoriteFilters.Application.Interfaces.Services;

public interface ICurrentUser
{
    Guid Id { get; }
    string Email { get; }
}
