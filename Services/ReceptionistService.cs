using FrontendEXAM.Models;

namespace FrontendEXAM.Services
{
    public class ReceptionistService
    {
        private readonly ApiService _apiService;

        public ReceptionistService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<List<QueueEntry>?> GetDailyQueueAsync(string date)
        {
            return _apiService.GetAsync<List<QueueEntry>>($"/queue?date={date}");
        }

        public Task<QueueEntry?> UpdateQueueStatusAsync(string id, QueueUpdateRequest request)
        {
            return _apiService.PatchAsync<QueueUpdateRequest, QueueEntry>($"/queue/{id}", request);
        }
    }
}
