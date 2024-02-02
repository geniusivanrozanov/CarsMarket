using FavoriteFilters.Domain.Entities;

namespace FavoriteFilters.Application.Interfaces.Services;

public interface IFiltersNotificationService
{
    Task NotifyUserByFilterId(Guid filterId);
}
