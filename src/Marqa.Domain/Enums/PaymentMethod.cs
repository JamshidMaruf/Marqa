using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum PaymentMethod
{
    [Description("Karta orqali")]
    Card = 1,

    [Description("Naqd pul")]
    Cash = 2,

    [Description("Bank o'tkazmasi")]
    Transfer = 3
}