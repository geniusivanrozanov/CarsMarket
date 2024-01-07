using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFilterById;

public class GetFilterByIdQueryHandler : IRequestHandler<GetFilterByIdQuery, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly ILogger<GetFilterByIdQueryHandler> _logger;

    public GetFilterByIdQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork, ILogger<GetFilterByIdQueryHandler> logger)
    {
        _logger = logger;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(GetFilterByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _filterRepository.GetFilterByIdAsync<GetFilterDto>(request.FilterId, cancellationToken);

        if (dto is null)
        {
            _logger.LogInformation("Filter with id {Id} not exists", request.FilterId);
            throw new NotExistsException($"Filter with id '{request.FilterId}' not exists.");
        }

        return dto;
    }
}
