using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum StudentHomeTaskStatus
{
    [Description("Jarayonda")]
    Pending = 1,

    [Description("Baholangan")]
    Evaluated = 2,

    [Description("Rad etilgan")]
    Rejected = 3
}

