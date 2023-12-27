using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Interfaces.Services;
using Advertisement.Application.Mappers;
using Advertisement.Domain.Enums;
using Advertisement.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Commands.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUser _user;
    private readonly ILogger<CreateAdCommandHandler> _logger;

    public CreateAdCommandHandler(IAdRepository adRepository, TimeProvider timeProvider, IUser user,
        ILogger<CreateAdCommandHandler> logger)
    {
        _adRepository = adRepository;
        _timeProvider = timeProvider;
        _user = user;
        _logger = logger;
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

        entity.CreatedAt = entity.UpdatedAt = currentTime;

        var price = dto.ToPrice();
        price.CreatedAt = currentTime;
        entity.CurrentPrice = price;

        entity.Prices = new[]
        {
            price
        };

        entity.Status = AdStatus.NotActive;

        entity.OwnerId = _user.Id;

        await _adRepository.CreateAdAsync(entity, cancellationToken);

        return entity.ToGetAdDto();
    }
}
