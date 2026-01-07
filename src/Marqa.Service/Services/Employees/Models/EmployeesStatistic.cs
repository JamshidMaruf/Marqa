namespace Marqa.Service.Services.Employees.Models;

public class EmployeesStatistic
{
    public int TotalEmployeesCount { get; set; }
    public int TotalActiveEmployeesCount { get; set; }
    public int TotalInactiveEmployeesCount { get; set; }
    public int TotalOnLeaveEmployeesCount { get; set; }
    public decimal TotalSalary { get; set; }
}