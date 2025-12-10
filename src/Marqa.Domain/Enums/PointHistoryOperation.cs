using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum PointHistoryOperation
{
    [Description("Ayirish")]
    Minus = -1,

    [Description("Qo'shish")]
    Plus = 1
}
