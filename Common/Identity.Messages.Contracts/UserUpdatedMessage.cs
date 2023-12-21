namespace Identity.Messages.Contracts;

public class UserUpdatedMessage
{
    public Guid UserId { get; set; }
    public string UpdatedFirstName { get; set; } = null!;
    public string UpdatedLastName { get; set; } = null!;
}
