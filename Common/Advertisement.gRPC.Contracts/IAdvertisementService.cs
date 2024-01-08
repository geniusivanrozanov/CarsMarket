using System.ServiceModel;
using Advertisement.gRPC.Contracts.Replies;
using Advertisement.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace Advertisement.gRPC.Contracts;

[ServiceContract]
public interface IAdvertisementService
{
    [OperationContract]
    Task<GetAdInfoByIdReply> GetAdInfoByIdAsync(GetAdInfoByIdRequest request, CallContext context = default);
}
