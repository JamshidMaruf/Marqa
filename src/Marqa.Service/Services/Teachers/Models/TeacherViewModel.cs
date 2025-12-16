using Marqa.Domain.Enums;

public class TeacherViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GenderInfo Gender { get; set; }
    public StatusInfo Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public TeacherPayment Payment { get; set; }
    public string Info { get; set; }
    public string Qualification { get; set; }
    public IEnumerable<SubjectInfo> Subjects { get; set; }
    public IEnumerable<CourseInfo> Courses { get; set; }
    public TeacherTypeInfo TypeInfo { get; set; }
    public TeacherStatistics Statistics { get; set; }

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

    public class TeacherPayment
    {
        public int Id { get; set; }
        public TeacherPaymentType Type { get; set; }
        public string Name { get; set; }
        public decimal? FixSalary { get; set; }
        public decimal? SalaryPercentPerStudent { get; set; }
        public decimal? SalaryAmountPerHour { get; set; }
    }

    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Level { get; set; }
        public CourseStatus Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int StudentCount { get; set; }
    }

    public class TeacherTypeInfo
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class TeacherStatistics
    {
        public int TotalSubjects { get; set; }
        public int TotalCourses { get; set; }
        public int ActiveCourses { get; set; }
    }
}