using Marqa.Service.Services.Auth.Models;

namespace Marqa.Service.Services.Auth;

public interface IAuthService
{
    ValueTask<LoginResponseModel> LoginAsync(LoginModel model, string ipAddress);
    ValueTask<LoginResponseModel> RefreshTokenAsync(RefreshTokenModel model);
    ValueTask<bool> LogoutAsync(LogoutModel model, string ipAddress);
    ValueTask<LoginResponseModel.UserData> GetCurrentUser(RefreshTokenModel model);
    SuperAdminResponseModel LoginAdmin(string phone, string password);
}