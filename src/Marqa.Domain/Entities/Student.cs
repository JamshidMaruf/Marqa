using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Student : Auditable
{ 
    public int UserId { get; set; }
    public string StudentAccessId { get; set; }
    public decimal Balance { get; set; }
    public string ImageFileName { get; set; }
    public string ImageFilePath { get; set; }
    public string ImageFileExtension { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }
    
    public Company Company { get; set; }
    public User User { get; set; }
    public StudentDetail StudentDetail { get; set; }
    public ICollection<StudentCourse> Courses { get; set; }
    public ICollection<StudentHomeTask> StudentHomeTasks { get; set; }
    public ICollection<StudentPointHistory> StudentPointHistories { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<StudentExamResult> ExamResults { get; set; }
}