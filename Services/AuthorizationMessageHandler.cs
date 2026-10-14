using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;

namespace Mintakt.Client.Services
{
    /// <summary>
    /// DelegatingHandler som automatiskt lägger på JWT-token från localStorage
    /// på varje HTTP-request, samt redirectar till /login när backend returnerar 401.
    /// Fångar auth-utgång centralt så att inga sidor kraschar med Unhandled Exception.
    /// </summary>
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly LocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public AuthorizationMessageHandler(
            LocalStorageService localStorage,
            NavigationManager navigation)
        {
            _localStorage = localStorage;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Lägg på JWT-token om den finns i localStorage
            var token = await _localStorage.GetItemAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Skicka request och fånga responsen
            var response = await base.SendAsync(request, cancellationToken);

            // Hantera 401 centralt
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Rensa gammal token
                await _localStorage.RemoveItemAsync("authToken");

                // Spara nuvarande sida som returnUrl så användaren kan komma tillbaka efter re-login
                var currentUri = _navigation.ToBaseRelativePath(_navigation.Uri);
                var loginUrl = $"/login?returnUrl=/{Uri.EscapeDataString(currentUri)}";

                // Navigera till login
                _navigation.NavigateTo(loginUrl, forceLoad: false);
            }

            return response;
        }
    }
}