using System.Runtime.Serialization;
using Advertisement.gRPC.Contracts.Enums;

namespace Advertisement.gRPC.Contracts.Models;

[DataContract]
public class AdDataContract
{
    [DataMember(Order = 1)]
    public Guid Id { get; set; }
    
    [DataMember(Order = 2)]
    public string? Description { get; set; }
    
    [DataMember(Order = 3)]
    public string? Vin { get; set; }
    
    [DataMember(Order = 4)]
    public string Brand { get; set; } = null!;
    
    [DataMember(Order = 5)]
    public string Model { get; set; } = null!;
    
    [DataMember(Order = 6)]
    public string Generation { get; set; } = null!;
    
    [DataMember(Order = 7)]
    public int Year { get; set; }
    
    [DataMember(Order = 8)]
    public int Mileage { get; set; }

    [DataMember(Order = 9)]
    public double Price { get; set; }
    
    [DataMember(Order = 10)]
    public Currency Currency { get; set; }
}
