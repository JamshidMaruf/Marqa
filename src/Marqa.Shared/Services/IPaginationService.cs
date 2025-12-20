using Marqa.Shared.Models;

namespace Marqa.Shared.Services;

/// <summary>
/// Service for pagination operations.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Paginates a queryable and adds pagination metadata to response headers.
    /// </summary>
    IQueryable<T> Paginate<T>(IQueryable<T> source, PaginationParams @params);
}

