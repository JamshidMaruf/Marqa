using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Student : Auditable
{ 
    public string StudentAccessId { get; set; }
    public decimal Balance { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public StudentStatus Status { get; set; }
    public int UserId { get; set; }
    public int? AssetId { get; set; }
    
    public User User { get; set; }
    public StudentDetail StudentDetail { get; set; }
    public ICollection<Enrollment> Courses { get; set; }
    public ICollection<StudentHomeTask> StudentHomeTasks { get; set; }
    public ICollection<StudentPointHistory> PointHistories { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<StudentExamResult> ExamResults { get; set; }
    public ICollection<StudentPaymentOperation> PaymentOperations { get; set; }
    public Asset Asset { get; set; }
    public ICollection<TeacherAssessment> TeacherAssessments { get; set; }

}