using Marqa.Domain.Entities;

namespace Marqa.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> SelectAsync(int id);
    IQueryable<TEntity> SelectAllAsQueryable();
}