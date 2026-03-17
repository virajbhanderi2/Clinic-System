using FrontendEXAM.Models;

namespace FrontendEXAM.Services
{
    public class AdminService
    {
        private readonly ApiService _apiService;

        public AdminService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<Clinic?> GetClinicInfoAsync()
        {
            return _apiService.GetAsync<Clinic>("/admin/clinic");
        }

        public Task<List<User>?> GetUsersAsync()
        {
            return _apiService.GetAsync<List<User>>("/admin/users");
        }

        public Task<User?> CreateUserAsync(AdminCreateUserRequest request)
        {
            return _apiService.PostAsync<AdminCreateUserRequest, User>("/admin/users", request);
        }
    }
}
