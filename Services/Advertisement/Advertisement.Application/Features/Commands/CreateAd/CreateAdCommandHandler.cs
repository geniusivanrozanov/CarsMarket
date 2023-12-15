using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Interfaces.Services;
using Advertisement.Application.Mappers;
using Advertisement.Domain.Enums;
using CarsCatalog.gRPC.Contracts;
using CarsCatalog.gRPC.Contracts.Requests;
using Identity.gRPC.Contracts;
using Identity.gRPC.Contracts.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Commands.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUser _user;
    private readonly IIdentityService _identityService;
    private readonly ICarsCatalogService _carsCatalogService;
    private readonly ILogger<CreateAdCommandHandler> _logger;

    public CreateAdCommandHandler(IAdRepository adRepository, TimeProvider timeProvider, IUser user,
        ILogger<CreateAdCommandHandler> logger, IIdentityService identityService, ICarsCatalogService carsCatalogService)
    {
        _adRepository = adRepository;
        _timeProvider = timeProvider;
        _user = user;
        _logger = logger;
        _identityService = identityService;
        _carsCatalogService = carsCatalogService;
    }

    public async Task<GetAdDto> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        var currentTime = _timeProvider.GetUtcNow();
        var dto = request.CreateAdDto;
        var entity = dto.ToAdEntity();
        
        if (entity.Vin is not null && await _adRepository.ExistsWithVinAsync(entity.Vin, cancellationToken))
        {
            _logger.LogInformation("Ad with VIN '{Vin}' already exists", entity.Vin);
            throw new AlreadyExistsException($"Ad with VIN '{entity.Vin}' already exists");
        }
        
        var ownerNameReplyTask = _identityService.GetUserFirstNameAsync(new GetUserFirstNameByIdRequest
        {
            UserId = _user.Id
        });
        
        var modificationNamesReplyTask = _carsCatalogService.GetModificationNames(new GetModificationNamesRequest
        {
            BrandId = entity.BrandId,
            ModelId = entity.ModelId,
            GenerationId = entity.GenerationId
        });

        var ownerNameReply = await ownerNameReplyTask;
        var modificationNamesReply = await modificationNamesReplyTask;

        if (ownerNameReply.Error is Identity.gRPC.Contracts.Enums.Error.UserNotFound)
        {
            _logger.LogInformation("gRPC call failed with message '{Message}'", ownerNameReply.ErrorMessage);
            throw new NotExistsException(ownerNameReply.ErrorMessage!);
        }

        entity.OwnerId = _user.Id;
        entity.OwnerName = ownerNameReply.FirstName;

        if (modificationNamesReply.Error is CarsCatalog.gRPC.Contracts.Enums.Error.ModificationNotFound)
        {
            _logger.LogInformation("gRPC call failed with message '{Message}'", modificationNamesReply.ErrorMessage);
            throw new NotExistsException(modificationNamesReply.ErrorMessage!);
        }

        entity.BrandName = modificationNamesReply.BrandName;
        entity.ModelName = modificationNamesReply.ModelName;
        entity.GenerationName = modificationNamesReply.GenerationName;
        
        entity.CreatedAt = entity.UpdatedAt = currentTime;

        var price = dto.ToPrice();
        price.CreatedAt = currentTime;
        entity.CurrentPrice = price;

        entity.Prices = new[]
        {
            price
        };

        entity.Status = AdStatus.NotActive;
        
        await _adRepository.CreateAd(entity);

        return entity.ToGetAdDto();
    }
}
