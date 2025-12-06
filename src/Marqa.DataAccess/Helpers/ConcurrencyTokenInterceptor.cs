using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Marqa.DataAccess.Helpers;

internal class ConcurrencyTokenInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context == null)
            return base.SavingChanges(eventData, result);

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if(entry.State == EntityState.Modified &&
                entry.Metadata.FindProperty("RowVersion") != null)
            {
                long currentValue = (long) entry.Property("RowVersion").CurrentValue;
                entry.Property("RowVersion").CurrentValue = currentValue + 1;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
