using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using Hangfire;
using Mapster;
using MapsterMapper;
using MediatR;
using Cron = FavoriteFilters.Domain.ValueObjects.Cron;

namespace FavoriteFilters.Application.Features.Commands.Filter.CreateFilter;

public class CreateFilterCommandHandler : IRequestHandler<CreateFilterCommand, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly IFiltersNotificationService _filtersNotification;
    private readonly IRecurringJobManager _recurringJobManager;

    public CreateFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ICurrentUser currentUser,
        IRecurringJobManager recurringJobManager,
        IFiltersNotificationService filtersNotification)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _currentUser = currentUser;
        _recurringJobManager = recurringJobManager;
        _filtersNotification = filtersNotification;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(CreateFilterCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateFilterDto;
        var entity = dto.Adapt<FilterEntity>();
        var cron = new Cron(dto.CronMinute, dto.CronHour, dto.CronDayOfMonth, dto.CronMonth, dto.CronDayOfWeek);

        entity.Cron = cron;
        entity.UserId = _currentUser.Id;
        entity.UserEmail = _currentUser.Email;
        
        _filterRepository.Create(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
        
        _recurringJobManager.AddOrUpdate(entity.Id.ToString(),
            () => _filtersNotification.NotifyUserByFilterId(entity.Id),
            entity.Cron.ToString());

        var getFilterDto = entity.Adapt<GetFilterDto>();

        return getFilterDto;
    }
}
