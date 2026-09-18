using Bunit;
using Mintakt.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor.Services;

namespace Mintakt.Client.Tests.TestHelpers
{
    /// <summary>
    /// bUnit context wired for MudBlazor: registers Mud services, sets loose JS interop,
    /// and disposes async-disposable Mud services correctly via IAsyncLifetime.
    /// Use as base class for any test rendering MudBlazor components.
    /// </summary>
    public abstract class MudBunitContext : BunitContext, IAsyncLifetime
    {
        protected Mock<AuthService> AuthMock { get; }

        protected MudBunitContext()
        {
            AuthMock = AuthServiceMock.Create();
            Services.AddSingleton(AuthMock.Object);
            Services.AddMudServices();
            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        protected void GivenAuthenticated(bool authed) =>
            AuthMock.Setup(a => a.IsAuthenticatedAsync()).ReturnsAsync(authed);

        public Task InitializeAsync() => Task.CompletedTask;

        public new async Task DisposeAsync()
        {
            await Services.DisposeAsync();
            base.Dispose();
        }
    }
}
