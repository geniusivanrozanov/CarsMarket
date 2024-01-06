namespace Notification.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string recipientEmailAddress, string subject, string body,
        CancellationToken cancellationToken = default);
}
