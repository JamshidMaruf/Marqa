using System.Linq.Expressions;
using Marqa.DataAccess.Extensions;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Comment out foreach loop if generating migration while you don't have corresponding database
        // Global query applied for all entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Auditable).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(Auditable.IsDeleted));
                var condition = Expression.Equal(prop, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);
                entityType.SetQueryFilter(lambda);
            }
        }

        // Ensures each concrete class that inherit from Auditable is registered in Model
        var entityAssembly = typeof(Auditable).Assembly;
        var entityTypes = entityAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(Auditable).IsAssignableFrom(t));

        foreach (var type in entityTypes)
            modelBuilder.Entity(type);
        
        // Applies global configurations written using reflection
        modelBuilder.ApplyGlobalConfigurations();

        // Applies custom entity configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
