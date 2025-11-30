namespace Marqa.Service.Services.Courses.Models;

public class MinimalCourseDataModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TeacherFullName { get; set; }
    public int MaxStudentCount { get; set; }
    public int EnrolledStudentCount { get; set; }
    public decimal CoursePrice { get; set; }
}
