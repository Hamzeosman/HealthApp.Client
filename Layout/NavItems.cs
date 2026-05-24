using MudBlazor;

namespace HealthApp.Client.Layout
{
    public record NavItem(string Href, string Icon, string Label);
    public record NavGroup(string Title, NavItem[] Items);

    /// <summary>
    /// Single source of truth for the sidebar navigation.
    /// Add new pages here instead of hard-coding markup in NavMenu.razor.
    /// </summary>
    public static class NavItems
    {
        public static readonly NavGroup Training = new("TRÄNING", new[]
        {
            new NavItem("exercises",       Icons.Material.Filled.FitnessCenter, "Övningar"),
            new NavItem("training-plans",  Icons.Material.Filled.ListAlt,       "Träningsprogram"),
            new NavItem("log-workout",     Icons.Material.Filled.AddCircle,     "Logga pass"),
            new NavItem("workout-history", Icons.Material.Filled.History,       "Historik"),
            new NavItem("running",         Icons.Material.Filled.DirectionsRun, "Löpning"),
        });

        public static readonly NavGroup Health = new("HÄLSA", new[]
        {
            new NavItem("biometrics",        Icons.Material.Filled.MonitorHeart, "Biometri"),
            new NavItem("body-measurements", Icons.Material.Filled.Straighten,   "Kroppsmått"),
            new NavItem("nutrition",         Icons.Material.Filled.Restaurant,   "Kost"),
        });

        public static readonly NavGroup Insights = new("INSIKTER", new[]
        {
            new NavItem("recommendations", Icons.Material.Filled.Lightbulb,      "Rekommendationer"),
            new NavItem("achievements",    Icons.Material.Filled.EmojiEvents,    "Prestationer"),
            new NavItem("profile",         Icons.Material.Filled.AccountCircle,  "Min profil"),
        });

        public static readonly NavGroup[] Authenticated = { Training, Health, Insights };

        public static readonly NavItem[] Guest =
        {
            new("login",    Icons.Material.Filled.Login,      "Logga in"),
            new("register", Icons.Material.Filled.PersonAdd,  "Registrera"),
        };
    }
}
