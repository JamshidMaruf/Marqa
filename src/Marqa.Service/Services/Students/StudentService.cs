using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Students.Models;
using Marqa.Service.Services.Students.Models.DetailModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Students;

public class StudentService(
    IUnitOfWork unitOfWork,
    IValidator<StudentCreateModel> createValidator,
    IValidator<StudentUpdateModel> updateValidator) : IStudentService
{
    public async Task CreateAsync(StudentCreateModel model)
    {
        await createValidator.EnsureValidatedAsync(model);

        var company = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId);
        if (company == null)
            throw new NotFoundException("Company not found");

        var existingStudent = await unitOfWork.Students
            .SelectAsync(e => e.User.Phone == model.Phone && e.CompanyId == model.CompanyId);

        if (existingStudent != null)
            throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");

        var studentPhoneResult = model.Phone.TrimPhoneNumber();
        if (!studentPhoneResult.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var fatherPhoneResult = model.StudentDetailCreateModel.FatherPhone.TrimPhoneNumber();
        var motherPhoneResult = model.StudentDetailCreateModel.MotherPhone.TrimPhoneNumber();
        var guardianPhoneResult = model.StudentDetailCreateModel.GuardianPhone.TrimPhoneNumber();

        bool anyParentPhoneValid =
            fatherPhoneResult.IsSuccessful ||
            motherPhoneResult.IsSuccessful ||
            guardianPhoneResult.IsSuccessful;

        if (!anyParentPhoneValid)
            throw new ArgumentIsNotValidException("At least one parent phone number must be valid");

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var createdStudent = new Student()
            {
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = studentPhoneResult.Phone,
                    Email = model.Email,
                },
                CompanyId = model.CompanyId,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender
            };

            unitOfWork.Students.Insert(createdStudent);
            await unitOfWork.SaveAsync();

            unitOfWork.StudentDetails.Insert(new StudentDetail
            {
                StudentId = createdStudent.Id,
                FatherFirstName = model.StudentDetailCreateModel.FatherFirstName,
                FatherLastName = model.StudentDetailCreateModel.FatherLastName,
                FatherPhone = fatherPhoneResult.Phone,
                MotherFirstName = model.StudentDetailCreateModel.MotherFirstName,
                MotherLastName = model.StudentDetailCreateModel.MotherLastName,
                MotherPhone = motherPhoneResult.Phone,
                GuardianFirstName = model.StudentDetailCreateModel.GuardianFirstName,
                GuardianLastName = model.StudentDetailCreateModel.GuardianLastName,
                GuardianPhone = guardianPhoneResult.Phone,
            });

            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, StudentUpdateModel model)
    {
        await updateValidator.EnsureValidatedAsync(model);

        var studentPhoneResult = model.Phone.TrimPhoneNumber();
        if (!studentPhoneResult.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var fatherPhoneResult = model.StudentDetailUpdateModel.FatherPhone.TrimPhoneNumber();
        var motherPhoneResult = model.StudentDetailUpdateModel.MotherPhone.TrimPhoneNumber();
        var guardianPhoneResult = model.StudentDetailUpdateModel.GuardianPhone.TrimPhoneNumber();

        bool anyParentPhoneValid =
            fatherPhoneResult.IsSuccessful ||
            motherPhoneResult.IsSuccessful ||
            guardianPhoneResult.IsSuccessful;

        if (!anyParentPhoneValid)
            throw new ArgumentIsNotValidException("At least one parent phone number must be valid");

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var existStudent = await unitOfWork.Students
                .SelectAsync(
                    predicate: s => s.Id == id,
                    includes: [ "StudentDetail", "User" ])
                ?? throw new NotFoundException($"Student is not found");

            var phoneExists = await unitOfWork.Students
                .SelectAsync(e => e.User.Phone == model.Phone
                              && e.CompanyId == existStudent.CompanyId
                              && e.Id != id);

            if (phoneExists != null)
                throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");

            existStudent.User.FirstName = model.FirstName;
            existStudent.User.LastName = model.LastName;
            existStudent.Gender = model.Gender;
            existStudent.DateOfBirth = model.DateOfBirth;
            existStudent.User.Phone = studentPhoneResult.Phone;
            existStudent.User.Email = model.Email;

            if (existStudent.StudentDetail != null)
            {
                existStudent.StudentDetail.FatherFirstName = model.StudentDetailUpdateModel.FatherFirstName;
                existStudent.StudentDetail.FatherLastName = model.StudentDetailUpdateModel.FatherLastName;
                existStudent.StudentDetail.FatherPhone = fatherPhoneResult.Phone;
                existStudent.StudentDetail.MotherFirstName = model.StudentDetailUpdateModel.MotherFirstName;
                existStudent.StudentDetail.MotherLastName = model.StudentDetailUpdateModel.MotherLastName;
                existStudent.StudentDetail.MotherPhone = motherPhoneResult.Phone;
                existStudent.StudentDetail.GuardianFirstName = model.StudentDetailUpdateModel.GuardianFirstName;
                existStudent.StudentDetail.GuardianLastName = model.StudentDetailUpdateModel.GuardianLastName;
                existStudent.StudentDetail.GuardianPhone = guardianPhoneResult.Phone;
            }

            unitOfWork.Students.Update(existStudent);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: s => s.Id == id,
                includes: "StudentDetail")
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail != null)
        {
            unitOfWork.StudentDetails.MarkAsDeleted(existStudent.StudentDetail);
        }

        unitOfWork.Students.MarkAsDeleted(existStudent);
        await unitOfWork.SaveAsync();
    }

    public async Task<int> GetByPhoneAsync(string phone)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.User.Phone == phone)
            ?? throw new NotFoundException($"Student with phone {phone} not found");

        return student.Id;
    }

    public async Task<int> GetStudentParentByPhoneAsync(string phone)
    {
        var studentDetail = await unitOfWork.StudentDetails
            .SelectAsync(sd => sd.FatherPhone == phone ||
                        sd.MotherPhone == phone ||
                        sd.GuardianPhone == phone)
            ?? throw new NotFoundException($"Student parent with phone {phone} not found");

        return studentDetail.StudentId;
    }

    public async Task<StudentViewModel> GetAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: t => t.Id == id,
                includes: [ "StudentDetail", "User" ])
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail == null)
            throw new NotFoundException("Student details not found");

        return new StudentViewModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.User.FirstName,
            LastName = existStudent.User.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Phone = existStudent.User.Phone,
            Email = existStudent.User.Email,
            StudentDetailViewModel = new StudentDetailViewModel
            {
                Id = existStudent.StudentDetail.Id,
                StudentId = existStudent.Id,
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
            .SelectAllAsQueryable(c => c.Id == courseId && !c.IsDeleted)
            .SelectMany(c => c.StudentCourses)
            .Select(sc => new StudentViewModel
            {
                Id = sc.Student.Id,
                FirstName = sc.Student.User.FirstName,
                LastName = sc.Student.User.LastName,
                DateOfBirth = sc.Student.DateOfBirth,
                Gender = sc.Student.Gender,
                Phone = sc.Student.User.Phone,
                Email = sc.Student.User.Email,
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

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(picture.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentIsNotValidException("Only image files are allowed (jpg, jpeg, png, webp)");

        if (picture.Length > 5 * 1024 * 1024)
            throw new ArgumentIsNotValidException("File size must not exceed 5MB");

        // if (!string.IsNullOrEmpty(student.ProfilePicture))
        // {
        //     var oldFilePath = Path.Combine("wwwroot", student.ProfilePicture.TrimStart('/'));
        //     if (File.Exists(oldFilePath))
        //         File.Delete(oldFilePath);
        // }

        var fileName = $"{studentId}_{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine("wwwroot", "uploads", "students", "profiles");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await picture.CopyToAsync(stream);
        }
        var relativePath = $"/uploads/students/profiles/{fileName}";
        //student.ProfilePicture = relativePath;

        unitOfWork.Students.Update(student);
        await unitOfWork.SaveAsync();

        return relativePath;
    }

    public async Task UpdateStudentCourseStatusAsync(int studentId, int courseId, int statusId)
    {
        var studentCourse = unitOfWork.StudentCourses
            .SelectAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId)
            .GetAwaiter().GetResult()
            ?? throw new NotFoundException("StudentCourse not found");

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            studentCourse.Status = (Domain.Enums.StudentStatus)statusId;
            unitOfWork.StudentCourses.Update(studentCourse);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<StudentViewModel>> GetAll(StudentFilterModel filterModel)
    {
        var query = unitOfWork.Students
            .SelectAllAsQueryable(s => !s.IsDeleted);

        if (filterModel.CompanyId != null)
            query = query.Where(s => s.CompanyId == filterModel.CompanyId);

        if (!string.IsNullOrEmpty(filterModel.SearchText))
            query = query.Where(s => s.User.FirstName.Contains(filterModel.SearchText) ||
                                     s.User.LastName.Contains(filterModel.SearchText) ||
                                     s.User.Phone.Contains(filterModel.SearchText) ||
                                     s.User.Email.Contains(filterModel.SearchText));
        if (filterModel.CourseId != null)
            query = query.Where(s => s.Courses.Any(sc => sc.CourseId == filterModel.CourseId));

        return await query.Select(s => new StudentViewModel
        {
            Id = s.Id,
            FirstName = s.User.FirstName,
            LastName = s.User.LastName,
            DateOfBirth = s.DateOfBirth,
            Gender = s.Gender,
            Phone = s.User.Phone,
            Email = s.User.Email,
            StudentDetailViewModel = new StudentDetailViewModel
            {
                Id = s.StudentDetail.Id,
                StudentId = s.Id,
                FatherFirstName = s.StudentDetail.FatherFirstName,
                FatherLastName = s.StudentDetail.FatherLastName,
                FatherPhone = s.StudentDetail.FatherPhone,
                MotherFirstName = s.StudentDetail.MotherFirstName,
                MotherLastName = s.StudentDetail.MotherLastName,
                MotherPhone = s.StudentDetail.MotherPhone,
                RelativeFirstName = s.StudentDetail.GuardianFirstName,
                RelativeLastName = s.StudentDetail.GuardianLastName,
                RelativePhone = s.StudentDetail.GuardianPhone
            }
        }).ToListAsync();
    }
}
