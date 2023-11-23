namespace Advertisement.Application.Interfaces.Services;

public interface IUser
{
    Guid Id { get; }
    bool IsUser { get; }
    bool IsAdmin { get; }
    bool IsModerator { get; }
}
