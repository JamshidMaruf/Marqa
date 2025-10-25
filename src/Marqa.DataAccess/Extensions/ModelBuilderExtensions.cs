using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marqa.DataAccess.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplyGlobalConfigurations(this ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(3);
        }

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            if (property.GetMaxLength() == null)
                property.SetMaxLength(255);
        
        
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.PropertyInfo.Name == "FilePath" || p.PropertyInfo.Name == "FileName")
            if (property.GetMaxLength() == null)
                property.SetMaxLength(2048);

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType.IsEnum))
        {
            var enumType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;

            var converterType = typeof(EnumToStringConverter<>).MakeGenericType(enumType);
            var converter = Activator.CreateInstance(converterType) as ValueConverter;

            if (converter != null)
                property.SetValueConverter(converter);
        }

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Auditable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreatedAt")
                    .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("UpdatedAt")
                    .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                    .ValueGeneratedOnAddOrUpdate();
            }
        }
    }
}
