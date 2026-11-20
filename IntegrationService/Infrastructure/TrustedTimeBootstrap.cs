using System;
using System.Diagnostics;
using Common.Helpers;

namespace IntegrationService.Infrastructure
{
    /// <summary>
    /// Owns the trusted-time lifecycle for the IntegrationService process.
    /// This instance is independent of the StaffSync desktop application's process.
    /// </summary>
    public static class TrustedTimeBootstrap
    {
        public static void TryInitializeAtStartup()
        {
            try
            {
                EnsureAccess();
                Trace.TraceInformation("IntegrationService trusted-time verification succeeded.");
            }
            catch (Exception ex)
            {
                // Keep the API process available so the portal can receive a controlled
                // service-unavailable response. Protected operations remain fail-closed.
                Trace.TraceError("IntegrationService trusted-time verification failed: {0}", ex);
            }
        }

        public static void EnsureAccess()
        {
            TrustedTimeAccessManager manager = TrustedTimeAccessManager.Instance;

            // If initial verification failed, allow a later request to retry initialization.
            // InitializeAsync also starts the manager's periodic revalidation timer on success.
            if (!manager.IsInitialized)
            {
                manager.InitializeAsync().GetAwaiter().GetResult();
            }

            manager.EnsureAccess();
        }

        public static void Stop()
        {
            TrustedTimeAccessManager.Instance.Stop();
        }
    }
}
