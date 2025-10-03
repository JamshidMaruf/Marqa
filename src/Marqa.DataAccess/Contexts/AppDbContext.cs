using Marqa.DataAccess.EntityConfigurations;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Company> Companies { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<HomeTask> HomeTasks { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonAttendance> LessonAttendances { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<StudentDetail> StudentDetails { get; set; }
    public DbSet<StudentExamResult> StudentExams { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<CourseWeekday> CourseWeekdays { get; set; }
    public DbSet<StudentHomeTask> StudentHomeTasks { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentDetailConfiguration());
    }
}
