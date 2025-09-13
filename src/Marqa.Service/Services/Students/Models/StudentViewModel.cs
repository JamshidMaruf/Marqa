using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public List<CourseInfo> Courses { get; set; }

    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
