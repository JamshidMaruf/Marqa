using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum CoursePaymentType
{
    [Description("Qat'iy")] 
    Fixed = 1,

    [Description("Chegirmasiz")]
    DiscountFree = 2,

    [Description("Foiz")] 
    DiscountInPercentage = 3
}