using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Qualification { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Info { get; set; }
    public TeacherType Type { get; set; }
    public TeacherStatus Status { get; set; }

    //Salary
    public decimal? FixSalary { get; set; }
    public decimal? SalaryPercentPerStudent { get; set; }
    public decimal? SalaryAmountPerHour { get; set; }
    public TeacherPaymentType PaymentType { get; set; }

    public DateOnly JoiningDate { get; set; }
    public List<int> SubjectIds { get; set; }
}
