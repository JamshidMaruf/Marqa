namespace Marqa.Service.Services.Courses.Models;

public class UpcomingCourseViewModel
{
    public int EnrolledStudentCount { get; set; }
    public int MaxStudentCount { get; set; }
    public int AvailableSeats { get; set; }
    public List<StudentData> Students { get; set; }
    
    public class StudentData
    {
        public string FullName { get; set; }
        public DateTime DateOfEnrollment { get; set; }
    }
}
