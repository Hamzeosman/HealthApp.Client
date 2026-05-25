using Bunit;
using HealthApp.Client.Layout;
using HealthApp.Client.Tests.TestHelpers;

namespace HealthApp.Client.Tests.Layout
{
    public class NavMenuTests : MudBunitContext
    {
        [Fact]
        public void WhenNotAuthenticated_ShowsGuestLinks_HidesProtectedLinks()
        {
            GivenAuthenticated(false);

            var cut = Render<NavMenu>();

            cut.WaitForAssertion(() =>
            {
                Assert.Contains("Logga in", cut.Markup);
                Assert.Contains("Registrera", cut.Markup);
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
                Assert.Contains("Min profil", cut.Markup);
                Assert.Contains("Övningar", cut.Markup);
                Assert.Contains("Träningsprogram", cut.Markup);
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
    }
}
