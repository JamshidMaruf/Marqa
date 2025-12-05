using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Services.Students.Models;

public class StudentUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<StudentCourseData> Courses { get; set; }

    //details
    public StudentDetailUpdateModel StudentDetailUpdateModel { get; set; }
    
    public class StudentCourseData
    {
        public int? CourseId { get; set; }
        public int? CourseStatusId { get; set; }
    }
}