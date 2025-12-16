using System.Linq.Expressions;
using Marqa.DataAccess.Extensions;
using Marqa.DataAccess.Helpers;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Marqa.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new ConcurrencyTokenInterceptor());
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensures each concrete class that inherit from Auditable is registered in the Model
        EnsureAllEntitiesRegistered(modelBuilder);

        // Applies global table name naming convention
        modelBuilder.ApplyGlobalTableNameConfiguration();

        // Applies custom entity configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


        base.OnModelCreating(modelBuilder);

        // Comment out foreach loop if generating migration while you don't have corresponding database
        // Global query applied for all entities        
        Expression<Func<Auditable, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(Auditable)))
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyDefaultConventions();
    }

    private void EnsureAllEntitiesRegistered(ModelBuilder modelBuilder)
    {
        var entityAssembly = typeof(Auditable).Assembly;
        var entityTypes = entityAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(Auditable).IsAssignableFrom(t));

        foreach (var type in entityTypes)
            modelBuilder.Entity(type);
    }
}
