using FrontendEXAM.Models;

namespace FrontendEXAM.Services
{
    public class DoctorService
    {
        private readonly ApiService _apiService;

        public DoctorService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<List<DoctorQueueItem>?> GetTodayQueueAsync()
        {
            return _apiService.GetAsync<List<DoctorQueueItem>>("/doctor/queue");
        }

        public Task<Prescription?> AddPrescriptionAsync(string appointmentId, AddPrescriptionRequest request)
        {
            return _apiService.PostAsync<AddPrescriptionRequest, Prescription>($"/prescriptions/{appointmentId}", request);
        }

        public Task<Report?> AddReportAsync(string appointmentId, AddReportRequest request)
        {
            return _apiService.PostAsync<AddReportRequest, Report>($"/reports/{appointmentId}", request);
        }

        public Task<List<Prescription>?> GetMyPrescriptionsAsync()
        {
            return _apiService.GetAsync<List<Prescription>>("/prescriptions/my");
        }

        public Task<List<Report>?> GetMyReportsAsync()
        {
            return _apiService.GetAsync<List<Report>>("/reports/my");
        }
    }
}
