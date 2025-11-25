namespace Marqa.Service.Services.Teachers.Models;

public class TeacherTableViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public GenderInfo Gender { get; set; }
    public StatusInfo Status { get; set; }
    public IEnumerable<SubjectInfo> Subjects { get; set; }
    public IEnumerable<CourseInfo> Courses { get; set; }
    public RoleInfo Role { get; set; }

    public class GenderInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StatusInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    public class RoleInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
