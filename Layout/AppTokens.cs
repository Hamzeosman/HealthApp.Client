namespace Mintakt.Client.Layout
{
    /// <summary>
    /// Single source of truth for visual design values (the "theme map").
    /// Components and AppTheme both reference these constants — change a value
    /// here and it propagates everywhere. No hex codes should live elsewhere.
    ///
    /// Namespace history: originally HealthApp.Client.Layout in Phase 1
    /// (brand values only). Renamed to Mintakt.Client.Layout in Phase 2
    /// alongside the full HealthApp.Client → Mintakt.Client namespace migration.
    /// </summary>
    public static class AppTokens
    {
        // ── Brand ───────────────────────────────────────────────────
        public const string ProductName    = "Mintakt";
        public const string ProductTagline = "Hitta din takt";
        public const string ProductDomain  = "mintakt.app";
        // Brand assets — drop SVGs into wwwroot/assets/brand/
        public const string LogoMarkGradient = "assets/brand/mintakt-icon-gradient.svg";
        public const string LogoMarkWhite    = "assets/brand/mintakt-mark-white.svg";
        public const string LogoMarkOrange   = "assets/brand/mintakt-mark-orange.svg";
        public const string LogoLockup       = "assets/brand/mintakt-lockup-horizontal.svg";
        public const string Favicon          = "assets/brand/favicon.svg";
        public const string AppleTouchIcon   = "assets/brand/apple-touch-icon.svg";
        // ── Brand colors ────────────────────────────────────────────
        public const string Primary   = "#ff6b35"; // orange (Mintakt CTA)
        public const string Secondary = "#00d9ff"; // cyan
        public const string Tertiary  = "#a855f7"; // purple
        // ── Status colors ───────────────────────────────────────────
        public const string Success = "#10b981";
        public const string Info    = "#3b82f6";
        public const string Warning = "#f59e0b";
        public const string Error   = "#ef4444";
        // ── Surfaces (dark mode) ────────────────────────────────────
        public const string Background   = "#0d1117"; // page bg
        public const string Surface      = "#161b22"; // card bg
        public const string BorderSubtle = "#21262d"; // dividers, card borders
        public const string BorderStrong = "#30363d"; // input borders
        // ── Text colors ─────────────────────────────────────────────
        public const string TextPrimary   = "#e6edf3"; // headings, body
        public const string TextSecondary = "#b3bcc6"; // labels, descriptions
        public const string TextDisabled  = "#6e7681";
        // ── Composed values ─────────────────────────────────────────
        public const string CardBorder = "1px solid " + BorderSubtle;
        public const string HeroGradient =
            "linear-gradient(135deg, rgba(255,107,53,0.15) 0%, rgba(168,85,247,0.15) 100%)";
        public const string LogoGradient =
            "linear-gradient(135deg, #ff8a5e 0%, #ff6b35 55%, #a855f7 100%)";
        // ── Layout dimensions ───────────────────────────────────────
        public const string BorderRadius = "10px";
        public const string DrawerWidth = "260px";
        public const string AppBarHeight = "64px";
        // ── Typography ──────────────────────────────────────────────
        public static readonly string[] FontFamily =
            { "Inter", "Roboto", "system-ui", "sans-serif" };
    }
}