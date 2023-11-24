using IdentityService.Application.Interfaces;
using IdentityService.Application.QueryParameters;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Data.Contexts;
using IdentityService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Data.Repositories;

public class UserRepository(IdentityContext context) : GenericRepository<UserEntity>(context), IUserRepository
{
    public async Task<TProjection?> GetUserByIdAsync<TProjection>(Guid userId,
        Func<IQueryable<UserEntity>, IQueryable<TProjection>> projector, CancellationToken cancellationToken)
    {
        var userEntity = await Get(u => u.Id == userId)
            .ApplyProjector(projector)
            .FirstOrDefaultAsync(cancellationToken);

        return userEntity;
    }

    public async Task<IEnumerable<TProjection>> GetUsersAsync<TProjection>(UserQueryParameters userQueryParameters,
        Func<IQueryable<UserEntity>, IQueryable<TProjection>> projector, CancellationToken cancellationToken)
    {
        var query = Get(userQueryParameters);

        return await query
            .ApplyProjector(projector)
            .ToListAsync(cancellationToken);
    }
}
