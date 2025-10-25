using Marqa.DataAccess.Extensions;
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
    public DbSet<OTP> OTPs { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<StudentDetail> StudentDetails { get; set; }
    public DbSet<StudentExamResult> StudentExamResults { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<CourseWeekday> CourseWeekdays { get; set; }
    public DbSet<StudentHomeTask> StudentHomeTasks { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<LessonFile> LessonFiles { get; set; }
    public DbSet<LessonVideo> LessonVideos { get; set; }
    public DbSet<StudentHomeTaskFeedback> StudentHomeTaskFeedbacks { get; set; }
    public DbSet<StudentHomeTaskFile> StudentHomeTaskFiles { get; set; }
    public DbSet<HomeTaskFile> HomeTaskFiles { get; set; }
    public DbSet<StudentPointHistory> StudentPointHistories { get; set; }
    public DbSet<PointSystemSetting> PointSystemSettings { get; set; }
    public DbSet<PointSetting> PointSettings { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Setting> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyGlobalConfigurations();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

