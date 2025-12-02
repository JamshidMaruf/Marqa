using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentViewForUpdateModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public StudentStatus Status { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherLastName { get; set; }
    public string FatherPhone { get; set; }
    public string MotherFirstName { get; set; }
    public string MotherLastName { get; set; }
    public string MotherPhone { get; set; }
    public string GuardianFirstName { get; set; }
    public string GuardianLastName { get; set; }
    public string GuardianPhone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<StudentCourseData> Courses { get; set; }
    
    public class StudentCourseData
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseStatusId { get; set; }
        public string CourseStatusName { get; set; }
    }
}