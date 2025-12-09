using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marqa.DataAccess.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplyGlobalConfigurations(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name + "s");
    }
}
