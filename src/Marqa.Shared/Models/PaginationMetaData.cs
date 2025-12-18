namespace Marqa.Shared.Models;

public class PaginationMetaData
{
    public PaginationMetaData(int totalCount, PaginationParams @params)
    {
        TotalPages = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
        TotalCount = totalCount;
        CurrentPage = @params.PageIndex;
        PageSize = @params.PageSize;
    }

    public int TotalPages { get; }
    public int TotalCount { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
