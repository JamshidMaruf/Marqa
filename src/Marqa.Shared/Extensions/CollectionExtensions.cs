using Marqa.Shared.Helpers;
using Marqa.Shared.Models;
using Newtonsoft.Json;

namespace Marqa.Service.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var metaData = new PaginationMetaData(totalCount, @params);

        var jsonObject = JsonConvert.SerializeObject(metaData);

        HttpContextHelper.Header.Add("X-Pagination", jsonObject);

        return source.Skip((@params.PageIndex - 1) * @params.PageSize)
            .Take(@params.PageSize);
    }
}
