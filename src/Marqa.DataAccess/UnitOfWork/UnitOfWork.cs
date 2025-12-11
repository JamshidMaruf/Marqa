using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Marqa.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    // 1. Management and Settings
    public IRepository<Banner> Banners { get; } = new Repository<Banner>(context);
    public IRepository<Company> Companies { get; } = new Repository<Company>(context);
    public IRepository<Permission> Permissions { get; } = new Repository<Permission>(context);
    public IRepository<RolePermission> RolePermissions { get; } = new Repository<RolePermission>(context);
    public IRepository<Setting> Settings { get; } = new Repository<Setting>(context);
    
    // 2. People and Personal Information
    public IRepository<Employee> Employees { get; } = new Repository<Employee>(context);
    public IRepository<EmployeeRole> EmployeeRoles { get; } = new Repository<EmployeeRole>(context);
    public IRepository<RefreshToken> RefreshTokens { get; } = new Repository<RefreshToken>(context);
    public IRepository<Student> Students { get; } = new Repository<Student>(context);
    public IRepository<StudentDetail> StudentDetails { get; } = new Repository<StudentDetail>(context);
    public IRepository<Teacher> Teachers { get; } = new Repository<Teacher>(context);
    public IRepository<User> Users { get; } = new Repository<User>(context);
    
    // 3. Courses and Learning Process
    public IRepository<Course> Courses { get; } = new Repository<Course>(context);
    public IRepository<CourseWeekday> CourseWeekdays { get; } = new Repository<CourseWeekday>(context);
    public IRepository<Enrollment> Enrollments { get; } = new Repository<Enrollment>(context);
    public IRepository<Exam> Exams { get; } = new Repository<Exam>(context);
    public IRepository<Lesson> Lessons { get; } = new Repository<Lesson>(context);
    public IRepository<LessonAttendance> LessonAttendances { get; } = new Repository<LessonAttendance>(context);
    public IRepository<Subject> Subjects { get; } = new Repository<Subject>(context);
    public IRepository<TeacherSubject> TeacherSubjects { get; } = new Repository<TeacherSubject>(context);
    
    // 4. Learning Components and Materials
    public IRepository<Asset> Assets { get; } = new Repository<Asset>(context);
    public IRepository<ExamSetting> ExamSettings { get; } = new Repository<ExamSetting>(context);
    public IRepository<ExamSettingItem> ExamSettingItems { get; } = new Repository<ExamSettingItem>(context);
    public IRepository<HomeTask> HomeTasks { get; } = new Repository<HomeTask>(context);
    public IRepository<HomeTaskFile> HomeTaskFiles { get; } = new Repository<HomeTaskFile>(context);
    public IRepository<LessonFile> LessonFiles { get; } = new Repository<LessonFile>(context);
    public IRepository<LessonVideo> LessonVideos { get; } = new Repository<LessonVideo>(context);
    public IRepository<OTP> OTPs { get; } = new Repository<OTP>(context);
    public IRepository<StudentExamResult> StudentExamResults { get; } = new Repository<StudentExamResult>(context);
    public IRepository<StudentHomeTask> StudentHomeTasks { get; } = new Repository<StudentHomeTask>(context);
    public IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; } = new Repository<StudentHomeTaskFile>(context);
    
    // 5. Enrollment and Payment Operations
    public IRepository<Basket> Baskets { get; } = new Repository<Basket>(context);
    public IRepository<BasketItem> BasketItems { get; } = new Repository<BasketItem>(context);
    public IRepository<EmployeePaymentOperation> EmployeePaymentOperations { get; } = new Repository<EmployeePaymentOperation>(context);
    public IRepository<EnrollmentCancellation> EnrollmentCancellations { get; } = new Repository<EnrollmentCancellation>(context);
    public IRepository<EnrollmentFrozen> EnrollmentFrozens { get; } = new Repository<EnrollmentFrozen>(context);
    public IRepository<EnrollmentTransfer> EnrollmentTransfers { get; } = new Repository<EnrollmentTransfer>(context);
    public IRepository<StudentPaymentOperation> StudentPaymentOperations { get; } = new Repository<StudentPaymentOperation>(context);
    public IRepository<TeacherPaymentOperation> TeacherPaymentOperations { get; } // TODO: Add implementation
    
    // 6. Points, Pricing and Financial System
    public IRepository<Expense> Expenses { get; } // TODO: Add implementation
    public IRepository<ExpenseCategory> ExpenseCategories { get; } // TODO: Add implementation
    public IRepository<Order> Orders { get; } = new Repository<Order>(context);
    public IRepository<OrderItem> OrderItems { get; } = new Repository<OrderItem>(context);
    public IRepository<PointSetting> PointSettings { get; } = new Repository<PointSetting>(context);
    public IRepository<PointSystemSetting> PointSystemSettings { get; } = new Repository<PointSystemSetting>(context);
    public IRepository<Product> Products { get; } = new Repository<Product>(context);
    public IRepository<StudentPointHistory> StudentPointHistories { get; } = new Repository<StudentPointHistory>(context);

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