using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Marqa.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IRepository<Banner> Banners { get; } = new Repository<Banner>(context);
    public IRepository<Company> Companies { get; } = new Repository<Company>(context);
    public IRepository<Course> Courses { get; } = new Repository<Course>(context);
    public IRepository<CourseWeekday> CourseWeekdays { get; } = new Repository<CourseWeekday>(context);
    public IRepository<Employee> Employees { get; } = new Repository<Employee>(context);
    public IRepository<EmployeeRole> EmployeeRoles { get; } = new Repository<EmployeeRole>(context);
    public IRepository<Student> Students { get; } = new Repository<Student>(context);
    public IRepository<StudentDetail> StudentDetails { get; } = new Repository<StudentDetail>(context);
    public IRepository<Exam> Exams { get; } = new Repository<Exam>(context);
    public IRepository<HomeTask> HomeTasks { get; } = new Repository<HomeTask>(context);
    public IRepository<Lesson> Lessons { get; } = new Repository<Lesson>(context);
    public IRepository<LessonAttendance> LessonAttendances { get; } = new Repository<LessonAttendance>(context);
    public IRepository<OTP> OTPs { get; } = new Repository<OTP>(context);
    public IRepository<Enrollment> Enrollments { get; } = new Repository<Enrollment>(context);
    public IRepository<StudentExamResult> StudentExamResults { get; } = new Repository<StudentExamResult>(context);
    public IRepository<Subject> Subjects { get; } = new Repository<Subject>(context);
    public IRepository<StudentHomeTask> StudentHomeTasks { get; } = new Repository<StudentHomeTask>(context);
    public IRepository<TeacherSubject> TeacherSubjects { get; } = new Repository<TeacherSubject>(context);
    public IRepository<LessonFile> LessonFiles { get; } = new Repository<LessonFile>(context);
    public IRepository<LessonVideo> LessonVideos { get; } = new Repository<LessonVideo>(context);
    public IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; } = new Repository<StudentHomeTaskFile>(context);
    public IRepository<HomeTaskFile> HomeTaskFiles { get; } = new Repository<HomeTaskFile>(context);
    public IRepository<StudentPointHistory> StudentPointHistories { get; } = new Repository<StudentPointHistory>(context);
    public IRepository<PointSystemSetting> PointSystemSettings { get; } = new Repository<PointSystemSetting>(context);
    public IRepository<PointSetting> PointSettings { get; } = new Repository<PointSetting>(context);
    public IRepository<Product> Products { get; } = new Repository<Product>(context);
    public IRepository<Order> Orders { get; } = new Repository<Order>(context);
    public IRepository<OrderItem> OrderItems { get; } = new Repository<OrderItem>(context);
    public IRepository<StudentPaymentOperation> StudentPaymentOperations { get; } = new Repository<StudentPaymentOperation>(context);
    public IRepository<Setting> Settings { get; } = new Repository<Setting>(context);
    public IRepository<Basket> Baskets { get; } = new Repository<Basket>(context);
    public IRepository<BasketItem> BasketItems { get; } = new Repository<BasketItem>(context);
    public IRepository<ExamSetting> ExamSettings { get; } = new Repository<ExamSetting>(context);
    public IRepository<EmployeePayment> EmployeePayments { get; } = new Repository<EmployeePayment>(context);
    public IRepository<ExamSettingItem> ExamSettingItems { get; } = new Repository<ExamSettingItem>(context);
    public IRepository<Permission> Permissions { get; }  = new Repository<Permission>(context);
    public IRepository<RolePermission> RolePermissions { get; } = new Repository<RolePermission>(context);
    public IRepository<RefreshToken> RefreshTokens { get; } = new Repository<RefreshToken>(context);
    public IRepository<User> Users { get; } = new Repository<User>(context);
    public IRepository<Asset> Assets { get; } = new Repository<Asset>(context);
    public IRepository<EnrollmentFrozen> EnrollmentFrozens => new Repository<EnrollmentFrozen>(context);
    public IRepository<EnrollmentCancellation> EnrollmentCancellations => new Repository<EnrollmentCancellation>(context);
    public IRepository<EnrollmentTransfer> EnrollmentTransfers => new Repository<EnrollmentTransfer>(context);
    public IRepository<Teacher> Teachers => new Repository<Teacher>(context);

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await context.Database.BeginTransactionAsync();
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}