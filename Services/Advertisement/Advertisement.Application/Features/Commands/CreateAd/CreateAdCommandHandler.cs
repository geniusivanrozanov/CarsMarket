using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Interfaces.Services;
using Advertisement.Application.Mappers;
using Advertisement.Domain.Enums;
using Advertisement.Domain.ValueObjects;
using MediatR;

namespace Advertisement.Application.Features.Commands.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUser _user;

    public CreateAdCommandHandler(IAdRepository adRepository, TimeProvider timeProvider, IUser user)
    {
        _adRepository = adRepository;
        _timeProvider = timeProvider;
        _user = user;
    }

    public async Task<GetAdDto> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        var currentTime = _timeProvider.GetUtcNow();
        var dto = request.CreateAdDto;
        var entity = dto.ToAdEntity();

        entity.CreatedAt = entity.UpdatedAt = currentTime;
        
        var price = dto.ToPrice();
        price.CreatedAt = currentTime;
        entity.CurrentPrice = price;

        entity.Prices = new []
        {
            price
        };

        entity.Status = AdStatus.NotActive;

        entity.OwnerId = _user.Id;
        
        await _adRepository.CreateAd(entity);

        return entity.ToGetAdDto();
    }
}
