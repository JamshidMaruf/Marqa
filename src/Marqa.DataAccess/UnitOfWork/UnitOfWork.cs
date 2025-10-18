using Marqa.DataAccess.Contexts;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;

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

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async Task BeginCommitAsync()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}