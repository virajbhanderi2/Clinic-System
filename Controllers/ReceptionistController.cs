using FrontendEXAM.Models;
using FrontendEXAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontendEXAM.Controllers
{
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistController : Controller
    {
        private readonly ReceptionistService _receptionistService;

        public ReceptionistController(ReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        public async Task<IActionResult> Dashboard(string? date)
        {
            try
            {
                var queryDate = string.IsNullOrEmpty(date) ? DateTime.Today.ToString("yyyy-MM-dd") : date;
                ViewBag.CurrentDate = queryDate;

                var queue = await _receptionistService.GetDailyQueueAsync(queryDate);
                return View(queue ?? new List<QueueEntry>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<QueueEntry>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string id, string status, string? date)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(status))
            {
                return RedirectToAction("Dashboard", new { date });
            }

            try
            {
                var request = new QueueUpdateRequest { Status = status };
                await _receptionistService.UpdateQueueStatusAsync(id, request);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to update status: {ex.Message}";
            }

            return RedirectToAction("Dashboard", new { date });
        }
    }
}
