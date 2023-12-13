using System.ServiceModel;
using CarsCatalog.gRPC.Contracts.Replies;
using CarsCatalog.gRPC.Contracts.Requests;
using ProtoBuf.Grpc;

namespace CarsCatalog.gRPC.Contracts;

[ServiceContract]
public interface ICarsCatalogService
{
    [OperationContract]
    Task<GetModificationNamesReply> GetModificationNames(GetModificationNamesRequest request,
        CallContext context = default);
}
