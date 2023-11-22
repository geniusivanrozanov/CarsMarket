using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.DTOs.Price;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdPriceHistoryByIdQueryHandler : IRequestHandler<GetAdPriceHistoryByIdQuery, IEnumerable<GetPriceDto>>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<GetAdPriceHistoryByIdQueryHandler> _logger;

    public GetAdPriceHistoryByIdQueryHandler(IAdRepository adRepository, ILogger<GetAdPriceHistoryByIdQueryHandler> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<GetPriceDto>> Handle(GetAdPriceHistoryByIdQuery request, CancellationToken cancellationToken)
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
