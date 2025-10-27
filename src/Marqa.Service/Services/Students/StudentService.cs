// using Marqa.DataAccess.Repositories;
// using Marqa.DataAccess.UnitOfWork;
// using Marqa.Domain.Entities;
// using Marqa.Service.Exceptions;
// using Marqa.Service.Services.Students.Models;
// using Marqa.Service.Services.Students.Models.DetailModels;
// using Microsoft.EntityFrameworkCore;
//
// namespace Marqa.Service.Services.Students;
//
// public class StudentService(IUnitOfWork unitOfWork) : IStudentService
// {
//     public async Task CreateAsync(StudentCreateModel model)
//     {
//         _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
//            ?? throw new NotFoundException("Company not found");
//
//         await unitOfWork.BeginTransactionAsync();
//
//         try
//         {
//             var createdStudent = new Student()
//             {
//                 CompanyId = model.CompanyId,
//                 FirstName = model.FirstName,
//                 LastName = model.LastName,
//                 DateOfBirth = model.DateOfBirth,
//                 Gender = model.Gender,
//                 Phone = model.Phone,
//                 Email = model.Email,
//             };
//             
//             unitOfWork.Students.Insert(createdStudent);
//
//             await unitOfWork.SaveAsync();
//
//
//             unitOfWork.StudentDetails.Insert(new StudentDetail
//             {
//                 StudentId = createdStudent.Id,
//                 FatherFirstName = model.StudentDetailCreateModel.FatherFirstName,
//                 FatherLastName = model.StudentDetailCreateModel.FatherLastName,
//                 FatherPhone = model.StudentDetailCreateModel.FatherPhone,
//                 MotherFirstName = model.StudentDetailCreateModel.MotherFirstName,
//                 MotherLastName = model.StudentDetailCreateModel.MotherLastName,
//                 MotherPhone = model.StudentDetailCreateModel.MotherPhone,
//                 GuardianFirstName = model.StudentDetailCreateModel.GuardianFirstName,
//                 GuardianLastName = model.StudentDetailCreateModel.GuardianLastName,
//                 GuardianPhone = model.StudentDetailCreateModel.GuardianPhone
//             });
//
//             await unitOfWork.SaveAsync();
//
//             await unitOfWork.CommitAsync();
//         }
//         catch
//         {
//             await unitOfWork.RollbackTransactionAsync();
//             throw;
//         }
//     }
//
//     // StudentDetails include qilingan va transaction bilan update qilish
//     public async Task UpdateAsync(int id, StudentUpdateModel model)
//     {
//         await unitOfWork.BeginTransactionAsync();
//
//         try
//         {
//             var existStudent = await unitOfWork.Students
//                 .SelectAsync(predicate: s => s.Id == id, includes: new[] { "StudentDetail" })
//                 ?? throw new NotFoundException($"Student is not found");
//
//             existStudent.FirstName = model.FirstName;
//             existStudent.LastName = model.LastName;
//             existStudent.Gender = model.Gender;
//             existStudent.DateOfBirth = model.DateOfBirth;
//             existStudent.Phone = model.Phone;
//             existStudent.Email = model.Email;
//
//             existStudent.StudentDetail.FatherFirstName = model.StudentDetailUpdateModel.FatherFirstName;
//             existStudent.StudentDetail.FatherLastName = model.StudentDetailUpdateModel.FatherLastName;
//             existStudent.StudentDetail.FatherPhone = model.StudentDetailUpdateModel.FatherPhone;
//             existStudent.StudentDetail.MotherFirstName = model.StudentDetailUpdateModel.MotherFirstName;
//             existStudent.StudentDetail.MotherLastName = model.StudentDetailUpdateModel.MotherLastName;
//             existStudent.StudentDetail.MotherPhone = model.StudentDetailUpdateModel.MotherPhone;
//             existStudent.StudentDetail.GuardianFirstName = model.StudentDetailUpdateModel.GuardianFirstName;
//             existStudent.StudentDetail.GuardianLastName = model.StudentDetailUpdateModel.GuardianLastName;
//             existStudent.StudentDetail.GuardianPhone = model.StudentDetailUpdateModel.GuardianPhone;
//
//             await unitOfWork.Students.UpdateAsync(existStudent);
//             await unitOfWork.SaveAsync();
//
//             await unitOfWork.BeginCommitAsync();
//         }
//         catch
//         {
//             await unitOfWork.RollbackTransactionAsync();
//             throw;
//         }
//     }
//
//     // StudentDetail bilan birga o'chirish (include qilingan)
//     public async Task DeleteAsync(int id)
//     {
//         var existStudent = await unitOfWork.Students
//             .SelectAsync(predicate: s => s.Id == id, includes: new[] { "StudentDetail" })
//             ?? throw new NotFoundException($"Student is not found");
//
//         // Agar StudentDetail mavjud bo'lsa, uni ham o'chirish
//         if (existStudent.StudentDetail != null)
//         {
//             await unitOfWork.StudentDetails.DeleteAsync(existStudent.StudentDetail);
//         }
//
//         await unitOfWork.Students.DeleteAsync(existStudent);
//         await unitOfWork.SaveAsync();
//     }
//
//     public async Task<StudentViewModel> GetAsync(int id, string name)
//     {
//         var existStudent = await unitOfWork.Students
//             .SelectAsync(predicate: t => t.Id == id, includes: new[] { "StudentDetail" })
//             ?? throw new NotFoundException($"Student is not found");
//
//         return new StudentViewModel
//         {
//             Id = existStudent.Id,
//             FirstName = existStudent.FirstName,
//             LastName = existStudent.LastName,
//             DateOfBirth = existStudent.DateOfBirth,
//             Gender = existStudent.Gender,
//             Phone = existStudent.Phone,
//             Email = existStudent.Email,
//             StudentDetailViewModel = new StudentDetailViewModel
//             {
//                 FatherFirstName = existStudent.StudentDetail.FatherFirstName,
//                 FatherLastName = existStudent.StudentDetail.FatherLastName,
//                 FatherPhone = existStudent.StudentDetail.FatherPhone,
//                 MotherFirstName = existStudent.StudentDetail.MotherFirstName,
//                 MotherLastName = existStudent.StudentDetail.MotherLastName,
//                 MotherPhone = existStudent.StudentDetail.MotherPhone,
//                 RelativeFirstName = existStudent.StudentDetail.GuardianFirstName,
//                 RelativeLastName = existStudent.StudentDetail.GuardianLastName,
//                 RelativePhone = existStudent.StudentDetail.GuardianPhone
//             }
//         };
//     }
//
//     public async Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId)
//     {
//         var courseStudents = await unitOfWork.Courses
//                 .SelectAllAsQueryable()
//                 .Where(c => c.Id == courseId && !c.IsDeleted)
//                 .Include(c => c.Students)
//                 .ThenInclude(s => s.StudentDetail)
//                 .Select(c => c.Students.Select(s => new StudentViewModel
//                 {
//                     Id = s.Id,
//                     FirstName = s.FirstName,
//                     LastName = s.LastName,
//                     DateOfBirth = s.DateOfBirth,
//                     Gender = s.Gender,
//                     Phone = s.Phone,
//                     Email = s.Email,
//                     StudentDetailViewModel = new StudentDetailViewModel
//                     {
//                         Id = s.StudentDetail.Id,
//                         StudentId = s.Id,
//                         FatherFirstName = s.StudentDetail.FatherFirstName,
//                         FatherLastName = s.StudentDetail.FatherLastName,
//                         FatherPhone = s.StudentDetail.FatherPhone,
//                         MotherFirstName = s.StudentDetail.MotherFirstName,
//                         MotherLastName = s.StudentDetail.MotherLastName,
//                         MotherPhone = s.StudentDetail.MotherPhone,
//                         RelativeFirstName = s.StudentDetail.GuardianFirstName,
//                         RelativeLastName = s.StudentDetail.GuardianLastName,
//                         RelativePhone = s.StudentDetail.GuardianPhone
//                     }
//                 }))
//                 .FirstOrDefaultAsync()
//                 ?? throw new NotFoundException("Course is not found");
//
//         return courseStudents.ToList();
//     }
//
//     public Task SendOTPAsync(string phone)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<(int StudentId, bool IsVerified)> VerifyOTPAsync(string phone, int otpCode)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<List<Account>> GetAccountsAsync(int studentId)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<StudentViewModel> GetAsync(int id)
//     {
//         throw new NotImplementedException();
//     }
// }