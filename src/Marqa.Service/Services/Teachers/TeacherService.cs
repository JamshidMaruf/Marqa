using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Teachers.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Teachers;

public class TeacherService : ITeacherService
{
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<Teacher> teacherRepository;
    public TeacherService()
    {
        companyRepository = new Repository<Company>();
        teacherRepository = new Repository<Teacher>();
    }

    public async Task CreateAsync(TeacherCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        await teacherRepository.InsertAsync(new Teacher
        {
            CompanyId = model.CompanyId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
        });
    }

    public async Task UpdateAsync(int id, TeacherUpdateModel model)
    {
        var existTeacher = await teacherRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Teacher is not found");

        existTeacher.FirstName = model.FirstName;
        existTeacher.LastName = model.LastName;
        existTeacher.DateOfBirth = model.DateOfBirth;
        existTeacher.Gender = model.Gender;

        await teacherRepository.UpdateAsync(existTeacher);
    }

    public async Task DeleteAsync(int id)
    {
        var existTeacher = await teacherRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Teacher is not found");

        await teacherRepository.DeleteAsync(existTeacher);
    }

    public async Task<TeacherViewModel> GetAsync(int id)
    {
        var existTeacher = await teacherRepository
            .SelectAllAsQueryable()
            .Include(t => t.Company)
            .Include(t => t.Courses)
            .ThenInclude(c => c.Subject)
            .Select(t => new TeacherViewModel
            {
                Id = t.Id,
                DateOfBirth = t.DateOfBirth,
                Gender = t.Gender,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Company = new TeacherViewModel.CompanyInfo
                {
                    Id = t.CompanyId,
                    Name = t.Company.Name,
                },
                Courses = t.Courses.Select(c => new TeacherViewModel.CourseInfo
                {
                    Id = c.Id,
                    Name = c.Name,
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                }).ToList()
            })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Teacher is not found");

        return existTeacher;
    }

    public async Task<List<TeacherViewModel>> GetAllAsync(int companyId)
    {
        return await teacherRepository
            .SelectAllAsQueryable()
            .Include(t => t.Company)
            .Include(t => t.Courses)
            .ThenInclude(c => c.Subject)
            .Select(t => new TeacherViewModel
            {
                Id = t.Id,
                DateOfBirth = t.DateOfBirth,
                Gender = t.Gender,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Company = new TeacherViewModel.CompanyInfo
                {
                    Id = t.CompanyId,
                    Name = t.Company.Name,
                },
                Courses = t.Courses.Select(c => new TeacherViewModel.CourseInfo
                {
                    Id = c.Id,
                    Name = c.Name,
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                }).ToList()
            })
            .ToListAsync();
    }
}
