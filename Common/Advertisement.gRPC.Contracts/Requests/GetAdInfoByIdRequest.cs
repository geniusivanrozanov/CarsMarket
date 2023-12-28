using System.Runtime.Serialization;

namespace Advertisement.gRPC.Contracts.Requests;

[DataContract]
public class GetAdInfoByIdRequest
{
    [DataMember(Order = 1)]
    public Guid AdId { get; set; }
}
