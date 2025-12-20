﻿using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Marqa.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// 1. User and Authentication
    public IRepository<User> Users { get; } = new Repository<User>(context);
    public IRepository<RefreshToken> RefreshTokens { get; } = new Repository<RefreshToken>(context);
    public IRepository<Permission> Permissions { get; } = new Repository<Permission>(context);
    public IRepository<RolePermission> RolePermissions { get; } = new Repository<RolePermission>(context);

    /// 2. Employee and Staff Management
    public IRepository<Employee> Employees { get; } = new Repository<Employee>(context);
    public IRepository<EmployeeRole> EmployeeRoles { get; } = new Repository<EmployeeRole>(context);
    public IRepository<EmployeePaymentOperation> EmployeePaymentOperations { get; } = new Repository<EmployeePaymentOperation>(context);

    // 3. Teacher Management
    public IRepository<Teacher> Teachers { get; } = new Repository<Teacher>(context);
    public IRepository<TeacherPaymentOperation> TeacherPaymentOperations { get; } = new Repository<TeacherPaymentOperation>(context);
    public IRepository<TeacherAssessment> TeacherAssessments { get; } = new Repository<TeacherAssessment>(context);

    // 4. Student Management
    public IRepository<Student> Students { get; } = new Repository<Student>(context);
    public IRepository<StudentDetail> StudentDetails { get; } = new Repository<StudentDetail>(context);
    public IRepository<StudentPaymentOperation> StudentPaymentOperations { get; } = new Repository<StudentPaymentOperation>(context);
    public IRepository<StudentPointHistory> StudentPointHistories { get; } = new Repository<StudentPointHistory>(context);

    // 5. Course and Learning
    public IRepository<Course> Courses { get; } = new Repository<Course>(context);
    public IRepository<Lesson> Lessons { get; } = new Repository<Lesson>(context);
    public IRepository<CourseWeekday> CourseWeekdays { get; } = new Repository<CourseWeekday>(context);
    public IRepository<CourseTeacher> CourseTeachers { get; } = new Repository<CourseTeacher>(context);


    // 6. Enrollment and Registration
    public IRepository<Enrollment> Enrollments { get; } = new Repository<Enrollment>(context);
    public IRepository<EnrollmentFrozen> EnrollmentFrozens { get; } = new Repository<EnrollmentFrozen>(context);
    public IRepository<EnrollmentCancellation> EnrollmentCancellations { get; } = new Repository<EnrollmentCancellation>(context);
    public IRepository<EnrollmentTransfer> EnrollmentTransfers { get; } = new Repository<EnrollmentTransfer>(context);

    // 7. Lessons and Attendance
    public IRepository<LessonAttendance> LessonAttendances { get; } = new Repository<LessonAttendance>(context);
    public IRepository<LessonFile> LessonFiles { get; } = new Repository<LessonFile>(context);
    public IRepository<LessonVideo> LessonVideos { get; } = new Repository<LessonVideo>(context);
    public IRepository<LessonTeacher> LessonTeachers { get; } = new Repository<LessonTeacher>(context);

    // 8. Exams and Results
    public IRepository<Exam> Exams { get; } = new Repository<Exam>(context);
    public IRepository<ExamSetting> ExamSettings { get; } = new Repository<ExamSetting>(context);
    public IRepository<ExamSettingItem> ExamSettingItems { get; } = new Repository<ExamSettingItem>(context);
    public IRepository<StudentExamResult> StudentExamResults { get; } = new Repository<StudentExamResult>(context);

    // 9. Home Tasks and Assignments
    public IRepository<HomeTask> HomeTasks { get; } = new Repository<HomeTask>(context);
    public IRepository<HomeTaskFile> HomeTaskFiles { get; } = new Repository<HomeTaskFile>(context);
    public IRepository<StudentHomeTask> StudentHomeTasks { get; } = new Repository<StudentHomeTask>(context);
    public IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; } = new Repository<StudentHomeTaskFile>(context);

    // 10. Points and Rewards System
    public IRepository<PointSystemSetting> PointSystemSettings { get; } = new Repository<PointSystemSetting>(context);
    public IRepository<PointSetting> PointSettings { get; } = new Repository<PointSetting>(context);

    // 11. E-commerce (Shop and Orders)
    public IRepository<Product> Products { get; } = new Repository<Product>(context);
    public IRepository<Order> Orders { get; } = new Repository<Order>(context);
    public IRepository<OrderItem> OrderItems { get; } = new Repository<OrderItem>(context);
    public IRepository<Basket> Baskets { get; } = new Repository<Basket>(context);
    public IRepository<BasketItem> BasketItems { get; } = new Repository<BasketItem>(context);

    // 12. Finance and Expenses
    public IRepository<Expense> Expenses { get; } = new Repository<Expense>(context);
    public IRepository<ExpenseCategory> ExpenseCategories { get; } = new Repository<ExpenseCategory>(context);

    // 13. Company and Settings
    public IRepository<Company> Companies { get; } = new Repository<Company>(context);
    public IRepository<Setting> Settings { get; } = new Repository<Setting>(context);

    // 14. Content and Media
    public IRepository<Banner> Banners { get; } = new Repository<Banner>(context);
    public IRepository<Asset> Assets { get; } = new Repository<Asset>(context);

    // 15. Security and OTP
    public IRepository<OTP> OTPs { get; } = new Repository<OTP>(context);



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