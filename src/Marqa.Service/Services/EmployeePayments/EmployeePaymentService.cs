using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.EmployeePayments.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.EmployeePayments;

public class EmployeePaymentService(IUnitOfWork unitOfWork,
        IValidator<EmployeePaymentCreateModel> createValidator,
        IValidator<EmployeePaymentUpdateModel> updateValidator) : IEmployeePaymentService
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
    /// This method maps EmployeePayment Entity to EmployeePaymentViewModel for detail view.
    /// </summary>
    private static EmployeePaymentViewModel MapToViewModel(EmployeePaymentOperation paymentOperation)
    {
        return new EmployeePaymentViewModel
        {
            Id = paymentOperation.Id,
            EmployeeId = paymentOperation.EmployeeId,
            PaymentNumber = paymentOperation.PaymentNumber,
            PaymentMethod = paymentOperation.PaymentMethod,
            EmployeePaymentOperationType = paymentOperation.OperationType,
            Amount = paymentOperation.Amount,
            DateTime = DateHelper.ToLocalTimeConverter(paymentOperation.DateTime),
            Description = paymentOperation.Description
        };
    }

    /// <summary>
    /// This method maps EmployeePayment Entity to EmployeePaymentListViewModel for list view.
    /// </summary>
    private static EmployeePaymentListViewModel MapToListViewModel(EmployeePaymentOperation paymentOperation)
    {
        return new EmployeePaymentListViewModel
        {
            Id = paymentOperation.Id,
            EmployeeId = paymentOperation.EmployeeId,
            PaymentNumber = paymentOperation.PaymentNumber,
            PaymentMethod = new EnumViewModel
            {
                Id = (int)paymentOperation.PaymentMethod,
                Name = paymentOperation.PaymentMethod.ToString()
            },
            EmployeePaymentOperationType = new EnumViewModel
            {
                Id = (int)paymentOperation.OperationType,
                Name = paymentOperation.OperationType.ToString()
            },
            Amount = paymentOperation.Amount,
            DateTime = DateHelper.ToLocalTimeConverter(paymentOperation.DateTime),
            Description = paymentOperation.Description
        };
    }

    public async Task CreateAsync(EmployeePaymentCreateModel model)
    {
        await createValidator.EnsureValidatedAsync(model);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var createdPayment = new EmployeePaymentOperation
            {
                PaymentNumber = GeneratePaymentNumber(),
                EmployeeId = model.EmployeeId,
                PaymentMethod = model.PaymentMethod,
                Amount = model.Amount,
                Description = model.Description,
                OperationType = model.EmployeePaymentOperationType,
            };

            unitOfWork.EmployeePaymentOperations.Insert(createdPayment);
            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(EmployeePaymentUpdateModel model)
    {
        await updateValidator.EnsureValidatedAsync(model);

        var existPayment = await unitOfWork.EmployeePaymentOperations
            .SelectAsync(p => p.Id == model.Id && !p.IsDeleted)
            ??throw new NotFoundException($"Payment not found with ID: {model.Id}");


        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            existPayment.EmployeeId = model.EmployeeId;
            existPayment.PaymentMethod = model.PaymentMethod;
            existPayment.Amount = model.Amount;
            existPayment.Description = model.Description;
            existPayment.OperationType = model.EmployeePaymentOperationType;

            unitOfWork.EmployeePaymentOperations.Update(existPayment);
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



    public async Task<EmployeePaymentViewModel> GetByPaymentIdAsync(int paymentId)
    {
        var existPayment = await unitOfWork.EmployeePaymentOperations
            .SelectAsync(
                predicate: p => p.Id == paymentId && !p.IsDeleted,
                includes: new[] { "Employee" })
            ?? throw new NotFoundException($"Payment not found with ID: {paymentId}");

        return MapToViewModel(existPayment);
    }

    public async Task<List<EmployeePaymentListViewModel>> GetAllAsync(
        string search = null,
        int? employeeId = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var query = unitOfWork.EmployeePaymentOperations
            .SelectAllAsQueryable(p => !p.IsDeleted)
            .Include(p => p.Employee)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            string normalizedSearch = search.Trim().ToLower();
            query = query.Where(p =>
                p.Description.ToLower().Contains(normalizedSearch) ||
                p.PaymentNumber.ToLower().Contains(normalizedSearch)
            );
        }

        if (employeeId.HasValue && employeeId.Value > 0)
        {
            query = query.Where(p => p.EmployeeId == employeeId.Value);
        }

        query = query.OrderByDescending(p => p.DateTime);

        if (pageIndex > 0 && pageSize > 0)
        {
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        var payments = await query.ToListAsync();

        return payments.Select(MapToListViewModel).ToList();
    }
}