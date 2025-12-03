namespace Marqa.Service.Helpers;

public static class PasswordHelper
{
    /// <summary>
    /// Berilgan parolni hash qiladi.
    /// </summary>
    /// <param name="password">
    /// Hash qilinishi kerak bo‘lgan parol matni.
    /// </param>
    /// <returns>
    /// 12 darajali "salt" qollanilgan holda BCrypt algoritmi yordamida
    /// yaratilgan Hash qiymati
    /// </returns>
    public static string Hash(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    /// <summary>
    /// Hash qilingan parolni kiritilgan (Plain text) li
    /// parol bilan solishtirib tekshiradi.
    /// </summary>
    /// <param name="password">
    /// Foydalanuvchi tomonidan kiritilgan parol matni.
    /// </param>
    /// <param name="passwordHash">
    /// DB da saqlangan hash qilingan parol matni.
    /// </param>
    /// <returns>
    /// Agar parol va hash qilingan parol mos kelsa true,
    /// aks holda false qiymatini qaytaradi.
    /// </returns>
    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}