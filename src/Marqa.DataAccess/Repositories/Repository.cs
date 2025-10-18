using System.Linq.Expressions;
using Marqa.DataAccess.Contexts;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext context;
    public Repository(AppDbContext context)
    {
        this.context = context;
        context.Set<TEntity>();
    }
    
    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var createdEntity = (await context.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return createdEntity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        context.Update(entity);
        await context.SaveChangesAsync();
    }
   
    public async Task DeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.IsDeleted = true;
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
    {
        var query = context.Set<TEntity>().Where(predicate).AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        
        return await query.FirstOrDefaultAsync(predicate);
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return context.Set<TEntity>().AsQueryable();
    }
}