# My Profile UI visibility fix

## What was corrected
- The My Profile Razor page now renders the employee identity card and all 12 read-only tabs even when the profile API reports an error or is not configured.
- The API warning remains visible at the top of the page; it no longer suppresses the profile controls.
- Empty profile sections show an explicit "No information available" placeholder.
- Existing My Profile navigation, styling, tab JavaScript, authentication cookie and employee-ID claim handling are retained.

## Live data dependency
The supplied WebStaffSync source configures IntegrationService at `https://localhost:44326/`, but the previous My Profile integration has an empty `IntegrationService:MyProfileEndpoint`. The uploaded files do not include the IntegrationService API source or an existing employee-profile route contract. Do not guess the route. Once the IntegrationService endpoint contract/source is supplied, connect it to the existing DALStaffSync profile retrieval methods and set the endpoint configuration.

This patch changes WebStaffSync only. It does not modify the desktop StaffSync project.
