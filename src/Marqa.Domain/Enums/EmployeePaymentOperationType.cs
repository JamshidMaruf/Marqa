using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum EmployeePaymentOperationType
{
    [Description("Oylik")]
    Salary = 1,

    [Description("Avans")]
    Advance = 2,

    [Description("Bonus")]
    Bonus = 3,

    [Description("Kompensatsiya")]
    Compensation = 4
}
