using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Exceptions;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using Hangfire;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Cron = FavoriteFilters.Domain.ValueObjects.Cron;

namespace FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;

public class UpdateFilterCommandHandler : IRequestHandler<UpdateFilterCommand, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateFilterCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IFiltersNotificationService _filtersNotification;
    private readonly IRecurringJobManager _recurringJobManager;

    public UpdateFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<UpdateFilterCommandHandler> logger,
        ICurrentUser currentUser,
        IRecurringJobManager recurringJobManager,
        IFiltersNotificationService filtersNotification)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _currentUser = currentUser;
        _recurringJobManager = recurringJobManager;
        _filtersNotification = filtersNotification;
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

        var dto = request.UpdateFilterDto;
        dto.Adapt(entity);
        var cron = new Cron(dto.CronMinute, dto.CronHour, dto.CronDayOfMonth, dto.CronMonth, dto.CronDayOfWeek);

        entity.Cron = cron;
        
        _filterRepository.Update(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
        
        _recurringJobManager.AddOrUpdate(entity.Id.ToString(),
            () => _filtersNotification.NotifyUserByFilterId(entity.Id),
            entity.Cron.ToString());

        var getFilterDto = entity.Adapt<GetFilterDto>();

        return getFilterDto;
    }
}
