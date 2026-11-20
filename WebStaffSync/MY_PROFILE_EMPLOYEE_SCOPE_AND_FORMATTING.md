# My Profile – employee scope and display rules

## Employee identity
- The login action places the authenticated employee database key in the protected cookie as `ClaimTypes.NameIdentifier`.
- MyProfileController reads only that claim. It does not accept an employee ID from a query string, form, or route parameter.
- The same employee ID is substituted into the configured profile API endpoint for the request.
- API/DAL queries in the upstream IntegrationService must also constrain every employee-related query by this employee ID; this WebStaffSync project cannot independently prove or enforce the upstream SQL predicates. Verify that service before production.

## Display rules
- Numeric integer values: `00000` (five-digit zero-padded).
- Fractional values and fields whose names indicate monetary values: `0.00`.
- Dates: `dd-MMM-yyyy`.
- Date/time values: `dd-MMM-yyyy hh:mm:ss tt`.
- Hide any returned property whose original column/property name contains `ID` (case-insensitive) or begins with `Is` (case-insensitive).
- Filtering applies to profile fields and dynamic tables. It does not delete or alter database records.

## Important verification
The API request is employee-specific from WebStaffSync. To guarantee the read layer is employee-specific, each related query in IntegrationService/DAL must use the authenticated employee ID as a parameter (including family, address, nominee, experience, skills, education, documents, bank, statutory, salary, leave, tax and weekly-off data). Do not trust an employee ID supplied by the browser.
