using System.Reflection;

namespace IdentityService.Domain.Constants;

public static class Roles
{
    public const string User = nameof(User);
    public const string Admin = nameof(Admin);
    public const string Moderator = nameof(Moderator);

    public static IReadOnlyList<string> GetAllRoles()
    {
        var list = typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!)
            .ToList()
            .AsReadOnly();

        return list;
    }
}