using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;

public class UpdateFilterCommandHandler : IRequestHandler<UpdateFilterCommand, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateFilterCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public UpdateFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<UpdateFilterCommandHandler> logger,
        ICurrentUser currentUser)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _currentUser = currentUser;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(UpdateFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _filterRepository.GetFilterByIdAndUserIdAsync<FilterEntity>(request.FilterId,
            _currentUser.Id,
            cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Filter with id {Id} not exists", request.FilterId);
            throw new NotExistsException($"Filter with id '{request.FilterId}' not exists.");
        }

        request.UpdateFilterDto.Adapt(entity);
        
        _filterRepository.Update(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.Adapt<GetFilterDto>();

        return dto;
    }
}
