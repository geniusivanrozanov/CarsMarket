using Advertisement.gRPC.Contracts.Replies;
using Advertisement.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace Advertisement.gRPC.Contracts;

public interface IAdvertisementService
{
    Task<GetAdInfoByIdReply> GetAdInfoByIdAsync(GetAdInfoByIdRequest request, CallContext context = default);
}
