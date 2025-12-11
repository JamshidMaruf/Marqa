using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

public interface IUnitOfWork : IDisposable
{
    // 1. User and Authentication
    IRepository<User> Users { get; }
    IRepository<RefreshToken> RefreshTokens { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<RolePermission> RolePermissions { get; }

    // 2. Employee and Staff Management
    IRepository<Employee> Employees { get; }
    IRepository<EmployeeRole> EmployeeRoles { get; }
    IRepository<EmployeePaymentOperation> EmployeePaymentOperations { get; }

    // 3. Teacher Management
    IRepository<Teacher> Teachers { get; }
    IRepository<TeacherSubject> TeacherSubjects { get; }
    IRepository<TeacherPaymentOperation> TeacherPaymentOperations { get; }

    // 4. Student Management
    IRepository<Student> Students { get; }
    IRepository<StudentDetail> StudentDetails { get; }
    IRepository<StudentPaymentOperation> StudentPaymentOperations { get; }
    IRepository<StudentPointHistory> StudentPointHistories { get; }

    // 5. Course and Learning
    IRepository<Course> Courses { get; }
    IRepository<Subject> Subjects { get; }
    IRepository<Lesson> Lessons { get; }
    IRepository<CourseWeekday> CourseWeekdays { get; }

    // 6. Enrollment and Registration
    IRepository<Enrollment> Enrollments { get; }
    IRepository<EnrollmentFrozen> EnrollmentFrozens { get; }
    IRepository<EnrollmentCancellation> EnrollmentCancellations { get; }
    IRepository<EnrollmentTransfer> EnrollmentTransfers { get; }

    // 7. Lessons and Attendance
    IRepository<LessonAttendance> LessonAttendances { get; }
    IRepository<LessonFile> LessonFiles { get; }
    IRepository<LessonVideo> LessonVideos { get; }

    // 8. Exams and Results
    IRepository<Exam> Exams { get; }
    IRepository<ExamSetting> ExamSettings { get; }
    IRepository<ExamSettingItem> ExamSettingItems { get; }
    IRepository<StudentExamResult> StudentExamResults { get; }

    // 9. Home Tasks and Assignments
    IRepository<HomeTask> HomeTasks { get; }
    IRepository<HomeTaskFile> HomeTaskFiles { get; }
    IRepository<StudentHomeTask> StudentHomeTasks { get; }
    IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; }

    // 10. Points and Rewards System
    IRepository<PointSystemSetting> PointSystemSettings { get; }
    IRepository<PointSetting> PointSettings { get; }

    // 11. E-commerce (Shop and Orders)
    IRepository<Product> Products { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<Basket> Baskets { get; }
    IRepository<BasketItem> BasketItems { get; }

    // 12. Finance and Expenses
    IRepository<Expense> Expenses { get; }
    IRepository<ExpenseCategory> ExpenseCategories { get; }

    // 13. Company and Settings
    IRepository<Company> Companies { get; }
    IRepository<Setting> Settings { get; }

    // 14. Content and Media
    IRepository<Banner> Banners { get; }
    IRepository<Asset> Assets { get; }

    // 15. Security and OTP
    IRepository<OTP> OTPs { get; }

    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}