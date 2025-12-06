using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.DTOs.StudentPaymentOperations;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Interfaces;
using Marqa.Service.Services.Exams.Models;
using Microsoft.EntityFrameworkCore;
namespace Marqa.Service.Services.StudentPayments;

public class StudentPaymentService(
    IUnitOfWork unitOfWork,
    IValidator<CreatePaymentModel> createValidator,
    IValidator<UpdatePaymentModel> updateValidator,
    IValidator<CancelPaymentModel> cancelValidator,
    IValidator<TransferPaymentModel> transferValidator) : IStudentPaymentService
{
    /// <summary>
    /// This method Generate PaymentNumber;
    /// </summary>
    /// <returns></returns>
    private static string GeneratePaymentNumber()
    {
        return $"PAY-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(1000, 9999)}";
    }
    
    /// <summary>
    /// This method make view model with PaymentNumber to show this model;
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    private StudentPaymentViewModel MapToViewModel(StudentPaymentOperation payment)
    {
        int paymentNumberInt = 0;
        var parts = payment.PaymentNumber.Split('-');
        if (parts.Length > 2 && int.TryParse(parts[2], out int result))
        {
            paymentNumberInt = result;
        }

        return new StudentPaymentViewModel
        {
            Id = payment.Id,
            PaymentNumber = paymentNumberInt,
            Amount = payment.Amount,
            DateTime = DateHelper.ToLocalTimeConverter(payment.GivenDate),
            Description = payment.Description,
            PaymentMethod = payment.PaymentMethod,
            PaymentOperationType = payment.PaymentOperationType,
            CourseId = payment.CourseId,
            CoursePrice = payment.CoursePrice,
            StudentId = payment.StudentId
        };
    }

    public async Task CreateAsync(CreatePaymentModel model)
    {
        await createValidator.EnsureValidatedAsync(model);



        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var createdPayment = new StudentPaymentOperation()
            {
                PaymentNumber = GeneratePaymentNumber(),
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                PaymentMethod = model.PaymentMethod,
                Amount = model.Amount,
                GivenDate = DateTime.UtcNow,
                Description = model.Description,
                PaymentOperationType = model.PaymentOperationType,
                CoursePrice = model.CoursePrice
            };

            unitOfWork.StudentPaymentOperations.Insert(createdPayment);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(UpdatePaymentModel model)
    {
        await updateValidator.EnsureValidatedAsync(model);

        var existPayment = await unitOfWork.StudentPaymentOperations
            .SelectAsync(p => p.Id == model.PaymentId)
            ?? throw new NotFoundException($"Payment not found with ID: {model.PaymentId}");


        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            existPayment.StudentId = model.StudentId;
            existPayment.CourseId = model.CourseId;
            existPayment.PaymentMethod = model.PaymentMethod;
            existPayment.Amount = model.Amount;
            existPayment.Description = model.Description;
            existPayment.PaymentOperationType = model.PaymentOperationType;
            existPayment.CoursePrice = model.CoursePrice;

            unitOfWork.StudentPaymentOperations.Update(existPayment);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            throw new Exception("The payment record has been updated by another user. Please refresh and try again.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task CancelAsync(CancelPaymentModel model)
    {
        await cancelValidator.EnsureValidatedAsync(model);

        var existPayment = await unitOfWork.StudentPaymentOperations
            .SelectAsync(p => p.Id == model.PaymentId && !p.IsDeleted)
            ?? throw new NotFoundException($"Active payment not found with ID: {model.PaymentId}");

        if (existPayment.PaymentOperationType != PaymentOperationType.Income)
        {
            throw new ArgumentIsNotValidException("Only Income payments can be cancelled.");
        }

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            unitOfWork.StudentPaymentOperations.MarkAsDeleted(existPayment);
            existPayment.Description = $"CANCELLED: {model.Description} | Original Description: {existPayment.Description}";

            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RefundAsync(CancelPaymentModel model)
    {
        await cancelValidator.EnsureValidatedAsync(model);

        var originalPayment = await unitOfWork.StudentPaymentOperations
            .SelectAsync(p => p.Id == model.PaymentId && !p.IsDeleted)
            ?? throw new NotFoundException($"Original payment not found with ID: {model.PaymentId}");

        if (originalPayment.PaymentOperationType != PaymentOperationType.Income)
        {
            throw new ArgumentIsNotValidException("Payment is not eligible for refund (must be Income).");
        }

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var refundPayment = new StudentPaymentOperation()
            {
                PaymentNumber = GeneratePaymentNumber(),
                StudentId = originalPayment.StudentId,
                CourseId = originalPayment.CourseId,
                PaymentMethod = originalPayment.PaymentMethod,
                Amount = originalPayment.Amount,
                GivenDate = DateTime.UtcNow,
                Description = $"REFUND for {originalPayment.PaymentNumber}: {model.Description}",
                PaymentOperationType = PaymentOperationType.Expense,
                CoursePrice = originalPayment.CoursePrice
            };

            unitOfWork.StudentPaymentOperations.Insert(refundPayment);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task TransferAsync(TransferPaymentModel model)
    {
        await transferValidator.EnsureValidatedAsync(model);

        var existPayment = await unitOfWork.StudentPaymentOperations
            .SelectAsync(p => p.Id == model.PaymentId && !p.IsDeleted)
            ?? throw new NotFoundException($"Payment not found with ID: {model.PaymentId}");

        var existStudent = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);

        if (!existStudent)
            throw new NotFoundException($"Student not found with ID: {model.StudentId}");

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var oldStudentId = existPayment.StudentId;

            existPayment.StudentId = model.StudentId;
            existPayment.Description += $" | Transferred from Student ID: {oldStudentId} to Student ID: {model.StudentId} on {DateTime.UtcNow}.";

            unitOfWork.StudentPaymentOperations.Update(existPayment);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<StudentPaymentViewModel> GetByIdAsync(int paymentId)
    {
        var existPayment = await unitOfWork.StudentPaymentOperations
            .SelectAsync(
                predicate: p => p.Id == paymentId && !p.IsDeleted)
            ?? throw new NotFoundException($"Payment not found with ID: {paymentId}");

        return MapToViewModel(existPayment);
    }

    /// <summary>
    /// Thos method makes model for GetAll Method
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    private StudentPaymentListViewModel MapToListViewModel(StudentPaymentOperation payment)
    {
        return new StudentPaymentListViewModel
        {
            StudentId = payment.StudentId,
            PaymentMethod = new EnumViewModel { Id = (int)payment.PaymentMethod, Name = payment.PaymentMethod.ToString() },
            Amount = payment.Amount,
            DateTime = DateHelper.ToLocalTimeConverter(payment.GivenDate),
            PaymentOperationType = new EnumViewModel { Id = (int)payment.PaymentOperationType, Name = payment.PaymentOperationType.ToString() },
            CourseName = payment.Course?.Name
        };
    }

    public async Task<List<StudentPaymentListViewModel>> GetAllAsync(
        string search = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var query = unitOfWork.StudentPaymentOperations
            .SelectAllAsQueryable(p => !p.IsDeleted)
            .Include(p => p.Course)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            string normalizedSearch = search.Trim().ToLower();
            query = query.Where(p =>
                p.PaymentNumber.ToLower().Contains(normalizedSearch) ||
                p.Course.Name.ToLower().Contains(normalizedSearch)


            );
        }

        query = query.OrderByDescending(p => p.GivenDate);

        if (pageIndex > 0 && pageSize > 0)
        {
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        var payments = await query.ToListAsync();

        if (!payments.Any())
        {
            return new List<StudentPaymentListViewModel>();
        }

        return payments.Select(MapToListViewModel).ToList();
    }
}