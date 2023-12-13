using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Commands.UpdateAdStatus;

public class UpdateAdStatusCommandHandler : IRequestHandler<UpdateAdStatusCommand, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<UpdateAdStatusCommandHandler> _logger;


    public UpdateAdStatusCommandHandler(IAdRepository adRepository, ILogger<UpdateAdStatusCommandHandler> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task<GetAdDto> Handle(UpdateAdStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);
            throw new NotExistsException($"Ad with id '{request.AdId}' not exists");
        }

        entity.Status = request.UpdateAdStatusDto.Status;
        await _adRepository.UpdateAd(entity);

        return entity.ToGetAdDto();
    }
}
