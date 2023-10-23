using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task<TProjection> GetUserByIdAsync<TProjection>(Guid userId, Func<UserEntity, TProjection> map);
    Task<IEnumerable<TProjection>> GetUsersAsync<TProjection>(UserQueryParameters userQueryParameters, Func<UserEntity, TProjection> map);
}