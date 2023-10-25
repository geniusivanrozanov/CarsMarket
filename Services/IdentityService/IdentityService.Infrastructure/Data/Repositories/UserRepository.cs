using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Data.Repositories;

public class UserRepository(IdentityContext context) : IUserRepository
{
    public async Task<TProjection?> GetUserByIdAsync<TProjection>(Guid userId, Func<UserEntity, TProjection> map)
    {
        var users = context.Users;

        var userEntity = await users
            .Where(u => u.Id == userId)
            .Select(u => map(u))
            .FirstOrDefaultAsync();

        return userEntity;
    }

    public async Task<IEnumerable<TProjection>> GetUsersAsync<TProjection>(UserQueryParameters userQueryParameters,
        Func<UserEntity, TProjection> map)
    {
        var query = context.Users.AsQueryable();

        if (userQueryParameters.PageSize is not null and not 0)
        {
            query = query.Take(userQueryParameters.PageSize.Value);

            if (userQueryParameters.Page is not null)
                query = query.Skip((userQueryParameters.Page * userQueryParameters.PageSize).Value);
        }

        if (userQueryParameters.FirstName is not null)
            query = query.Where(u => u.FirstName.Contains(userQueryParameters.FirstName));

        if (userQueryParameters.LastName is not null)
            query = query.Where(u => u.LastName.Contains(userQueryParameters.LastName));

        if (userQueryParameters.Email is not null)
            query = query.Where(u => u.Email!.Contains(userQueryParameters.Email));

        return await query
            .Select(u => map(u))
            .ToListAsync();
    }
}