using Bunit;
using Mintakt.Client.Layout;
using Mintakt.Client.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Mintakt.Client.Tests.Layout
{
    public class MainLayoutTests : MudBunitContext
    {
        private static RenderFragment LayoutFragment() => builder =>
        {
            builder.OpenComponent<MudPopoverProvider>(0);
            builder.CloseComponent();
            builder.OpenComponent<MainLayout>(1);
            builder.CloseComponent();
        };

        [Fact]
        public void WhenAuthenticated_HidesGuestActionsInAppBar()
        {
            GivenAuthenticated(true);

            var cut = Render(LayoutFragment());

            // Brand replaced "HEALTHAPP"-text with <BrandLogo /> in Mintakt rebrand (phase 1).
            cut.WaitForAssertion(() => Assert.Contains("Mintakt", cut.Markup));
            Assert.DoesNotContain("Logga in", cut.Markup);
            Assert.DoesNotContain("Registrera", cut.Markup);
        }

        [Fact]
        public void WhenNotAuthenticated_ShowsLoginAndRegisterButtons()
        {
            GivenAuthenticated(false);

            var cut = Render(LayoutFragment());

            cut.WaitForAssertion(() =>
            {
                Assert.Contains("Logga in", cut.Markup);
                Assert.Contains("Registrera", cut.Markup);
            });
        }
    }
}