using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum TeacherType
{
    [Description("Asosiy o'qituvchi")]
    Lead = 1, 

    [Description("Yordamchi o'qituvchi")]
    Assistant = 2
}