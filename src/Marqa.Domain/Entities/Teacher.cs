using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Teacher : Auditable
{
    public int UserId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Qualification { get; set; }
    public string Info { get; set; }
    public TeacherType Type { get; set; }   
    public TeacherStatus Status { get; set; }
    
    // Salary
    public decimal? FixSalary { get; set; }
    public decimal? SalaryPercentPerStudent {  get; set; }
    public decimal? SalaryAmountPerHour { get; set; }
    public TeacherPaymentType PaymentType { get; set; }
    
    // Navigation
    public User User { get; set; }
    public ICollection<Course> Courses { get; set; }
    public ICollection<TeacherSubject> TeacherSubjects { get; set; }
}