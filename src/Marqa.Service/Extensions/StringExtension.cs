namespace Marqa.Service.Extensions;

public static class StringExtension
{
    public static string Hash(this string password)
    {
       // var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verify(this string sourcePassword, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(sourcePassword, passwordHash);
    }
}
