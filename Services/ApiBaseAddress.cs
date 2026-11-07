namespace Mintakt.Client.Services
{
    /// <summary>
    /// Bestämmer vilken API-adress klienten använder.
    ///
    /// Värdet läses från ApiBaseUrl i wwwroot/appsettings*.json. I en worktree skriver
    /// scripts/worktree/worktree.mjs värdet till wwwroot/appsettings.Development.json.
    /// </summary>
    public static class ApiBaseAddress
    {
        public const string Default = "http://localhost:5137/";

        public static Uri Resolve(string? configured)
        {
            if (string.IsNullOrWhiteSpace(configured)) return new Uri(Default);

            if (!Uri.TryCreate(configured.Trim(), UriKind.Absolute, out var uri)
                || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new InvalidOperationException($"ApiBaseUrl måste vara en http- eller https-adress, men var \"{configured}\".");
            }

            return uri.AbsolutePath.EndsWith('/') ? uri : new Uri(uri + "/");
        }
    }
}
