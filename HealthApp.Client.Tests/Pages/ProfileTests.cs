using Bunit;
using HealthApp.Client.Models;
using HealthApp.Client.Pages;
using HealthApp.Client.Services;
using HealthApp.Client.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HealthApp.Client.Tests.Pages
{
    public class ProfileTests : BunitContext
    {
        private readonly Mock<AuthService> _authMock;

        public ProfileTests()
        {
            _authMock = AuthServiceMock.Create();
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

        // ─── Clear field coverage (regression tests for PR #30) ──────────────

        [Fact]
        public void WhenUserClearsGoal_SendsEmptyStringInRequest()
        {
            // Arrange: profil med Goal satt
            var profile = SampleProfile("dave");
            profile.Goal = "Build Muscle";
            GivenAuthenticated(profile);
            GivenSaveResult(profile);

            UpdateProfileRequest? capturedRequest = null;
            _authMock.Setup(a => a.UpdateProfileAsync(It.IsAny<UpdateProfileRequest>()))
                .Callback<UpdateProfileRequest>(req => capturedRequest = req)
                .ReturnsAsync(profile);

            var cut = Render<Profile>();
            cut.WaitForAssertion(() => Assert.Contains("Build Muscle", cut.Markup));

            // Act: töm Goal-textarea och spara
            var goalTextarea = cut.Find("textarea");
            goalTextarea.Change(string.Empty);
            cut.Find("button.btn-primary").Click();

            // Assert: backend anropades med tom Goal (inte null)
            cut.WaitForAssertion(() =>
            {
                Assert.NotNull(capturedRequest);
                Assert.Equal(string.Empty, capturedRequest!.Goal);
            });
        }

        [Fact]
        public void WhenUserClearsFitnessLevel_SendsEmptyStringInRequest()
        {
            // Arrange: profil med FitnessLevel satt
            var profile = SampleProfile("erin");
            profile.FitnessLevel = "Intermediate";
            GivenAuthenticated(profile);
            GivenSaveResult(profile);

            UpdateProfileRequest? capturedRequest = null;
            _authMock.Setup(a => a.UpdateProfileAsync(It.IsAny<UpdateProfileRequest>()))
                .Callback<UpdateProfileRequest>(req => capturedRequest = req)
                .ReturnsAsync(profile);

            var cut = Render<Profile>();
            cut.WaitForAssertion(() => Assert.Contains("Spara ändringar", cut.Markup));

            // Act: välj tom option ("Välj nivå...") och spara
            var fitnessSelect = cut.Find("select");
            fitnessSelect.Change(string.Empty);
            cut.Find("button.btn-primary").Click();

            // Assert: backend anropades med tom FitnessLevel
            cut.WaitForAssertion(() =>
            {
                Assert.NotNull(capturedRequest);
                Assert.Equal(string.Empty, capturedRequest!.FitnessLevel);
            });
        }
    }
}