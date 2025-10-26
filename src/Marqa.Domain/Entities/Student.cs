using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Student : Auditable
{ 
    public string StudentAccessId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }

    public Company Company { get; set; }
    public StudentDetail StudentDetail { get; set; }
    public ICollection<StudentCourse> Courses { get; set; }
    public ICollection<StudentHomeTask> StudentHomeTasks { get; set; }
    public ICollection<StudentPointHistory> StudentPointHistories { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<StudentExamResult> ExamResults { get; set; }
}
