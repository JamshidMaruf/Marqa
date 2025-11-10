using System.Linq.Expressions;
using Marqa.Domain.Entities;

namespace Marqa.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    TEntity Insert(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    
    void Update(TEntity entity);
    
    /// <summary>
    /// Marks the entity as deleted. But does not delete the actual record from database
    /// </summary>
    /// <param name="entity"></param>
    void MarkAsDeleted(TEntity entity);

    /// <summary>
    /// Sets the entity object's state as deleted which ef core will delete the corresponding record from database table 
    /// when savechanges method is called
    /// </summary>
    /// <param name="entity"></param>
    void Remove(TEntity entity);

    /// <summary>
    /// Sets the all of the entity objects' state as deleted which ef core will delete the corresponding records from database table 
    /// when savechanges method is called
    /// </summary>
    /// <param name="entities"></param>
    void RemoveRange(IEnumerable<TEntity> entities);
    
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
    
    IQueryable<TEntity> SelectAllAsQueryable(Expression<Func<TEntity, bool>> predicate, string[] includes = null, bool tracking = false);
}