using FrontendEXAM.Models;
using FrontendEXAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontendEXAM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var clinic = await _adminService.GetClinicInfoAsync();
                var users = await _adminService.GetUsersAsync();
                
                ViewBag.Clinic = clinic;
                
                return View(users ?? new List<User>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<User>());
            }
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminCreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _adminService.CreateUserAsync(request);
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to create user: {ex.Message}");
                return View(request);
            }
        }
    }
}
