using System.ComponentModel.DataAnnotations;

namespace Marqa.Domain.Enums;

public enum TeacherPaymentType
{
    [Display(Name = "Fixed", Description = "Qat'iy oylik har oyda o'zgarmaydigan belgilangan summa")]
    Fixed = 1,

    [Display(Name = "Percentage", Description = "Foiz yutuq miqdoriga asosan to'langan foiz")]
    Percentage = 2,

    [Display(Name = "Hourly", Description = "Soatlik har bir soat uchun to'langan oylik")]
    Hourly = 3,

    [Display(Name = "Mixed", Description = "Aralash qat'iy oylik + foiz kombinatsiyasi")]
    Mixed = 4
}
