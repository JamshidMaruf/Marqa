using System.Linq.Expressions;
using Marqa.Domain.Entities;

namespace Marqa.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
    IQueryable<TEntity> SelectAllAsQueryable(); 
}