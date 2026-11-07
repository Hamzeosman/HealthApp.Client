using Mintakt.Client.Services;

namespace Mintakt.Client.Tests.Services
{
    public class ApiBaseAddressTests
    {
        // Utan inställning används den vanliga API-adressen
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Resolve_WithoutValue_ReturnsDefault(string? configured)
        {
            Assert.Equal(new Uri("http://localhost:5137/"), ApiBaseAddress.Resolve(configured));
        }

        // En worktree kan peka klienten mot ett API på en annan port
        [Fact]
        public void Resolve_WithConfiguredUrl_ReturnsIt()
        {
            Assert.Equal(new Uri("http://localhost:5280/"), ApiBaseAddress.Resolve("http://localhost:5280/"));
        }

        // Utan avslutande snedstreck skulle relativa anrop som "api/workoutsession" hamna fel
        [Fact]
        public void Resolve_WithoutTrailingSlash_AddsIt()
        {
            Assert.Equal("http://localhost:5280/", ApiBaseAddress.Resolve("http://localhost:5280").ToString());
        }

        // Felaktiga värden ska ge ett tydligt fel direkt, inte ett tyst byte till standardadressen
        [Theory]
        [InlineData("localhost:5280")]
        [InlineData("not a url")]
        [InlineData("ftp://localhost:5280")]
        [InlineData("/api")]
        public void Resolve_WithInvalidUrl_Throws(string configured)
        {
            Assert.Throws<InvalidOperationException>(() => ApiBaseAddress.Resolve(configured));
        }
    }
}
