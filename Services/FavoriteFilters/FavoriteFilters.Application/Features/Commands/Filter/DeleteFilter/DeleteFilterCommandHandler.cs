using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Commands.Filter.DeleteFilter;

public class DeleteFilterCommandHandler : IRequestHandler<DeleteFilterCommand>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<DeleteFilterCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public DeleteFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<DeleteFilterCommandHandler> logger,
        ICurrentUser currentUser)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _currentUser = currentUser;
        _filterRepository = repositoryUnitOfWork.Filters;
    }

    public async Task Handle(DeleteFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _filterRepository.GetFilterByIdAndUserIdAsync<FilterEntity>(request.FilterId,
            _currentUser.Id,
            cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Filter with id {Id} not exists", request.FilterId);
            throw new NotExistsException($"Filter with id '{request.FilterId}' not exists.");
        }

        _filterRepository.Delete(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
