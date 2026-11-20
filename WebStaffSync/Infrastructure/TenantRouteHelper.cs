using System.Text.RegularExpressions;

namespace WebStaffSync.Infrastructure
{
    /// <summary>
    /// Resolves the tenant/client URL slug configured for this WebStaffSync deployment.
    /// The slug is a routing identifier only; authenticated employee identity remains
    /// sourced exclusively from the protected authentication cookie.
    /// </summary>
    public static class TenantRouteHelper
    {
        private static readonly Regex ValidSlug = new(
            "^[a-z0-9]+(?:-[a-z0-9]+)*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static string? GetConfiguredSlug(IConfiguration configuration)
        {
            var slug = configuration["Tenant:ClientSlug"]?.Trim().ToLowerInvariant();
            return !string.IsNullOrWhiteSpace(slug) && ValidSlug.IsMatch(slug) ? slug : null;
        }

        /// <summary>
        /// Returns false if the configured slug is missing/invalid or the requested
        /// route slug belongs to another tenant. A missing route slug can be redirected
        /// to the canonical tenant-prefixed URL by the caller.
        /// </summary>
        public static bool TryResolve(
            string? routeSlug,
            IConfiguration configuration,
            out string configuredSlug,
            out bool redirectToCanonical)
        {
            configuredSlug = GetConfiguredSlug(configuration) ?? string.Empty;
            redirectToCanonical = false;

            if (string.IsNullOrWhiteSpace(configuredSlug))
                return false;

            if (string.IsNullOrWhiteSpace(routeSlug))
            {
                redirectToCanonical = true;
                return true;
            }

            return string.Equals(routeSlug, configuredSlug, StringComparison.OrdinalIgnoreCase);
        }
    }
}
