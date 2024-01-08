using System.Runtime.Serialization;
using Advertisement.gRPC.Contracts.Enums;

namespace Advertisement.gRPC.Contracts.Requests;

[DataContract]
public class GetAdsByQueryParametersRequest
{
    [DataMember(Order = 1)]
    public Guid? BrandId { get; set; }
    
    [DataMember(Order = 2)]
    public Guid? ModelId { get; set; }
    
    [DataMember(Order = 3)]
    public Guid? GenerationId { get; set; }
    
    [DataMember(Order = 4)]
    public int? MinYear { get; set; }
    
    [DataMember(Order = 5)]
    public int? MaxYear { get; set; }
    
    [DataMember(Order = 6)]
    public int? MinMileage { get; set; }
    
    [DataMember(Order = 7)]
    public int? MaxMileage { get; set; }
    
    [DataMember(Order = 8)]
    public double? MinPrice { get; set; }
    
    [DataMember(Order = 9)]
    public double? MaxPrice { get; set; }
    
    [DataMember(Order = 10)]
    public Currency? Currency { get; set; }
}
