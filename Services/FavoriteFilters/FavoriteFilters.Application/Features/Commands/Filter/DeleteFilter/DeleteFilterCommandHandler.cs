using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Commands.Filter.DeleteFilter;

public class DeleteFilterCommandHandler : IRequestHandler<DeleteFilterCommand>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateFilterCommandHandler> _logger;

    public DeleteFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork, ILogger<UpdateFilterCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task Handle(DeleteFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _filterRepository.GetFilterByIdAsync<FilterEntity>(request.FilterId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Filter with id {Id} not exists", request.FilterId);
            throw new NotExistsException($"Filter with id '{request.FilterId}' not exists.");
        }
        
        _filterRepository.Delete(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
