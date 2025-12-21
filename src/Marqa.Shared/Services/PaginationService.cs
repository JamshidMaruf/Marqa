using Marqa.Shared.Models;
using Newtonsoft.Json;

namespace Marqa.Shared.Services;

public class PaginationService : IPaginationService
{
    private readonly IHttpContextService _httpContextService;

    public PaginationService(IHttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }

    public IQueryable<T> Paginate<T>(IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var metaData = new PaginationMetaData(totalCount, @params);
        var json = JsonConvert.SerializeObject(metaData);

        _httpContextService.RemoveResponseHeader("X-Pagination");
        _httpContextService.AddResponseHeader("X-Pagination", json);

        return source
            .Skip((@params.PageNumber - 1) * @params.PageSize)
            .Take(@params.PageSize);
    }
}

