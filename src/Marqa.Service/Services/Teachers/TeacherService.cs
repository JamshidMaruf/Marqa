using System.Globalization;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
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
            Phone = model.Phone,
            Email = model.Email,
            Status = model.Status,
            PasswordHash = model.Password.Hash(),
            JoiningDate = model.JoiningDate,
            Specialization = model.Specialization
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
        existTeacher.Phone = model.Phone;
        existTeacher.Email = model.Email;
        existTeacher.Status = model.Status;
        existTeacher.JoiningDate = model.JoiningDate;
        existTeacher.Specialization = model.Specialization; 

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
            .Where(c => !c.IsDeleted)
            .Include(t => t.Company)
            .Include(c => c.Subject)
            .Include(t => t.Courses)
            .ThenInclude(c => c.Subject)
            .Select(t => new TeacherViewModel
            {
                Id = t.Id,
                DateOfBirth = t.DateOfBirth,
                Gender = t.Gender,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                Status = t.Status,
                JoiningDate = t.JoiningDate,
                Specialization = t.Specialization,
                Subject = new TeacherViewModel.SubjectInfo
                {
                    Id = t.Subject.Id,
                    Name = t.Subject.Name,
                },
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

    public async Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search, int? subjectId)
    {
        var query = teacherRepository
            .SelectAllAsQueryable()
            .Where(c => !c.IsDeleted && c.CompanyId == companyId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => 
                t.FirstName.ToLower().Contains(search) || 
                t.LastName.ToLower().Contains(search) ||
                t.Specialization.ToLower().Contains(search) ||
                t.Phone.Contains(search) || 
                t.Email.Contains(search));
        }
        
        if(subjectId != null)
            query = query.Where(t => t.SubjectId == subjectId);
        
        return await query
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
                Email = t.Email,
                Phone = t.Phone,
                Status = t.Status,
                JoiningDate = t.JoiningDate,
                Specialization = t.Specialization,
                Subject = new TeacherViewModel.SubjectInfo
                {
                    Id = t.Subject.Id,
                    Name = t.Subject.Name,
                },
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
