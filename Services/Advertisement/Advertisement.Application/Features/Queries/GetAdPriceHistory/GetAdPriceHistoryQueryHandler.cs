using Advertisement.Application.DTOs.Price;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdPriceHistoryQueryHandler : IRequestHandler<GetAdPriceHistoryQuery, IEnumerable<GetPriceDto>>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<GetAdPriceHistoryQueryHandler> _logger;

    public GetAdPriceHistoryQueryHandler(IAdRepository adRepository,
        ILogger<GetAdPriceHistoryQueryHandler> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<GetPriceDto>> Handle(GetAdPriceHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);
            throw new NotExistsException($"Ad with id '{request.AdId}' not exists");
        }

        return entity.Prices!.ToGetPriceDto();
    }
}
