using MudBlazor;

namespace HealthApp.Client.Layout
{
    /// <summary>
    /// Modern Fitness MudBlazor theme — wires <see cref="AppTokens"/> into a MudTheme
    /// so MudBlazor components inherit the design tokens automatically.
    /// Do not put raw hex values here — they belong in AppTokens.
    /// </summary>
    public static class AppTheme
    {
        public static readonly MudTheme ModernFitness = new()
        {
            PaletteDark = new PaletteDark
            {
                Primary   = AppTokens.Primary,
                Secondary = AppTokens.Secondary,
                Tertiary  = AppTokens.Tertiary,
                Success   = AppTokens.Success,
                Info      = AppTokens.Info,
                Warning   = AppTokens.Warning,
                Error     = AppTokens.Error,

                Background        = AppTokens.Background,
                Surface           = AppTokens.Surface,
                AppbarBackground  = AppTokens.Background,
                AppbarText        = AppTokens.TextPrimary,
                DrawerBackground  = AppTokens.Background,
                DrawerText        = AppTokens.TextPrimary,
                DrawerIcon        = AppTokens.TextSecondary,

                TextPrimary       = AppTokens.TextPrimary,
                TextSecondary     = AppTokens.TextSecondary,
                TextDisabled      = AppTokens.TextDisabled,

                ActionDefault             = AppTokens.TextSecondary,
                ActionDisabled            = AppTokens.TextDisabled,
                ActionDisabledBackground  = AppTokens.BorderSubtle,

                Divider      = AppTokens.BorderSubtle,
                DividerLight = AppTokens.BorderStrong,
                TableLines   = AppTokens.BorderSubtle,
                LinesDefault = AppTokens.BorderSubtle,
                LinesInputs  = AppTokens.BorderStrong,
            },

            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = AppTokens.BorderRadius,
                DrawerWidthLeft     = AppTokens.DrawerWidth,
                AppbarHeight        = AppTokens.AppBarHeight,
            },

            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = AppTokens.FontFamily
                }
            }
        };
    }
}
