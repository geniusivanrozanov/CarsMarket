using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Domain.Entities;
using Advertisement.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Commands.DeleteAd;

public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<DeleteAdCommandHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public DeleteAdCommandHandler(IAdRepository adRepository, ILogger<DeleteAdCommandHandler> logger, TimeProvider timeProvider)
    {
        _adRepository = adRepository;
        _logger = logger;
        _timeProvider = timeProvider;
    }

    public async Task Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        var currentTime = _timeProvider.GetUtcNow();
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, cancellationToken);
        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);
            throw new NotExistsException($"Ad with id '{request.AdId}' not exists");
        }

        entity.UpdatedAt = currentTime;
        entity.Status = AdStatus.Deleted;

        await _adRepository.UpdateAd(entity);
    }
}
