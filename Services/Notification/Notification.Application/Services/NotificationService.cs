using Notification.Application.Interfaces;
using Notification.gRPC.Contracts;
using Notification.gRPC.Contracts.Replies;
using Notification.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace Notification.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;

    public NotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<SendEmailReply> SendEmailAsync(SendEmailRequest request, CallContext context = default)
    {
        await _emailService.SendEmailAsync(request.RecipientEmailAddress, request.Body, request.Subject,
            context.CancellationToken);

        return new SendEmailReply();
    }
}
