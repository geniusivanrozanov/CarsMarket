using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Interfaces.Repositories;
using Advertisement.Application.Mappers;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdsList;

public class GetAdsListQueryHandler : IRequestHandler<GetAdsListQuery, IEnumerable<GetAdDto>>
{
    private readonly IAdRepository _adRepository;

    public GetAdsListQueryHandler(IAdRepository adRepository)
    {
        _adRepository = adRepository;
    }

    public async Task<IEnumerable<GetAdDto>> Handle(GetAdsListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _adRepository.GetAdsAsync(request.QueryParameters, cancellationToken);

        return entities.ToGetAdDto();
    }
}
