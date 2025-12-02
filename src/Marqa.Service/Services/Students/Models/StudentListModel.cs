using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentListModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public decimal Balance { get; set; }
    public StudentStatus Status { get; set; }

    public class StudentCourseData
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseStatus { get; set; }
    }
}