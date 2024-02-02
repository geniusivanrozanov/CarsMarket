namespace Notification.Application.Configurations;

public class MailConfiguration
{
    public string DisplayName { get; set; } = null!;
    public string From { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
}
