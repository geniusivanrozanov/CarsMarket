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

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SetAuditableFields(eventData.Context);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetAuditableFields(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var dateTime = timeProvider.GetUtcNow();
        var entries = context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAuditable);
        foreach (var entry in entries)
        {
            if (entry.Entity is not IAuditable auditable) continue;
            if (entry.State is EntityState.Added)
            {
                auditable.CreatedAt = dateTime;
            }
            
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                auditable.LastModifiedAt = dateTime;
            }
        }
    }
}