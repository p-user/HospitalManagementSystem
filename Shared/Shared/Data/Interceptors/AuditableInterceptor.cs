using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Shared.Data.Interceptors
{
    public class AuditableInterceptor : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateIAuditable(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateIAuditable(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateIAuditable(DbContext? context)
        {
            if (context is null) return;


            var entities = context.ChangeTracker.Entries<IEntity>().ToList();

            foreach (EntityEntry<IEntity> entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Admin";
                    entry.Entity.CreatedAt = DateTime.Now;
                }

                entry.Entity.LastUpdatedBy = "Admin";
                entry.Entity.LastUpdate = DateTime.Now;


            }
        }
    }
}
