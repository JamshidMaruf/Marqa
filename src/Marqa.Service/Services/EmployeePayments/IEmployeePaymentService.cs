using Marqa.Service.Services.EmployeePayments.Models;

namespace Marqa.Service.Services.EmployeePayments;

public interface IEmployeePaymentService
{
    /// <summary>
    /// Creates a new employee payment record in the system.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CreateAsync(EmployeePaymentCreateModel model);

    /// <summary>
    /// Updates an existing employee payment record.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task UpdateAsync(EmployeePaymentUpdateModel model);

    /// <summary>
    /// Retrieves detailed information for a single employee payment by its ID.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    Task<EmployeePaymentViewModel> GetByPaymentIdAsync(int paymentId);

    /// <summary>
    ///  Retrieves a filtered, paginated list of all employee payments.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="employeeId"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<EmployeePaymentListViewModel>> GetAllAsync(
        string search = null,
        int? employeeId = null,
        int pageIndex = 1,
        int pageSize = 10);
}
