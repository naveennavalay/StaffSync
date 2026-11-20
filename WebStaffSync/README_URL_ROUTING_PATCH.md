# WebStaffSync tenant-prefixed URL routing patch

This patch changes **WebStaffSync only**. It does not modify the desktop StaffSync project or IntegrationService.

## What this patch changes

- Redirects a successfully authenticated employee to `/{clientname}/home`.
- Maps `/{clientname}/home` to `Home/Index`.
- Maps `/{clientname}/myprofile` to `MyProfile/Index`.
- Preserves the configured client slug in Dashboard, My Profile, brand/home, and Sign out links.
- Redirects legacy `/Home/Index` and `/MyProfile/Index` requests to the tenant-prefixed URL.
- Rejects a tenant slug that does not match this WebStaffSync deployment.
- Keeps employee identity sourced from the protected authentication cookie; the URL client slug does not select an employee or database.
- Preserves the existing My Profile API call and data mapping.

## Files in this ZIP

Copy the included files into the **WebStaffSync project root**, preserving their folders:

- `Program.cs`
- `Controllers/LoginController.cs`
- `Controllers/HomeController.cs`
- `Controllers/MyProfileController.cs`
- `Infrastructure/TenantRouteHelper.cs` (new)
- `Views/Shared/_Layout.cshtml`

`CONFIGURATION_SNIPPET.json` is a reference snippet only; do not replace your full appsettings files with it.

## Required configuration

Add this section to the existing `appsettings.Development.json` for local testing and to the production configuration for deployment. Merge it into the existing JSON; do not overwrite existing IntegrationService settings or secrets.

```json
"Tenant": {
  "ClientSlug": "your-client-name"
}
```

Replace `your-client-name` with the actual client URL slug, using lowercase letters, numbers, and hyphens only. Example: `acme-pharma`.

For production, you can instead set the environment variable:

`Tenant__ClientSlug=acme-pharma`

The slug is configured per WebStaffSync deployment. The current IntegrationService login response supplies `EmployeeId` and `UserName`, but no client/tenant slug, so this patch does not pretend to discover a tenant dynamically from the login response. If multiple tenants will share one WebStaffSync deployment, the login API must first be extended to return a validated tenant identifier and the tenant resolution design must be updated accordingly.

## Apply steps

1. Back up your current WebStaffSync project.
2. Stop the WebStaffSync application.
3. Extract this ZIP into the WebStaffSync project root and allow the included files to replace the corresponding files.
4. Add the `Tenant:ClientSlug` configuration block to your existing `appsettings.Development.json` (local) and production configuration, preserving all existing settings and API secrets.
5. Replace `your-client-name` with the correct client slug.
6. Rebuild and run WebStaffSync.
7. Sign out and sign in again.

## Expected URLs

If `Tenant:ClientSlug` is set to `acme-pharma`:

- Local Dashboard: `https://localhost:7089/acme-pharma/home`
- Local My Profile: `https://localhost:7089/acme-pharma/myprofile`
- Production Dashboard: `https://www.staffsync.com/acme-pharma/home`
- Production My Profile: `https://www.staffsync.com/acme-pharma/myprofile`

The host and port come from the local/production hosting configuration; the path structure is the same.

## Verification checklist

- Login succeeds and redirects to `/{clientname}/home`.
- Dashboard loads and the Dashboard navigation link retains the client slug.
- My Profile opens at `/{clientname}/myprofile` and continues loading the authenticated employee's profile.
- Sign out works from the tenant-prefixed route.
- Refreshing either page preserves the tenant-prefixed URL.
- `/another-client/home` returns 404 when it does not match the configured slug.
- No desktop StaffSync files or IntegrationService files are replaced by this patch.

## Build note

The source changes were reviewed against the supplied WebStaffSync project and the previously supplied My Profile integration patch. A .NET SDK was not available in the packaging environment, so a local Visual Studio build is still required for final compilation verification.
