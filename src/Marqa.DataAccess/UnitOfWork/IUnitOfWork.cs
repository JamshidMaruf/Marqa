using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;

namespace Marqa.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Banner> Banners { get; }
   
    IRepository<Company> Companies { get; }
    
    IRepository<Course> Courses { get; }
   
    IRepository<CourseWeekday> CourseWeekdays { get; }
    
    IRepository<Employee> Employees { get; }
    
    IRepository<EmployeeRole> EmployeeRoles { get; }
   
    IRepository<Student> Students { get; }
   
    IRepository<StudentDetail> StudentDetails { get; }
   
    IRepository<Exam> Exams { get; }
   
    IRepository<HomeTask> HomeTasks { get; } 
   
    IRepository<Lesson> Lessons { get; } 
   
    IRepository<LessonAttendance> LessonAttendances { get; } 
   
    IRepository<OTP> OTPs { get; } 
   
    IRepository<StudentCourse> StudentCourses { get; }
   
    IRepository<StudentExamResult> StudentExamResults { get; }
   
    IRepository<Subject> Subjects { get; } 
   
    IRepository<StudentHomeTask> StudentHomeTasks { get; } 
   
    IRepository<TeacherSubject> TeacherSubjects { get; } 
   
    IRepository<LessonFile> LessonFiles { get; }
   
    IRepository<LessonVideo> LessonVideos { get; } 
   
    IRepository<StudentHomeTaskFeedback> StudentHomeTaskFeedbacks { get; }
   
    IRepository<StudentHomeTaskFile> StudentHomeTaskFiles { get; } 
   
    IRepository<HomeTaskFile> HomeTaskFiles { get; } 
   
    IRepository<StudentPointHistory> StudentPointHistories { get; } 
   
    IRepository<PointSystemSetting> PointSystemSettings { get; }

    IRepository<PointSetting> PointSettings { get; } 

    IRepository<Product> Products { get; }

    IRepository<Order> Orders { get; }

    IRepository<OrderItem> OrderItems { get; }
    IRepository<Setting> Settings { get; }
    IRepository<Basket> Baskets { get; }
    IRepository<BasketItem> BasketItems { get; }

    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackTransactionAsync();
}