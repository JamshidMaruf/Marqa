using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum PaymentOperationType
{
    [Description("Kirim")]
    Income = 1,

    [Description("Chiqim")]
    Expense = 2,
}