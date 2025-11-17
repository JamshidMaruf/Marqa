namespace Marqa.Service.Helpers;

public static class SettingConstant
{
    public const string EncryptionKey = "c776cd64-62cb-4d17-acfa-f22ef44a3b83";
}

public static class PasswordHelper
{
    public static string Hash(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}