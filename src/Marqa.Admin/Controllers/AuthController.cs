using System.Security.Claims;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        //if already logged in, redirect to dashboard
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "DashBoard");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string phone, string password, bool rememberMe)
    {
        try
        {
            // Check if the inputs are valid
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                TempData["ErrorMessage"] = "Both phone and password are required.";
                return View();
            }

            // Authenticate admin using Login method
            var loginResponse = _authService.LoginAdmin(phone, password);

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResponse.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{loginResponse.FirstName} {loginResponse.LastName}"),
                new Claim(ClaimTypes.MobilePhone, loginResponse.Phone),
                new Claim(ClaimTypes.Role, loginResponse.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Store tokens in session
            HttpContext.Session.SetString("AccessToken", loginResponse.AccessToken);
            HttpContext.Session.SetString("RefreshToken", loginResponse.RefreshToken);

            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        try
        {
            // Get refresh token from session
            var refreshToken = HttpContext.Session.GetString("RefreshToken");

            if (!string.IsNullOrEmpty(refreshToken))
            {
                // Logout from backend
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                
                await _authService.LogoutAsync(new LogoutModel{Token = refreshToken}, ipAddress);
            }

            // Clear session
            HttpContext.Session.Clear();

            // Sign out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["InfoMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Login");
        }
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}