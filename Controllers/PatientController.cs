using FrontendEXAM.Models;
using FrontendEXAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontendEXAM.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var appointments = await _patientService.GetMyAppointmentsAsync();
                return View(appointments ?? new List<Appointment>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Appointment>());
            }
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookAppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _patientService.BookAppointmentAsync(request);
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Booking failed: {ex.Message}");
                return View(request);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var appointment = await _patientService.GetAppointmentDetailsAsync(id.ToString());
                if (appointment == null)
                {
                    return NotFound();
                }
                return View(appointment);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new Appointment());
            }
        }

        public async Task<IActionResult> MyPrescriptions()
        {
            try
            {
                var prescriptions = await _patientService.GetMyPrescriptionsAsync();
                return View(prescriptions ?? new List<Prescription>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Prescription>());
            }
        }

        public async Task<IActionResult> MyReports()
        {
            try
            {
                var reports = await _patientService.GetMyReportsAsync();
                return View(reports ?? new List<Report>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Report>());
            }
        }
    }
}
