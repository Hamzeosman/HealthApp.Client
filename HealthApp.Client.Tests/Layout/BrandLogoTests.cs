using Bunit;
using Mintakt.Client.Layout;
using Mintakt.Client.Tests.TestHelpers;

namespace Mintakt.Client.Tests.Layout
{
    /// <summary>
    /// Regression tests for Mintakt brand logo (Variant 3 — rounded Stroke-M with beat dot).
    /// Protects the brand identity — if someone accidentally changes name, tagline,
    /// or replaces the SVG path with a different letter, these tests catch it.
    /// </summary>
    public class BrandLogoTests : MudBunitContext
    {
        // Guards AppTokens.ProductName — brand name must stay "Mintakt" in wordmark.
        [Fact]
        public void RendersMintaktWordmark_ByDefault()
        {
            // Arrange + Act
            var cut = Render<BrandLogo>();

            // Assert
            Assert.Contains("brand-logo", cut.Markup);
            Assert.Contains("brand-name", cut.Markup);
            Assert.Contains("Mintakt", cut.Markup);
        }

        // Guards the SVG path — Variant 3 Stroke-M has a specific coordinate signature.
        // If this test fails, someone changed the logo (regression to T-monogram etc).
        // Requires Mark=true since SVG renders only when Mark=true OR Wordmark=false.
        [Fact]
        public void WhenMarkRendered_HasVariant3StrokeMSvgPath()
        {
            // Arrange + Act
            var cut = Render<BrandLogo>(parameters => parameters
                .Add(p => p.Mark, true));

            // Assert — Variant 3 path signature (M-shape: pillar-up, dip, pillar-up).
            Assert.Contains("M 60 178 L 60 62 L 120 158 L 180 62 L 180 178", cut.Markup);
        }

        // Guards the beat-dot signature (upper-right, matches wordmark's period).
        [Fact]
        public void WhenMarkRendered_HasBeatDotInUpperRightCorner()
        {
            // Arrange + Act
            var cut = Render<BrandLogo>(parameters => parameters
                .Add(p => p.Mark, true));

            // Assert
            Assert.Contains("cx=\"200\" cy=\"68\"", cut.Markup);
        }

        // Guards tagline text when explicitly requested via parameter.
        [Fact]
        public void WhenTaglineTrue_RendersHittaDinTakt()
        {
            // Arrange + Act
            var cut = Render<BrandLogo>(parameters => parameters
                .Add(p => p.Tagline, true));

            // Assert
            Assert.Contains("Hitta din takt", cut.Markup);
        }

        // Guards mark-only mode — no wordmark should render when Wordmark=false + Mark=true.
        [Fact]
        public void WhenMarkOnly_DoesNotRenderWordmark()
        {
            // Arrange + Act
            var cut = Render<BrandLogo>(parameters => parameters
                .Add(p => p.Mark, true)
                .Add(p => p.Wordmark, false));

            // Assert
            Assert.DoesNotContain("brand-name", cut.Markup);
            Assert.DoesNotContain("Mintakt", cut.Markup);
            // Mark SVG should still be present
            Assert.Contains("brand-mark", cut.Markup);
        }
    }
}