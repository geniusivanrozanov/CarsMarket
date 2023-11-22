using Advertisement.Application.DTOs.Ad;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdsList;

public class GetAdsListQuery : IRequest<IEnumerable<GetAdDto>>
{
    
}
