﻿using System.Linq.Expressions;
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
        _context.Add(entity);
        return entity;
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        foreach (var entity in entityList)
        {
            entity.CreatedAt = DateTime.UtcNow;
        }
        await _context.AddRangeAsync(entityList);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Update(entity);
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach(var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
        }
        _context.UpdateRange(entities);

        return Task.CompletedTask;
    }
    public void MarkAsDeleted(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.IsDeleted = true;
        _context.Entry(entity).Property(e => e.DeletedAt).IsModified = true;
        _context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
    }

    public Task MarkRangeAsDeletedAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            _context.Entry(entity).Property(e => e.DeletedAt).IsModified = true;
            _context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
        }
        return Task.CompletedTask;
    }

    public void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = _context.Set<TEntity>().Where(predicate).AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> SelectAllAsQueryable(
        Expression<Func<TEntity, bool>> predicate = null,
        bool tracking = true,
        params string[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        foreach (var include in includes)
            query = query.Include(include);

        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<bool> CheckExistAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public void DetachFromChangeTracker(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }

    public async Task<bool> CanConnectAsync()
    {
        return await _context.Database.CanConnectAsync();
    }
}