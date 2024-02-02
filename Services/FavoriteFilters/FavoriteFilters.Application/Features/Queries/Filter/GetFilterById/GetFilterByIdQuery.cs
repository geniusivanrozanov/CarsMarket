using FavoriteFilters.Application.DTOs.Filter;
using MediatR;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFilterById;

public class GetFilterByIdQuery : IRequest<GetFilterDto>
{
    public Guid FilterId { get; }
    
    public GetFilterByIdQuery(Guid filterId)
    {
        FilterId = filterId;
    }
}
