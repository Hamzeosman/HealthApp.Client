using System.Net.Http.Json;
using Mintakt.Client.Models;

namespace Mintakt.Client.Services
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
                // Token läses nu från localStorage av AuthorizationMessageHandler
                // på varje request — ingen manuell DefaultRequestHeaders behövs.
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
            }
            return authResponse;
        }

        public virtual async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            // Ingen DefaultRequestHeaders att rensa — handler slutar hitta
            // token i localStorage och skickar inget Authorization-header.
        }

        public virtual async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _localStorage.GetItemAsync("authToken");
            return !string.IsNullOrEmpty(token);
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