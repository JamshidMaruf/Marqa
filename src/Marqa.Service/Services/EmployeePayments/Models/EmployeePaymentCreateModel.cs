using System.ComponentModel.DataAnnotations;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.EmployeePayments.Models;

public class EmployeePaymentCreateModel
{
    public int EmployeeId { get; set; }

    public PaymentMethod PaymentMethod { get; set; } 

    public EmployeePaymentOperationType EmployeePaymentOperationType { get; set; } 

    public decimal Amount { get; set; }

    public DateTime DateTime { get; set; }= DateTime.Now;

    public string Description { get; set; }
}