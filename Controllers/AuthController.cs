using FrontendEXAM.Models;
using FrontendEXAM.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace FrontendEXAM.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        private static string NormalizeRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return role;
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(role.ToLowerInvariant());
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(roleClaim))
                {
                    return RedirectBasedOnRole(roleClaim);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                var response = await _authService.LoginAsync(model);
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    var normalizedRole = NormalizeRole(response.User.Role);

                    HttpContext.Session.SetString("Token", response.Token);
                    HttpContext.Session.SetString("UserRole", normalizedRole);
                    HttpContext.Session.SetString("UserName", response.User.Name);
                    HttpContext.Session.SetString("UserId", response.User.Id.ToString());

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, response.User.Name),
                        new Claim(ClaimTypes.Role, normalizedRole),
                        new Claim("Token", response.Token)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectBasedOnRole(normalizedRole);
                }
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
        

        private IActionResult RedirectBasedOnRole(string role)
        {
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Admin");

            if (string.Equals(role, "Doctor", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Doctor");

            if (string.Equals(role, "Receptionist", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Receptionist");

            if (string.Equals(role, "Patient", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Patient");

            return RedirectToAction("Index", "Home");
        }
    }
}
