using Bunit;
using HealthApp.Client.Layout;
using HealthApp.Client.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HealthApp.Client.Tests.Layout
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

            cut.WaitForAssertion(() => Assert.Contains("HEALTHAPP", cut.Markup));
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
