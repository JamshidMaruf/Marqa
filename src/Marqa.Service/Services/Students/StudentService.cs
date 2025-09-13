using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Students.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Students;

public class StudentService : IStudentService
{
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<Student> studentRepository;
    private readonly IRepository<StudentCourse> studentCourseRepository;
    public StudentService()
    {
        companyRepository = new Repository<Company>();
        studentRepository = new Repository<Student>();
        studentCourseRepository = new Repository<StudentCourse>();
    }

    public async Task CreateAsync(StudentCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        await studentRepository.InsertAsync(new Student
        {
            CompanyId = model.CompanyId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
        });
    }

    public async Task UpdateAsync(int id, StudentUpdateModel model)
    {
        var existStudent = await studentRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Student is not found");

        existStudent.FirstName = model.FirstName;
        existStudent.LastName = model.LastName;
        existStudent.Gender = model.Gender;
        existStudent.DateOfBirth = model.DateOfBirth;

        await studentRepository.UpdateAsync(existStudent);
    }

    public async Task DeleteAsync(int id)
    {
        var existStudent = await studentRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Student is not found");

        await studentRepository.DeleteAsync(existStudent);
    }

    public async Task<StudentViewModel> GetAsync(int id)
    {
        var existStudent = await studentRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Student is not found");

        var courses = await studentCourseRepository
            .SelectAllAsQueryable()
            .Include(t => t.Course)
            .Where(x => x.StudentId == id)
            .Select(t => new StudentViewModel.CourseInfo
            {
                Id = t.Id,  
                Name = t.Course.Name,
            })
            .ToListAsync();

        return new StudentViewModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.FirstName,
            LastName = existStudent.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Courses = courses
        };
    }

    public async Task<List<StudentViewModel>> GetAllAsync(int companyId)
    {
        return await studentRepository
            .SelectAllAsQueryable()
            .Where(t => t.CompanyId == companyId)
            .Select(t => new StudentViewModel
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                DateOfBirth = t.DateOfBirth,
                Gender = t.Gender,
            })
            .ToListAsync();
    }
}
