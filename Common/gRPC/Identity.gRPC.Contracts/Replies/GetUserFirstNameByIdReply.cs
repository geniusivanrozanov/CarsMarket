using System.Runtime.Serialization;
using Identity.gRPC.Contracts.Enums;

namespace Identity.gRPC.Contracts.Replies;

[DataContract]
public class GetUserFirstNameByIdReply
{
    [DataMember(Order = 1)]
    public string FirstName { get; set; } = null!;
    
    [DataMember(Order = 2)]
    public Error? Error { get; set; }
    
    [DataMember(Order = 3)]
    public string? ErrorMessage { get; set; }
}
