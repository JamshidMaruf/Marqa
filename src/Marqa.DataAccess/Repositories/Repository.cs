using System.Linq.Expressions;
using Marqa.DataAccess.Contexts;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        this._context = context;
        _context.Set<TEntity>();
    }
    
    public TEntity Insert(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var createdEnity = (_context.Add(entity)).Entity;
        return createdEnity;
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await entities.AsQueryable().ForEachAsync(entity =>
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Add(entity);
        });
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Update(entity);
    }
   
    public void Delete(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.IsDeleted = true;
        _context.Entry(entity).Property(e => e.DeletedAt).IsModified = true;
        _context.Entry(entity).Property(e => e.IsDeleted).IsModified = true; 
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
    {
        var query = _context.Set<TEntity>().Where(predicate).AsQueryable();

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
        return _context.Set<TEntity>().AsQueryable();
    }


}