namespace Marqa.Service.Services.Users.Models;

public class UserPasswordModel
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmationPassword { get; set; }
}