using System.Linq.Expressions;
using Marqa.Domain.Entities;

namespace Marqa.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> Insert(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
    
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
    
    IQueryable<TEntity> SelectAllAsQueryable();
}