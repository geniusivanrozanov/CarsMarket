using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationsListQuery : IRequest<IEnumerable<GetGenerationDto>>
{
    public GetGenerationsListQuery(GetGenerationsListQueryParameters queryParameters)
    {
        QueryParameters = queryParameters;
    }

    public GetGenerationsListQueryParameters QueryParameters { get; }
}
