using System.ServiceModel;
using Identity.gRPC.Contracts.Replies;
using Identity.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace Identity.gRPC.Contracts;

[ServiceContract]
public interface IIdentityService
{
    [OperationContract]
    Task<GetUserFirstNameByIdReply> GetUserFirstNameAsync(GetUserFirstNameByIdRequest request, CallContext context = default);
}
