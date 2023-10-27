using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task<TProjection?> GetUserByIdAsync<TProjection>(Guid userId,
        Func<IQueryable<UserEntity>, IQueryable<TProjection>> projector);

    Task<IEnumerable<TProjection>> GetUsersAsync<TProjection>(UserQueryParameters userQueryParameters,
        Func<IQueryable<UserEntity>, IQueryable<TProjection>> projector);
}
