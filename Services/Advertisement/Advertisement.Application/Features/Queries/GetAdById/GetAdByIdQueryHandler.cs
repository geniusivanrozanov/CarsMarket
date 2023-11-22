using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Exceptions;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, GetAdDto>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<GetAdByIdQueryHandler> _logger;

    public GetAdByIdQueryHandler(IAdRepository adRepository, ILogger<GetAdByIdQueryHandler> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }
    
    public async Task<GetAdDto> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _adRepository.GetAdByIdAsync(request.AdId, cancellationToken);
        
        if (entity is null)
        {
            _logger.LogInformation("Ad with id '{Id}' not exists", request.AdId);
            throw new NotExistsException($"Ad with id '{request.AdId}' not exists");
        }

        return entity.ToGetAdDto();
    }
}
