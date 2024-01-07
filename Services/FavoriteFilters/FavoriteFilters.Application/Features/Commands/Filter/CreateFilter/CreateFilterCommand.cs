using FavoriteFilters.Application.DTOs.Filter;
using MediatR;

namespace FavoriteFilters.Application.Features.Commands.Filter.CreateFilter;

public class CreateFilterCommand : IRequest<GetFilterDto>
{
    public CreateFilterDto CreateFilterDto { get; }
    
    public CreateFilterCommand(CreateFilterDto createFilterDto)
    {
        CreateFilterDto = createFilterDto;
    }
}
