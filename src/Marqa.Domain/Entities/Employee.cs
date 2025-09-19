using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Teacher : Auditable
{
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public TeacherStatus Status { get; set; }
    public int SubjectId { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
    public string Info { get; set; }
    
    // Navigation
    public Company Company { get; set; }
    public Subject Subject { get; set; }
    public ICollection<Course> Courses { get; set; }
}
