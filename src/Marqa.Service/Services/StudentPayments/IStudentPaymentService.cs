﻿using Marqa.Service.DTOs.StudentPaymentOperations;
using Marqa.Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marqa.Service.Interfaces;

public interface IStudentPaymentService : IScopedService
{
    /// <summary>
    /// Creates a new student payment record.
    /// Returns no value (Task).
    /// </summary>
    /// <param name="model">Payment data to be created (StudentId, Amount, CourseId, etc.).</param>
    Task CreateAsync(CreatePaymentModel model);

    /// <summary>
    /// Updates an existing student payment record.
    /// Returns no value (Task).
    /// </summary>
    /// <param name="model">Payment data for the update (PaymentId, StudentId, Amount, etc.).</param>
    Task UpdateAsync(UpdatePaymentModel model);

    /// <summary>
    /// Cancels an existing payment (e.g., due to an error).
    /// Returns no value (Task).
    /// </summary>
    /// <param name="model">The ID of the payment to cancel and the reason (Description).</param>
    Task CancelAsync(CancelPaymentModel model);

    /// <summary>
    /// Performs a refund operation by creating a new 'Expense' payment record 
    /// that mirrors the original 'Income' amount.
    /// </summary>
    /// <param name="model">The ID of the original payment and the reason for the refund.</param>
    Task RefundAsync(CancelPaymentModel model);

    /// <summary>
    /// Transfers the ownership of an existing payment record to a different student 
    /// by updating the StudentId field.
    /// </summary>
    /// <param name="paymentId">The ID of the payment to be transferred.</param>
    /// <param name="model">Contains the ID of the new student recipient.</param>
    Task TransferAsync(TransferPaymentModel model);

    /// <summary>
    /// Retrieves detailed information for a single payment by its ID.
    /// </summary>
    /// <param name="paymentId">The unique ID of the payment.</param>
    /// <returns>The complete payment details (ViewModel).</returns>
    Task<StudentPaymentViewModel> GetByIdAsync(int paymentId);

    /// <summary>
    /// Retrieves a paginated and searchable list of all student payments.
    /// </summary>
    /// <param name="search">Optional search term to filter results.</param>
    /// <param name="pageIndex">The current page number for pagination.</param>
    /// <param name="pageSize">The number of records per page.</param>
    /// <returns>A list of payments suitable for a general list view (ListViewModel).</returns>
    Task<List<StudentPaymentListViewModel>> GetAllAsync(
        string search = null,
        int pageIndex = 1,
        int pageSize = 10);
}