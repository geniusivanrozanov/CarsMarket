using System.Runtime.Serialization;

namespace CarsCatalog.gRPC.Contracts.Requests;

[DataContract]
public class GetModificationNamesRequest
{
    [DataMember(Order = 1)]
    public Guid BrandId { get; set; }
    [DataMember(Order = 2)]
    public Guid ModelId { get; set; }
    [DataMember(Order = 3)]
    public Guid GenerationId { get; set; }
}
