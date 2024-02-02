namespace IdentityService.Domain.Constants;

public static class ExceptionMessages
{
    public static string FailedToCreateUser { get; } = "Failed to create user with errors: {0}";
    
    public static string FailedToAddUserToRole { get; } = "Failed to add user to role with errors: {0}";
}
