using Mintakt.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Mintakt.Client.Layout
{
    /// <summary>
    /// Base for components that need to know if the user is authenticated.
    /// Exposes IsAuthenticated and a LogoutAsync helper so layout/nav components
    /// don't duplicate the auth-check + logout-navigate boilerplate.
    /// </summary>
    public abstract class AuthAwareComponentBase : ComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected bool IsAuthenticated { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            IsAuthenticated = await AuthService.IsAuthenticatedAsync();
        }

        protected async Task LogoutAsync()
        {
            await AuthService.LogoutAsync();
            Navigation.NavigateTo("/login", forceLoad: true);
        }
    }

    /// <summary>
    /// Same as <see cref="AuthAwareComponentBase"/> but for layouts — adds the Body parameter.
    /// </summary>
    public abstract class AuthAwareLayoutBase : AuthAwareComponentBase
    {
        [Parameter] public RenderFragment? Body { get; set; }
    }
}
