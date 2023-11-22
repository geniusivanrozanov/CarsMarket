using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.DTOs.Price;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdPriceHistoryByIdQuery : IRequest<IEnumerable<GetPriceDto>>
{
    public GetAdPriceHistoryByIdQuery(Guid adId)
    {
        AdId = adId;
    }

    public Guid AdId { get; }
}
