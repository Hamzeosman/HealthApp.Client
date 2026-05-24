using Bunit;
using HealthApp.Client.Layout;
using HealthApp.Client.Services;
using HealthApp.Client.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HealthApp.Client.Tests.Layout
{
    public class NavMenuTests : BunitContext
    {
        private readonly Mock<AuthService> _authMock;

        public NavMenuTests()
        {
            _authMock = AuthServiceMock.Create();
            Services.AddSingleton(_authMock.Object);
        }

        private void GivenAuthenticated(bool authed) =>
            _authMock.Setup(a => a.IsAuthenticatedAsync()).ReturnsAsync(authed);

        [Fact]
        public void WhenNotAuthenticated_ShowsGuestLinks_HidesProtectedLinks()
        {
            GivenAuthenticated(false);

            var cut = Render<NavMenu>();

            cut.WaitForAssertion(() =>
            {
                Assert.Contains("Logga in", cut.Markup);
                Assert.Contains("Registrera", cut.Markup);
                Assert.DoesNotContain("Logga ut", cut.Markup);
                Assert.DoesNotContain("Min profil", cut.Markup);
                Assert.DoesNotContain("Övningar", cut.Markup);
            });
        }

        [Fact]
        public void WhenAuthenticated_ShowsProtectedLinks_HidesGuestLinks()
        {
            GivenAuthenticated(true);

            var cut = Render<NavMenu>();

            cut.WaitForAssertion(() =>
            {
                Assert.Contains("Logga ut", cut.Markup);
                Assert.Contains("Min profil", cut.Markup);
                Assert.Contains("Övningar", cut.Markup);
                Assert.DoesNotContain("Logga in", cut.Markup);
                Assert.DoesNotContain("Registrera", cut.Markup);
            });
        }

        [Fact]
        public void HomeLink_IsAlwaysVisible()
        {
            GivenAuthenticated(false);
            var cut = Render<NavMenu>();
            cut.WaitForAssertion(() => Assert.Contains("Hem", cut.Markup));

            GivenAuthenticated(true);
            var cut2 = Render<NavMenu>();
            cut2.WaitForAssertion(() => Assert.Contains("Hem", cut2.Markup));
        }

        [Fact]
        public void WhenLogoutClicked_CallsLogoutAndNavigatesToLogin()
        {
            GivenAuthenticated(true);
            var nav = Services.GetRequiredService<NavigationManager>();
            var cut = Render<NavMenu>();
            cut.WaitForAssertion(() => Assert.Contains("Logga ut", cut.Markup));

            cut.Find("button.nav-link").Click();

            _authMock.Verify(a => a.LogoutAsync(), Times.Once);
            Assert.EndsWith("/login", nav.Uri);
        }
    }
}
