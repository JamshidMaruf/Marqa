using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class TeacherSalary : Auditable
{
    public int TeacherId { get; set; }
    public decimal? FixSalary { get; set; }
    public decimal? SalaryPercentPerStudent { get; set; }
    public decimal? SalaryAmountPerHour { get; set; }
    public TeacherSalaryType SalaryType { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public short ForYear { get; set; }
    public Month ForMonth { get; set; }
}
