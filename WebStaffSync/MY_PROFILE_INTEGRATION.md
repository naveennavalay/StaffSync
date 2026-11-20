# WebStaffSync - My Profile integration

## What this change contains
- Adds an authenticated `MyProfile/Index` page and active sidebar navigation.
- Reads the employee ID only from the protected `ClaimTypes.NameIdentifier` claim created by the existing login flow.
- Calls the configured IntegrationService endpoint server-to-server; the browser cannot supply an employee ID.
- Renders all 12 profile areas as read-only information using the existing WebStaffSync theme.

## Required IntegrationService configuration
Set `IntegrationService:MyProfileEndpoint` in appsettings (or an environment-specific configuration source) to the actual GET route exposed by IntegrationService. The value must contain `{employeeId}`, for example:

`api/<confirmed-profile-route>/{employeeId}`

Do not use the example text literally. The supplied WebStaffSync ZIP contains the IntegrationService base URL and login API call, but does not contain the IntegrationService API project or an existing employee-profile API route. Therefore the exact route and response contract cannot be verified from this source package.

## Required JSON response contract
The configured GET endpoint must return JSON matching `WebStaffSync.Models.MyProfileViewModel`:
- `employeeCode`, `employeeName`, `status`, `photoDataUrl`
- `personalInfo`, `professionalInfo`, `contactInfo`, `nominationInfo`, `bankInfo`, `statutoryInfo`, `salarySummary`, `leaveSummary`: arrays of `{ "label": "...", "value": "..." }`
- `familyMembers`, `previousExperience`, `documents`, `leaveBalances`, `salaryComponents`: arrays of `{ "values": { "Column label": "value" } }`
- `skills`, `education`: arrays of strings
- `taxScheme`, `effectiveFrom`, `weeklyOff`, `sameAsCurrentAddress`

`photoDataUrl` should be a safe data URL such as `data:image/jpeg;base64,...`, or null. Avoid returning physical server paths.

## DAL boundary
The IntegrationService API implementation must obtain these values from DALStaffSync using the already existing Employee Master retrieval methods. DALStaffSync is not referenced by or included in the supplied WebStaffSync project, so no direct DAL/database code has been added to WebStaffSync.

## Files added/changed
- Added `Controllers/MyProfileController.cs`
- Added `Models/MyProfileViewModel.cs`
- Added `Views/MyProfile/Index.cshtml`
- Added `wwwroot/css/my-profile.css`
- Added `wwwroot/js/my-profile.js`
- Changed `Views/Shared/_Layout.cshtml` (My Profile navigation)
- Changed `appsettings.json` (empty endpoint setting)

## Verification
Build with .NET 8. Configure the confirmed endpoint before expecting live employee data. If the endpoint is not yet available, the page deliberately displays a clear service-not-configured message rather than sample or fabricated employee information.
