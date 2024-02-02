using Advertisement.Application.DTOs.Price;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdPriceHistoryQuery : IRequest<IEnumerable<GetPriceDto>>
{
    public GetAdPriceHistoryQuery(Guid adId)
    {
        AdId = adId;
    }

    public Guid AdId { get; }
}
