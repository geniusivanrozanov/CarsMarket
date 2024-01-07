using MediatR;

namespace FavoriteFilters.Application.Features.Commands.Filter.DeleteFilter;

public class DeleteFilterCommand : IRequest
{
    public Guid FilterId { get; }
    
    public DeleteFilterCommand(Guid filterId)
    {
        FilterId = filterId;
    }
}
