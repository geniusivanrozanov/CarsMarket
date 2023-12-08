using System.Runtime.Serialization;

namespace Identity.gRPC.Contracts.Requests;

[DataContract]
public class GetUserFirstNameByIdRequest
{
    [DataMember(Order = 1)]
    public Guid UserId { get; set; }
}
