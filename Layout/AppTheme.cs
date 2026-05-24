using MudBlazor;

namespace HealthApp.Client.Layout
{
    /// <summary>Modern Fitness dark theme: deep slate background, orange + cyan accents.</summary>
    public static class AppTheme
    {
        public static readonly MudTheme ModernFitness = new()
        {
            PaletteDark = new PaletteDark
            {
                Primary = "#ff6b35",
                Secondary = "#00d9ff",
                Tertiary = "#a855f7",
                Success = "#10b981",
                Info = "#3b82f6",
                Warning = "#f59e0b",
                Error = "#ef4444",

                Background = "#0d1117",
                Surface = "#161b22",
                AppbarBackground = "#0d1117",
                AppbarText = "#e6edf3",
                DrawerBackground = "#0d1117",
                DrawerText = "#e6edf3",
                DrawerIcon = "#8b949e",

                TextPrimary = "#e6edf3",
                TextSecondary = "#b3bcc6",
                TextDisabled = "#6e7681",

                ActionDefault = "#8b949e",
                ActionDisabled = "#484f58",
                ActionDisabledBackground = "#21262d",

                Divider = "#21262d",
                DividerLight = "#30363d",
                TableLines = "#21262d",
                LinesDefault = "#21262d",
                LinesInputs = "#30363d",
            },

            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "10px",
                DrawerWidthLeft = "260px",
                AppbarHeight = "64px",
            },

            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = new[] { "Inter", "Roboto", "system-ui", "sans-serif" }
                }
            }
        };
    }
}
