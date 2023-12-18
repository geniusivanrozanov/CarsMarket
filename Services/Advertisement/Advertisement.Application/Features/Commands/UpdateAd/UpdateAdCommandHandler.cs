using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using Advertisement.Domain.Enums;
using Advertisement.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Commands.UpdateAd;

public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<UpdateAdCommandHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public UpdateAdCommandHandler(IAdRepository adRepository, ILogger<UpdateAdCommandHandler> logger,
        TimeProvider timeProvider)
    {
        _adRepository = adRepository;
        _logger = logger;
        _timeProvider = timeProvider;
    }

    public async Task<GetAdDto> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        var currentTime = _timeProvider.GetUtcNow();
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);
            throw new NotExistsException($"Ad with id '{request.AdId}' not exists");
        }

        var dto = request.UpdateAdDto;

        if (dto.Vin is not null && entity.Vin != dto.Vin &&
            await _adRepository.ExistsWithVinAsync(dto.Vin, cancellationToken))
        {
            _logger.LogInformation("Ad with VIN '{Vin}' already exists", dto.Vin);
            throw new AlreadyExistsException($"Ad with VIN '{dto.Vin}' already exists");
        }

        dto.ToAdEntity(entity);

        entity.UpdatedAt = currentTime;

        var newPrice = dto.ToPrice();
        newPrice.CreatedAt = currentTime;

        entity.Prices ??= new List<Price>();
        entity.Prices.Add(newPrice);

        entity.CurrentPrice = newPrice;

        entity.Status = AdStatus.NotActive;

        await _adRepository.UpdateAd(entity, cancellationToken);

        return entity.ToGetAdDto();
    }
}
