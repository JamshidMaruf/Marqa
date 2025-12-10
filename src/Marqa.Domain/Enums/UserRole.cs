using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum UserRole
{
    [Description("Ishchi")]
    Employee = 1,

    [Description("O'quvchi")]
    Student = 2
}