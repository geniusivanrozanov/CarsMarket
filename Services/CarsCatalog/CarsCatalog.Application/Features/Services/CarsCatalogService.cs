using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.gRPC.Contracts;
using CarsCatalog.gRPC.Contracts.Enums;
using CarsCatalog.gRPC.Contracts.Replies;
using CarsCatalog.gRPC.Contracts.Requests;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;

namespace CarsCatalog.Application.Features.Services;

public class CarsCatalogService : ICarsCatalogService
{
    private readonly IGenerationRepository _generationRepository;
    private readonly ILogger<CarsCatalogService> _logger;

    public CarsCatalogService(IGenerationRepository generationRepository, ILogger<CarsCatalogService> logger)
    {
        _generationRepository = generationRepository;
        _logger = logger;
    }

    public async Task<GetModificationNamesReply> GetModificationNames(GetModificationNamesRequest request, CallContext context = default)
    {
        var reply = await _generationRepository.GetGenerationByOwnIdAndModelIdAndBrandIdAsync<GetModificationNamesReply>(
            request.GenerationId,
            request.ModelId,
            request.BrandId,
            context.CancellationToken);

        if (reply is null)
        {
            reply = new GetModificationNamesReply
            {
                Error = Error.ModificationNotFound,
                ErrorMessage = "Modification with requested parameters doesn't exists"
            };
            
            _logger.LogInformation(
                "Generation with Id '{GenerationId}', ModelId '{ModelId}' and BrandId {BrandId} doesn't exists",
                request.GenerationId,
                request.ModelId,
                request.BrandId);
        }
        
        return reply;
    }
}
