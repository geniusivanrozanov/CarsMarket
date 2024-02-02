using System.Runtime.Serialization;
using CarsCatalog.gRPC.Contracts.Enums;

namespace CarsCatalog.gRPC.Contracts.Replies;

[DataContract]
public class GetModificationNamesReply
{
    [DataMember(Order = 1)]
    public string BrandName { get; set; } = null!;
    
    [DataMember(Order = 2)]
    public string ModelName { get; set; } = null!;
    
    [DataMember(Order = 3)]
    public string GenerationName { get; set; } = null!;
    
    [DataMember(Order = 4)]
    public Error? Error { get; set; }
    
    [DataMember(Order = 5)]
    public string? ErrorMessage { get; set; }
}
