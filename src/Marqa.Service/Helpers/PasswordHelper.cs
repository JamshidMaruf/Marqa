namespace Marqa.Service.Helpers;

public static class PasswordHelper
{
    /// <summary>
    /// The method hashes the provided plain text password using the BCrypt algorithm.
    /// </summary>
    /// <param name="password">
    /// The plain text password to be hashed.
    /// </param>
    /// <returns>
    /// The method returns the hashed version of the provided password.
    /// </returns>
    public static string Hash(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    /// <summary>
    /// The method verifies whether the plain text password entered by the user
    /// matches the hashed password stored in the database.
    /// </summary>
    /// <param name="password">
    /// The plain text password entered by the user.
    /// </param>
    /// <param name="passwordHash">
    /// The hashed password retrieved from the database.
    /// </param>
    /// <returns>
    /// If the password the user entered matches the hashed password from the database,
    /// the method returns true; otherwise, it returns false.
    /// </returns>
    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}