using FrontendEXAM.Models;

namespace FrontendEXAM.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;

        public AuthService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            return _apiService.PostAsync<LoginRequest, LoginResponse>("/auth/login", request);
        }
    }
}
