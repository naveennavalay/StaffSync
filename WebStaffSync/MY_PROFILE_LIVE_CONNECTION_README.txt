# WebStaffSync My Profile live API connection patch

This patch is for WebStaffSync only. It does not change the desktop StaffSync application.

## Root causes corrected
1. `IntegrationService:MyProfileEndpoint` was blank, so the page returned before making any HTTP request.
2. The WebStaffSync controller did not send the required `X-StaffSync-Profile-Key` header.
3. The IntegrationService response is an envelope (`success`, `employee`, `personal`, ...), not a `MyProfileViewModel`; deserializing the envelope directly into the view model left all fields empty.
4. The updated controller parses the envelope and maps its data into the existing read-only page model.
5. Grid sections now display actual returned DAL property names when they do not match the screenshot's preferred column labels.

## Required configuration (both applications)
- IntegrationService Web.config appSetting `WebStaffSyncProfileApiKey`
- WebStaffSync appsettings key `IntegrationService:ProfileApiKey`

Set both to the exact same long random secret. Do not commit production secrets. Prefer an environment variable / secret store in production:
`IntegrationService__ProfileApiKey`

## WebStaffSync endpoint
`IntegrationService:BaseUrl` is retained as `https://localhost:44326/`.
`IntegrationService:MyProfileEndpoint` is set to:
`api/employee-profile/{employeeId}`

Confirm the IntegrationService actually runs on the configured port. If its HTTPS port differs, update BaseUrl.

## Apply
1. Stop WebStaffSync.
2. Copy this patch's `Controllers/MyProfileController.cs`, `Views/MyProfile/Index.cshtml`, and `appsettings.json` over the corresponding WebStaffSync files (or merge the appsettings entries).
3. Confirm the IntegrationService API patch is added to the IntegrationService solution and built.
4. Set the same API secret in IntegrationService Web.config and WebStaffSync configuration.
5. Start IntegrationService first, then WebStaffSync. Sign out and sign in again.
6. Open `/MyProfile`.

## Expected diagnostic messages
- API endpoint missing: configuration has not been applied.
- API key missing: set `ProfileApiKey`.
- HTTP 401/403: keys do not match or API patch is not deployed.
- HTTP 404: route not deployed or wrong IntegrationService base URL.
- HTTP 500: inspect IntegrationService trace output; a DAL call/connection/model issue occurred.
- Success: employee, personal and related objects are mapped from the API response.

## Security
The employee ID is taken only from the authenticated `ClaimTypes.NameIdentifier` cookie claim. No employee ID is accepted from browser input. Profile endpoint is GET-only and the UI has no edit actions.
