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

    Task SaveAsync();
    Task BeginTransactionAsync();
    Task BeginCommitAsync();
    Task RollbackTransactionAsync();
}