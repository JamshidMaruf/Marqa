using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Student : BaseEntity
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}
