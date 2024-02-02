using System.Runtime.Serialization;

namespace Notification.gRPC.Contracts.Requests;

[DataContract]
public class SendEmailRequest
{
    [DataMember(Order = 1)]
    public string RecipientEmailAddress { get; set; } = null!;
    [DataMember(Order = 2)]
    public string Subject { get; set; } = null!;
    [DataMember(Order = 3)]
    public string Body { get; set; } = null!;
}
