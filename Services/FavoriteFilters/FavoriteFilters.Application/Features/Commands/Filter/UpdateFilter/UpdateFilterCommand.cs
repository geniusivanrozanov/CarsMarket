using FavoriteFilters.Application.DTOs.Filter;
using MediatR;

namespace FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;

public class UpdateFilterCommand : IRequest<GetFilterDto>
{
    public Guid FilterId { get; }
    public UpdateFilterDto UpdateFilterDto { get; }
    
    public UpdateFilterCommand(Guid filterId, UpdateFilterDto updateFilterDto)
    {
        UpdateFilterDto = updateFilterDto;
        FilterId = filterId;
    }
}
