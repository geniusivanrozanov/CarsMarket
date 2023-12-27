namespace Advertisement.Application.Interfaces.Services;

public interface IUser
{
    Guid Id { get; }
    bool IsInRole(string role);
}
