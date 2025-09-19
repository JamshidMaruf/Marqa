
using Marqa.DataAccess.Contexts;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext context;
    public Repository()
    {
        context = new AppDbContext();
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

    public async Task<TEntity> SelectAsync(int id)
    {
        return await context.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return context.Set<TEntity>().AsQueryable();
    }
}