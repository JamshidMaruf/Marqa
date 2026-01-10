using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Users.Models;

public class UserViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Status  { get; set; }
    public bool IsUseSystem { get; set; }
    public bool IsBlocked { get; set; }
    public string Role { get; set; }
}
