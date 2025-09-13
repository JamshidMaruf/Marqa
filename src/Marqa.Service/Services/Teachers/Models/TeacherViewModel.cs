using Marqa.Domain.Entities;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherViewModel : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Specialization { get; set; }
    public TeacherStatus Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public SubjectInfo Subject { get; set; }
    public CompanyInfo Company { get; set; }
    public List<CourseInfo> Courses { get; set; }

    public class CompanyInfo
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
}
