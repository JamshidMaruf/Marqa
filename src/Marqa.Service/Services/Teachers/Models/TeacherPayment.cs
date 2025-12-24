namespace Marqa.Service.Services.Teachers.Models;

public class TeacherPayment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? FixSalary { get; set; }
    public decimal? SalaryPercentPerStudent { get; set; }
    public decimal? SalaryAmountPerHour { get; set; }
}