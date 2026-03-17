using FrontendEXAM.Models;
using FrontendEXAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontendEXAM.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var queue = await _doctorService.GetTodayQueueAsync();
                return View(queue ?? new List<DoctorQueueItem>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<DoctorQueueItem>());
            }
        }

        [HttpGet]
        public IActionResult AddPrescription(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Dashboard");
            }
            
            ViewBag.AppointmentId = id;
            var model = new AddPrescriptionRequest
            {
                Medicines = new List<PrescriptionMedicine>
                {
                    new PrescriptionMedicine() 
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(string id, AddPrescriptionRequest request)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Dashboard");
            }

            if (request.Medicines != null)
            {
                request.Medicines = request.Medicines
                    .Where(m => !string.IsNullOrWhiteSpace(m.Name))
                    .ToList();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AppointmentId = id;
                return View(request);
            }

            try
            {
                await _doctorService.AddPrescriptionAsync(id, request);
                TempData["SuccessMessage"] = "Prescription added successfully!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to add prescription: {ex.Message}");
                ViewBag.AppointmentId = id;
                return View(request);
            }
        }

        [HttpGet]
        public IActionResult AddReport(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Dashboard");
            }
            
            ViewBag.AppointmentId = id;
            return View(new AddReportRequest());
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(string id, AddReportRequest request)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Dashboard");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AppointmentId = id;
                return View(request);
            }

            try
            {
                await _doctorService.AddReportAsync(id, request);
                TempData["SuccessMessage"] = "Report added successfully!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to add report: {ex.Message}");
                ViewBag.AppointmentId = id;
                return View(request);
            }
        }
    }
}
