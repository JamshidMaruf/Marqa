namespace Marqa.Service.Services.Auth.Models;

public class LoginResponseModel
{
    public UserData User { get; set; }
    public TokenData Token { get; set; }
    
    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; }
    }
    
    public class TokenData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}