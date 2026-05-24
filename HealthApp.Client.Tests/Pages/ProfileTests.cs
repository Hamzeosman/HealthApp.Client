using Bunit;
using HealthApp.Client.Models;
using HealthApp.Client.Pages;
using HealthApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace HealthApp.Client.Tests.Pages
{
    public class ProfileTests : BunitContext
    {
        private readonly Mock<AuthService> _authMock;

        public ProfileTests()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost/") };
            var localStorage = new LocalStorageService(new Mock<IJSRuntime>().Object);
            _authMock = new Mock<AuthService>(httpClient, localStorage);
            Services.AddSingleton(_authMock.Object);
        }

        private void GivenAuthenticated(UserProfile? profile)
        {
            _authMock.Setup(a => a.IsAuthenticatedAsync()).ReturnsAsync(true);
            _authMock.Setup(a => a.GetProfileAsync()).ReturnsAsync(profile);
        }

        private void GivenSaveResult(UserProfile? result) =>
            _authMock.Setup(a => a.UpdateProfileAsync(It.IsAny<UpdateProfileRequest>()))
                .ReturnsAsync(result);

        private static UserProfile SampleProfile(string name = "test-user") => new()
        {
            Name = name,
            Email = $"{name}@example.com",
            CreatedAt = new DateTime(2025, 1, 1)
        };

        [Fact]
        public void WhenNotAuthenticated_RedirectsToLogin()
        {
            _authMock.Setup(a => a.IsAuthenticatedAsync()).ReturnsAsync(false);
            var nav = Services.GetRequiredService<NavigationManager>();

            Render<Profile>();

            Assert.EndsWith("/login", nav.Uri);
        }

        [Fact]
        public void WhenAuthenticated_RendersFormWithProfileData()
        {
            var profile = SampleProfile("alice");
            profile.Age = 30;
            profile.Goal = "Run a marathon";
            GivenAuthenticated(profile);

            var cut = Render<Profile>();

            cut.WaitForAssertion(() =>
            {
                Assert.Contains(profile.Name, cut.Markup);
                Assert.Contains(profile.Email, cut.Markup);
                Assert.Contains(profile.Goal, cut.Markup);
            });
        }

        [Fact]
        public void WhenProfileFetchFails_ShowsWarning()
        {
            GivenAuthenticated(profile: null);

            var cut = Render<Profile>();

            cut.WaitForAssertion(() => Assert.Contains("Kunde inte ladda profilen", cut.Markup));
        }

        [Fact]
        public void WhenSaveSucceeds_ShowsSuccessMessage()
        {
            GivenAuthenticated(SampleProfile("bob"));
            GivenSaveResult(SampleProfile("bob"));

            var cut = Render<Profile>();
            cut.WaitForAssertion(() => Assert.Contains("Spara ändringar", cut.Markup));

            cut.Find("button.btn-primary").Click();

            cut.WaitForAssertion(() => Assert.Contains("Profilen har sparats", cut.Markup));
            _authMock.Verify(a => a.UpdateProfileAsync(It.IsAny<UpdateProfileRequest>()), Times.Once);
        }

        [Fact]
        public void WhenSaveFails_ShowsErrorMessage()
        {
            GivenAuthenticated(SampleProfile("carol"));
            GivenSaveResult(result: null);

            var cut = Render<Profile>();
            cut.WaitForAssertion(() => Assert.Contains("Spara ändringar", cut.Markup));

            cut.Find("button.btn-primary").Click();

            cut.WaitForAssertion(() => Assert.Contains("Kunde inte spara profilen", cut.Markup));
        }
    }
}
