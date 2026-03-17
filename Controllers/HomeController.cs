using System.Diagnostics;
using FrontendEXAM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontendEXAM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Admin");
            if (string.Equals(role, "Patient", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Patient");
            if (string.Equals(role, "Receptionist", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Receptionist");
            if (string.Equals(role, "Doctor", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("Dashboard", "Doctor");

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        //[AllowAnonymous]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
