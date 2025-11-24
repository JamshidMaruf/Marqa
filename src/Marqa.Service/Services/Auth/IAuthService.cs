using Marqa.Service.Services.Auth.Models;

namespace Marqa.Service.Services.Auth;

public interface IAuthService
{
    ValueTask<LoginResponseModel> LoginAsync(LoginModel model, string ipAddress);
    ValueTask<LoginResponseModel> RefreshTokenAsync(RefreshTokenModel model);
}