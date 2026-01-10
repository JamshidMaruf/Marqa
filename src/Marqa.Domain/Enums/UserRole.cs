using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum UserRole
{
    [Description("Admin")]
    Admin = 0,
    
    [Description("Ishchi")]
    Employee = 1,

    [Description("O'quvchi")]
    Student = 2,
    
    [Description("O'qituvchi")]
    Teacher = 3,

    [Description("Ota-ona")]
    Parent = 4
}