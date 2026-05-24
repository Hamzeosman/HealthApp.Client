using System.Net.Http.Headers;
using System.Net.Http.Json;
using HealthApp.Client.Models;

namespace HealthApp.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly LocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, LocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public virtual async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            if (!response.IsSuccessStatusCode) return null;

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (authResponse != null)
            {
                await _localStorage.SetItemAsync("authToken", authResponse.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
            }
            return authResponse;
        }

        public virtual async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            if (!response.IsSuccessStatusCode) return null;

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (authResponse != null)
            {
                await _localStorage.SetItemAsync("authToken", authResponse.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
            }
            return authResponse;
        }

        public virtual async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public virtual async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _localStorage.GetItemAsync("authToken");
            if (string.IsNullOrEmpty(token)) return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        public virtual async Task<UserProfile?> GetProfileAsync()
        {
            var response = await _httpClient.GetAsync("api/user/me");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserProfile>();
        }

        public virtual async Task<UserProfile?> UpdateProfileAsync(UpdateProfileRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/user/me", request);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserProfile>();
        }
    }
}