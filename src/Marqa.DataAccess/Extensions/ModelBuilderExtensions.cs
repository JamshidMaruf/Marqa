﻿using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplyGlobalConfigurations(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name + "s");

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
                .Where(p => p.PropertyInfo.Name.EndsWith("FilePath") || p.PropertyInfo.Name.EndsWith("FileName")))
            if (property.GetMaxLength() == null)
                property.SetMaxLength(2048);     
    }
}
