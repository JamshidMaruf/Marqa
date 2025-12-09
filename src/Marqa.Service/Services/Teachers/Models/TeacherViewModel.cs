namespace Marqa.Service.Services.Teachers.Models;

public class TeacherViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GenderInfo Gender { get; set; }
    public string Specialization { get; set; }
    public StatusInfo Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public decimal Amount { get; set; }
    public string Info { get; set; }
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
