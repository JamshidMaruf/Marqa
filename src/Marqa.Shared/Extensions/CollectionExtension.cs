using Marqa.Shared.Helpers;
using Marqa.Shared.Models;
using Newtonsoft.Json;

namespace Marqa.Shared.Extensions;

public static class CollectionExtension
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationParams paginationParams)
    {
        var totalCount = query.Count();

        var metaData = new PaginationMetaData(totalCount, paginationParams);  
        
        var json = JsonConvert.SerializeObject(metaData);
        
        HttpContextHelper.Header?.Add("X-Pagination", json);
        
        return query.Skip((paginationParams.PageIndex - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize);
    }
}