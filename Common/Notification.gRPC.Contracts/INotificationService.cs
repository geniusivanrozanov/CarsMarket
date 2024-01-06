using System.ServiceModel;
using Notification.gRPC.Contracts.Replies;
using Notification.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace Notification.gRPC.Contracts;

[ServiceContract]
public interface INotificationService
{
    [OperationContract]
    Task<SendEmailReply> SendEmailAsync(SendEmailRequest request, CallContext context = default);
}
