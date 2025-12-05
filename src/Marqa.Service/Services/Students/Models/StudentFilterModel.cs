using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentFilterModel
{
    public int CompanyId { get; set; }
    public string SearchText { get; set; } = null;
    public int? CourseId { get; set; } = null;
    public StudentFilteringStatus? Status { get; set; }
}
