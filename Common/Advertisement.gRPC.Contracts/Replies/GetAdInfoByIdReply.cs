using System.Runtime.Serialization;
using Advertisement.gRPC.Contracts.Enums;

namespace Advertisement.gRPC.Contracts.Replies;

[DataContract]
public class GetAdInfoByIdReply
{
    [DataMember(Order = 1)]
    public Guid OwnerId { get; set; }
    
    [DataMember(Order = 2)]
    public Error? Error { get; set; }
    
    [DataMember(Order = 3)]
    public string? ErrorMessage { get; set; }
}
