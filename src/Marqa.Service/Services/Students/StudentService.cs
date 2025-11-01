using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Students.Models;
using Marqa.Service.Services.Students.Models.DetailModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Students;


public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    public async Task CreateAsync(StudentCreateModel model)
    {
        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        _ = await unitOfWork.Students
            .SelectAsync(e => e.Phone == model.Phone && e.CompanyId == model.CompanyId)
           ?? throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");
        
        _ = await unitOfWork.StudentDetails
            .SelectAsync(e => (e.FatherPhone == model.StudentDetailCreateModel.FatherPhone ||
             e.MotherPhone == model.StudentDetailCreateModel.MotherPhone ||
             e.GuardianPhone == model.StudentDetailCreateModel.GuardianPhone) 
             && e.CompanyId == model.CompanyId)
           ?? throw new AlreadyExistException($"this phone {model.Phone} already exists");

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var createdStudent = new Student()
            {
                CompanyId = model.CompanyId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
            };

            unitOfWork.Students.Insert(createdStudent);

            await unitOfWork.SaveAsync();


            unitOfWork.StudentDetails.Insert(new StudentDetail
            {
                StudentId = createdStudent.Id,
                FatherFirstName = model.StudentDetailCreateModel.FatherFirstName,
                FatherLastName = model.StudentDetailCreateModel.FatherLastName,
                FatherPhone = model.StudentDetailCreateModel.FatherPhone,
                MotherFirstName = model.StudentDetailCreateModel.MotherFirstName,
                MotherLastName = model.StudentDetailCreateModel.MotherLastName,
                MotherPhone = model.StudentDetailCreateModel.MotherPhone,
                GuardianFirstName = model.StudentDetailCreateModel.GuardianFirstName,
                GuardianLastName = model.StudentDetailCreateModel.GuardianLastName,
                GuardianPhone = model.StudentDetailCreateModel.GuardianPhone
            });

            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, int companyId, StudentUpdateModel model)
    {
        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var existStudent = await unitOfWork.Students
                .SelectAsync(predicate: s => s.Id == id, includes: new[] { "StudentDetail" })
                ?? throw new NotFoundException($"Student is not found");

            _ = await unitOfWork.Students
                    .SelectAsync(e => e.Phone == model.Phone && e.CompanyId == companyId)
                   ?? throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");

            _ = await unitOfWork.StudentDetails
                .SelectAsync(e => (e.FatherPhone == model.StudentDetailUpdateModel.FatherPhone ||
                 e.MotherPhone == model.StudentDetailUpdateModel.MotherPhone ||
                 e.GuardianPhone == model.StudentDetailUpdateModel.GuardianPhone)
                 && e.CompanyId == companyId)
               ?? throw new AlreadyExistException($"this phone {model.Phone} already exists");

            existStudent.FirstName = model.FirstName;
            existStudent.LastName = model.LastName;
            existStudent.Gender = model.Gender;
            existStudent.DateOfBirth = model.DateOfBirth;
            existStudent.Phone = model.Phone;
            existStudent.Email = model.Email;

            existStudent.StudentDetail.FatherFirstName = model.StudentDetailUpdateModel.FatherFirstName;
            existStudent.StudentDetail.FatherLastName = model.StudentDetailUpdateModel.FatherLastName;
            existStudent.StudentDetail.FatherPhone = model.StudentDetailUpdateModel.FatherPhone;
            existStudent.StudentDetail.MotherFirstName = model.StudentDetailUpdateModel.MotherFirstName;
            existStudent.StudentDetail.MotherLastName = model.StudentDetailUpdateModel.MotherLastName;
            existStudent.StudentDetail.MotherPhone = model.StudentDetailUpdateModel.MotherPhone;
            existStudent.StudentDetail.GuardianFirstName = model.StudentDetailUpdateModel.GuardianFirstName;
            existStudent.StudentDetail.GuardianLastName = model.StudentDetailUpdateModel.GuardianLastName;
            existStudent.StudentDetail.GuardianPhone = model.StudentDetailUpdateModel.GuardianPhone;

            unitOfWork.Students.Update(existStudent);
            await unitOfWork.SaveAsync();


            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    
    public async Task DeleteAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(predicate: s => s.Id == id, includes: new[] { "StudentDetail" })
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail != null)
        {
            unitOfWork.StudentDetails.Delete(existStudent.StudentDetail);
        }

        unitOfWork.Students.Delete(existStudent);
        await unitOfWork.SaveAsync();
    }

    public async Task<int?> GetByPhoneAsync(string phone)
    {
        var employee = await unitOfWork.Employees.SelectAsync(emp => emp.Phone == phone);

        return employee?.Id;
    }

    public async Task<int?> GetStudentParentByPhoneAsync(string phone)
    {
        var studentDetail = await unitOfWork.StudentDetails
            .SelectAsync(sd => sd.FatherPhone == phone ||
                        sd.MotherPhone == phone ||
                        sd.GuardianPhone == phone);

        if(studentDetail == null)
            return null;
        else
            return studentDetail.Id;
    }

    public async Task<StudentViewModel> GetAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(predicate: t => t.Id == id, includes: new[] { "StudentDetail" })
            ?? throw new NotFoundException($"Student is not found");

        return new StudentViewModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.FirstName,
            LastName = existStudent.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Phone = existStudent.Phone,
            Email = existStudent.Email,
            StudentDetailViewModel = new StudentDetailViewModel
            {
                FatherFirstName = existStudent.StudentDetail.FatherFirstName,
                FatherLastName = existStudent.StudentDetail.FatherLastName,
                FatherPhone = existStudent.StudentDetail.FatherPhone,
                MotherFirstName = existStudent.StudentDetail.MotherFirstName,
                MotherLastName = existStudent.StudentDetail.MotherLastName,
                MotherPhone = existStudent.StudentDetail.MotherPhone,
                RelativeFirstName = existStudent.StudentDetail.GuardianFirstName,
                RelativeLastName = existStudent.StudentDetail.GuardianLastName,
                RelativePhone = existStudent.StudentDetail.GuardianPhone
            }
        };
    }

    public async Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId)
    {
        var students = await unitOfWork.Courses
            .SelectAllAsQueryable()
            .Where(c => c.Id == courseId && !c.IsDeleted)
            .SelectMany(c => c.StudentCourses)
            .Select(sc => new StudentViewModel
            {
                Id = sc.Student.Id,
                FirstName = sc.Student.FirstName,
                LastName = sc.Student.LastName,
                DateOfBirth = sc.Student.DateOfBirth,
                Gender = sc.Student.Gender,
                Phone = sc.Student.Phone,
                Email = sc.Student.Email,
                StudentDetailViewModel = new StudentDetailViewModel
                {
                    Id = sc.Student.StudentDetail.Id,
                    StudentId = sc.Student.Id,
                    FatherFirstName = sc.Student.StudentDetail.FatherFirstName,
                    FatherLastName = sc.Student.StudentDetail.FatherLastName,
                    FatherPhone = sc.Student.StudentDetail.FatherPhone,
                    MotherFirstName = sc.Student.StudentDetail.MotherFirstName,
                    MotherLastName = sc.Student.StudentDetail.MotherLastName,
                    MotherPhone = sc.Student.StudentDetail.MotherPhone,
                    RelativeFirstName = sc.Student.StudentDetail.GuardianFirstName,
                    RelativeLastName = sc.Student.StudentDetail.GuardianLastName,
                    RelativePhone = sc.Student.StudentDetail.GuardianPhone
                }
            })
            .ToListAsync();

        if (!students.Any())
            throw new NotFoundException("Course is not found or has no students");

        return students;
    }
    public async Task<string> UploadProfilePictureAsync(long studentId, IFormFile picture)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
           ?? throw new NotFoundException($"Student not found (ID: {studentId})");

        // Fayl formatini tekshirish
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(picture.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentIsNotValidException("Only image files are allowed (jpg, jpeg, png, webp)");

        // Fayl hajmini tekshirish (masalan, max 5MB)
        if (picture.Length > 5 * 1024 * 1024)
            throw new ArgumentIsNotValidException("File size must not exceed 5MB");

        // Eski rasmni o'chirish (agar mavjud bo'lsa)
        if (!string.IsNullOrEmpty(student.ProfilePicture))
        {
            var oldFilePath = Path.Combine("wwwroot", student.ProfilePicture.TrimStart('/'));
            if (File.Exists(oldFilePath))
                File.Delete(oldFilePath);
        }

        // Yangi fayl nomi generatsiya qilish
        var fileName = $"{studentId}_{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine("wwwroot", "uploads", "students", "profiles");

        // Papka mavjud emasligini tekshirish va yaratish
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, fileName);

        // Faylni saqlash
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await picture.CopyToAsync(stream);
        }

        // Database uchun nisbiy path
        var relativePath = $"/uploads/students/profiles/{fileName}";
        student.ProfilePicture = relativePath;

        unitOfWork.Students.Update(student);
        await unitOfWork.SaveAsync();

        return relativePath;
    }
}