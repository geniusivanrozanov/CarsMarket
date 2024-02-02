using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Notification.Application.Configurations;
using Notification.Application.Interfaces;

namespace Notification.Application.Services;

public class EmailService : IEmailService
{
    private readonly MailConfiguration _mailConfiguration;

    public EmailService(IOptions<MailConfiguration> mailConfigurationOptions)
    {
        _mailConfiguration = mailConfigurationOptions.Value;
    }

    public async Task SendEmailAsync(string recipientEmailAddress, string subject, string body,
        CancellationToken cancellationToken = default)
    {
        var emailMessage = BuildMessage(recipientEmailAddress, subject, body);

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_mailConfiguration.Host, _mailConfiguration.Port,
            cancellationToken: cancellationToken);

        await smtpClient.AuthenticateAsync(_mailConfiguration.UserName, _mailConfiguration.Password, cancellationToken);

        await smtpClient.SendAsync(emailMessage, cancellationToken);

        await smtpClient.DisconnectAsync(true, cancellationToken);
    }

    private MimeMessage BuildMessage(string recipientEmailAddress, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress(_mailConfiguration.DisplayName, _mailConfiguration.From));
        emailMessage.To.Add(new MailboxAddress("", recipientEmailAddress));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = body
        };

        return emailMessage;
    }
}
