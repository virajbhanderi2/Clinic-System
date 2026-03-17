using FrontendEXAM.Models;

namespace FrontendEXAM.Services
{
    public class PatientService
    {
        private readonly ApiService _apiService;

        public PatientService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<Appointment?> BookAppointmentAsync(BookAppointmentRequest request)
        {
            return _apiService.PostAsync<BookAppointmentRequest, Appointment>("/appointments", request);
        }

        public Task<List<Appointment>?> GetMyAppointmentsAsync()
        {
            return _apiService.GetAsync<List<Appointment>>("/appointments/my");
        }

        public Task<Appointment?> GetAppointmentDetailsAsync(string id)
        {
            return _apiService.GetAsync<Appointment>($"/appointments/{id}");
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
