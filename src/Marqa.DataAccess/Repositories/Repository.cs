
using Marqa.DataAccess.Contexts;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext context;
    public Repository()
    {
        context = new AppDbContext();
        context.Set<TEntity>();
    }
    
    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var createdEntity = (await context.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return createdEntity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }
   
    public async Task DeleteAsync(TEntity entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity> SelectAsync(int id)
    {
        return await context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return context.Set<TEntity>().AsQueryable();
    }
}