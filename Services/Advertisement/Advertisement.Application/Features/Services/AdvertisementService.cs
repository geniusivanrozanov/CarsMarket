using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using Advertisement.gRPC.Contracts;
using Advertisement.gRPC.Contracts.Enums;
using Advertisement.gRPC.Contracts.Replies;
using Advertisement.gRPC.Contracts.Requests;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;

namespace Advertisement.Application.Features.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<AdvertisementService> _logger;

    public AdvertisementService(IAdRepository adRepository, ILogger<AdvertisementService> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task<GetAdInfoByIdReply> GetAdInfoByIdAsync(GetAdInfoByIdRequest request, CallContext context = default)
    {
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, context.CancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);

            return new GetAdInfoByIdReply
            {
                Error = Error.AdNotFound,
                ErrorMessage = $"Ad with id '{request.AdId}' not exists"
            };
        }

        return entity.ToGetAdInfoByIdReply();
    }
}
