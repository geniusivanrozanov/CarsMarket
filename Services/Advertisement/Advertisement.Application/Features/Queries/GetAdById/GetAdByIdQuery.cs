using Advertisement.Application.DTOs.Ad;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdById;

public class GetAdByIdQuery : IRequest<GetAdDto>
{
    public GetAdByIdQuery(Guid adId)
    {
        AdId = adId;
    }

    public Guid AdId { get; }
}
