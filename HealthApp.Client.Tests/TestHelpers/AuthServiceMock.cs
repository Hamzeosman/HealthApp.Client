using HealthApp.Client.Services;
using Microsoft.JSInterop;
using Moq;

namespace HealthApp.Client.Tests.TestHelpers
{
    /// <summary>Creates an AuthService Moq with the no-op constructor deps tests need.</summary>
    public static class AuthServiceMock
    {
        public static Mock<AuthService> Create()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost/") };
            var localStorage = new LocalStorageService(new Mock<IJSRuntime>().Object);
            return new Mock<AuthService>(httpClient, localStorage);
        }
    }
}
