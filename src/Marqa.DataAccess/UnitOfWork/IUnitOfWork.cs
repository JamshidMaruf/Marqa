using System.Threading.Tasks;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Marqa.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // 1. Management and Settings
    IRepository<Banner> Banners { get; }
    IRepository<Company> Companies { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<RolePermission> RolePermissions { get; }
    IRepository<Setting> Settings { get; }

    // 2. People and Personal Information
    IRepository<Employee> Employees { get; }
    IRepository<EmployeeRole> EmployeeRoles { get; }
    IRepository<RefreshToken> RefreshTokens { get; }
    IRepository<Student> Students { get; }
    IRepository<StudentDetail> StudentDetails { get; }
    IRepository<Teacher> Teachers { get; }
    IRepository<User> Users { get; }

    // 3. Courses and Learning Process
    IRepository<Course> Courses { get; }
    IRepository<CourseWeekday> CourseWeekdays { get; }
    IRepository<Enrollment> Enrollments { get; }
    IRepository<Exam> Exams { get; }
    IRepository<Lesson> Lessons { get; }
    IRepository<LessonAttendance> LessonAttendances { get; }
    IRepository<Subject> Subjects { get; }
    IRepository<TeacherSubject> TeacherSubjects { get; }

    // 4. Learning Components and Materials
    IRepository<Asset> Assets { get; }
    IRepository<ExamSetting> ExamSettings { get; }
    IRepository<ExamSettingItem> ExamSettingItems { get; }
    IRepository<HomeTask> HomeTasks { get; }
    IRepository<HomeTaskFile> HomeTaskFiles { get; }
    IRepository<LessonFile> LessonFiles { get; }
    IRepository<LessonVideo> LessonVideos { get; }
    IRepository<OTP> OTPs { get; }
    IRepository<StudentExamResult> StudentExamResults { get; }
    IRepository<StudentHomeTask> StudentHomeTasks { get; }
    IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; }

    // 5. Enrollment and Payment Operations
    IRepository<Basket> Baskets { get; }
    IRepository<BasketItem> BasketItems { get; }
    IRepository<EmployeePaymentOperation> EmployeePaymentOperations { get; }
    IRepository<EnrollmentCancellation> EnrollmentCancellations { get; }
    IRepository<EnrollmentFrozen> EnrollmentFrozens { get; }
    IRepository<EnrollmentTransfer> EnrollmentTransfers { get; }
    IRepository<StudentPaymentOperation> StudentPaymentOperations { get; }
    IRepository<TeacherPaymentOperation> TeacherPaymentOperations { get; }

    // 6. Points, Pricing and Financial System
    IRepository<Expense> Expenses { get; }
    IRepository<ExpenseCategory> ExpenseCategories { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<PointSetting> PointSettings { get; }
    IRepository<PointSystemSetting> PointSystemSettings { get; }
    IRepository<Product> Products { get; }
    IRepository<StudentPointHistory> StudentPointHistories { get; }

    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}