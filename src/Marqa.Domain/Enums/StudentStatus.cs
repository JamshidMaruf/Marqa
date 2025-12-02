using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum StudentStatus
{
    [Description("Faol")] Active = 1,
    [Description("Nofaol")] InActive = 2,
    [Description("Chiqib ketgan")] Dropped = 3,
    [Description("Tugatgan")] Completed = 4
}
