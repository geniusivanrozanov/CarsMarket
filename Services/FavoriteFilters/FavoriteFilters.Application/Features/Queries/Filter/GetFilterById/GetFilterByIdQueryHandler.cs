using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFilterById;

public class GetFilterByIdQueryHandler : IRequestHandler<GetFilterByIdQuery, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly ILogger<GetFilterByIdQueryHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public GetFilterByIdQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<GetFilterByIdQueryHandler> logger,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(GetFilterByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _filterRepository.GetFilterByIdAndUserIdAsync<GetFilterDto>(request.FilterId,
            _currentUser.Id,
            cancellationToken);

        if (dto is null)
        {
            _logger.LogInformation("Filter with id {Id} not exists", request.FilterId);
            throw new NotExistsException($"Filter with id '{request.FilterId}' not exists.");
        }

        return dto;
    }
}
