using System.Runtime.Serialization;
using Advertisement.gRPC.Contracts.Models;

namespace Advertisement.gRPC.Contracts.Replies;

[DataContract]
public class GetAdsByQueryParametersReply
{
    [DataMember(Order = 1)]
    public List<AdDataContract>? Ads { get; set; }
}
