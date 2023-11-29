using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.QueryParameters;
using MediatR;

namespace Advertisement.Application.Features.Queries.GetAdsList;

public class GetAdsListQuery : IRequest<IEnumerable<GetAdDto>>
{
    public GetAdsListQuery(AdQueryParameters queryParameters)
    {
        QueryParameters = queryParameters;
    }

    public AdQueryParameters QueryParameters { get; }
}
