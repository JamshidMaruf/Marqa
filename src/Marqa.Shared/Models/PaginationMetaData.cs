namespace Marqa.Shared.Models;

public class PaginationMetaData
{
    public PaginationMetaData(int totalCount, PaginationParams @params)
    {
        CurrentPage = @params.PageIndex;
        TotalPages = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
        TotalCount = totalCount;
        PageSize = @params.PageSize;
    }
    
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;
}