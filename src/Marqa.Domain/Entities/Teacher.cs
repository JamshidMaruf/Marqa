using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Teacher : BaseEntity
{
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    // Navigation
    public Company Company { get; set; }
    public ICollection<Course> Courses { get; set; }
}
