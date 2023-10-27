using IdentityService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IdentityService.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetAuditableFields(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        SetAuditableFields(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetAuditableFields(DbContext? context)
    {
        if (context is null) return;

        var dateTime = timeProvider.GetUtcNow();
        var entries = context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is ICreatedAtAuditable or IUpdatedAtAuditable);
        foreach (var entry in entries)
        {
            if (entry is
                {
                    Entity: ICreatedAtAuditable createdAtAuditable,
                    State: EntityState.Added
                })
                createdAtAuditable.CreatedAt = dateTime;

            if (entry is
                {
                    Entity: IUpdatedAtAuditable updatedAtAuditable,
                    State: EntityState.Added or EntityState.Modified
                })
                updatedAtAuditable.UpdatedAt = dateTime;
        }
    }
}