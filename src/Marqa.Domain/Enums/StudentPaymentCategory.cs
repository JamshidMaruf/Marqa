using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum StudentPaymentCategory
{
    [Description("Kurs uchun to'lov")]
    CoursePayment = 1,

    [Description("Imtihon uchun to'lov")]
    ExamPayment = 2,

    [Description("Darslik uchun to'lov")]
    TextBookPayment = 3,

    [Description("Boshqa to'lovlar")]
    OtherPayment = 4
}