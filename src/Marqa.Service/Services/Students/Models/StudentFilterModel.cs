using Marqa.Domain.Enums;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Students.Models;

public class StudentFilterModel
{
    public PaginationParams PaginationParams { get; set; }
    public int CompanyId { get; set; }
    public string SearchText { get; set; } = null;
    public int? CourseId { get; set; } = null;
    public StudentFilteringStatus? Status { get; set; }
}
