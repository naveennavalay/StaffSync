using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStaffSync.Models;
using WebStaffSync.Infrastructure;

namespace WebStaffSync.Controllers
{
    [Authorize]
    public sealed class MyProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MyProfileController> _logger;

        public MyProfileController(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ILogger<MyProfileController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(string? client, CancellationToken cancellationToken)
        {
            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
                return StatusCode(500, "Tenant:ClientSlug is missing or invalid in WebStaffSync configuration.");

            if (!TenantRouteHelper.TryResolve(client, _configuration, out var clientSlug, out var redirect))
                return NotFound();

            if (redirect)
                return RedirectToAction(nameof(Index), new { client = clientSlug });

            ViewBag.ClientSlug = clientSlug;

            // Employee identity comes only from the protected authentication cookie.
            var employeeIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(employeeIdText, out var employeeId) || employeeId <= 0)
                return Challenge();

            var model = new MyProfileViewModel { EmployeeName = User.Identity?.Name ?? "Employee" };
            var endpointTemplate = _configuration["IntegrationService:MyProfileEndpoint"];
            var apiKey = _configuration["IntegrationService:ProfileApiKey"];

            if (string.IsNullOrWhiteSpace(endpointTemplate))
            {
                model.ErrorMessage = "My Profile API endpoint is not configured. Set IntegrationService:MyProfileEndpoint to api/employee-profile/{employeeId}.";
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                model.ErrorMessage = "My Profile API key is not configured. Set IntegrationService:ProfileApiKey to the same secret configured in IntegrationService Web.config.";
                return View(model);
            }

            try
            {
                var endpoint = endpointTemplate.Replace("{employeeId}", employeeId.ToString(), StringComparison.OrdinalIgnoreCase);
                var client1 = _httpClientFactory.CreateClient("IntegrationService");
                using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                request.Headers.Add("X-StaffSync-Profile-Key", apiKey);
                using var response = await client1.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    model.ErrorMessage = "No employee profile was found for the authenticated employee ID.";
                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    model.ErrorMessage = "IntegrationService rejected the profile request. Verify that both applications use the same WebStaffSyncProfileApiKey.";
                    return View(model);
                }
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogWarning("My Profile API returned {StatusCode}. Response: {ResponseBody}",
                        (int)response.StatusCode, body);
                    model.ErrorMessage = $"IntegrationService returned HTTP {(int)response.StatusCode}. Check the IntegrationService log for the DAL error.";
                    return View(model);
                }

                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
                model = MapProfile(json.RootElement, User.Identity?.Name ?? "Employee", employeeId);
                return View(model);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load My Profile for authenticated employee {EmployeeId}.", employeeId);
                model.ErrorMessage = "Unable to connect to IntegrationService. Verify that it is running and that IntegrationService:BaseUrl is correct.";
                return View(model);
            }
        }

        private static MyProfileViewModel MapProfile(JsonElement root, string fallbackName, int requestedEmployeeId)
        {
            // IntegrationService returns an envelope: { success, employee, personal, ... }.
            if (root.ValueKind != JsonValueKind.Object)
                throw new InvalidOperationException("IntegrationService returned an invalid profile response.");
            if (TryGet(root, "success", out var success) && success.ValueKind == JsonValueKind.False)
                throw new InvalidOperationException("IntegrationService reported that profile retrieval failed.");

            var employee = Get(root, "employee");
            var personal = Get(root, "personal");

            // Fail closed if the API returns a different employee or omits the identity
            // key needed to prove that the response belongs to this authenticated user.
            var returnedEmployeeId = FirstText(employee, "EmpMasID", "EmpID", "EmployeeID", "EmployeeId", "EmpId");
            if (!int.TryParse(returnedEmployeeId, out var parsedEmployeeId) || parsedEmployeeId != requestedEmployeeId)
                throw new InvalidOperationException("The profile response employee ID does not match the authenticated employee. The profile was not displayed.");

            var model = new MyProfileViewModel
            {
                EmployeeName = FirstText(employee, "EmpName", "EmployeeName", "Name") ?? fallbackName,
                EmployeeCode = FirstText(employee, "EmpCode", "EmployeeCode"),
                Status = FirstText(employee, "Status") ?? "—",
                PhotoDataUrl = BuildPhotoUrl(Get(root, "photoBase64")),
                PersonalInfo = Fields(personal),
                ProfessionalInfo = Fields(employee),
                ContactInfo = MergeAddressFields(Get(root, "currentAddress"), Get(root, "permanentAddress"), personal),
                NominationInfo = Fields(Get(root, "nominee")),
                BankInfo = Fields(Get(root, "bank")),
                StatutoryInfo = Fields(Get(root, "statutory")),
                SalarySummary = Fields(Get(root, "salaryProfile")),
                LeaveSummary = Fields(Get(root, "leaveSummary")),
                FamilyMembers = Rows(Get(root, "familyMembers")),
                PreviousExperience = Rows(Get(root, "previousExperience")),
                Skills = Values(Get(root, "skills")),
                Education = Values(Get(root, "education")),
                Documents = Rows(Get(root, "documents")),
                LeaveBalances = Rows(Get(Get(root, "leaveSummary"), "leaveEntitlements")),
                SalaryComponents = Rows(Get(root, "salaryComponents")),
                TaxScheme = FirstText(Get(root, "taxScheme"), "TaxSchemeName", "TaxScheme", "SchemeName") ?? Scalar(Get(root, "taxScheme")),
                EffectiveFrom = FirstText(Get(root, "taxScheme"), "EffectDateFrom", "EffectiveFrom"),
                WeeklyOff = FirstText(Get(root, "weeklyOff"), "WeeklyOffTitle", "WeeklyOffName", "WeeklyOff") ?? Scalar(Get(root, "weeklyOff"))
            };

            // Contact and nominee records are separate DAL results; add contact/nominee labels
            // without exposing any editing capability.
            var contactPerson = Fields(Get(root, "contactPerson"), "Contact Person - ");
            model.ContactInfo.AddRange(contactPerson);
            model.SameAsCurrentAddress = "false";
            if (model.PersonalInfo.Count == 0 && model.ProfessionalInfo.Count == 0)
                throw new InvalidOperationException("The profile API response contained no employee or personal data.");
            return model;
        }

        private static List<ProfileField> MergeAddressFields(JsonElement current, JsonElement permanent, JsonElement personal)
        {
            var result = new List<ProfileField>();
            result.AddRange(Fields(current, "Current "));
            result.AddRange(Fields(permanent, "Permanent "));
            result.AddRange(Fields(personal).Where(x =>
                x.Label.Contains("Contact", StringComparison.OrdinalIgnoreCase) ||
                x.Label.Contains("Phone", StringComparison.OrdinalIgnoreCase) ||
                x.Label.Contains("Mail", StringComparison.OrdinalIgnoreCase)));
            return result;
        }

        // Hide database/internal columns from the employee-facing profile. The source
        // values remain available to the API/database layer and are not modified.
        private static bool ShouldHideColumn(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName)) return true;

            // Hide internal identity columns and boolean/status flag columns.
            // "Is" is checked case-insensitively as requested.
            return columnName.Contains("ID", StringComparison.OrdinalIgnoreCase)
                || columnName.Contains("Is", StringComparison.OrdinalIgnoreCase);
        }

        private static List<ProfileField> Fields(JsonElement element, string prefix = "")
        {
            var result = new List<ProfileField>();
            if (element.ValueKind != JsonValueKind.Object) return result;
            foreach (var p in element.EnumerateObject())
            {
                if (ShouldHideColumn(p.Name)) continue;
                if (p.Value.ValueKind is JsonValueKind.Object or JsonValueKind.Array or JsonValueKind.Null) continue;
                var value = FormatValue(p.Name, p.Value);
                if (string.IsNullOrWhiteSpace(value)) continue;
                result.Add(new ProfileField { Label = prefix + Humanize(p.Name), Value = value });
            }
            return result;
        }

        private static List<ProfileRow> Rows(JsonElement element)
        {
            var result = new List<ProfileRow>();
            if (element.ValueKind != JsonValueKind.Array) return result;
            foreach (var item in element.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object) continue;
                var row = new ProfileRow();
                foreach (var p in item.EnumerateObject())
                {
                    if (ShouldHideColumn(p.Name)) continue;
                    if (p.Value.ValueKind is JsonValueKind.Object or JsonValueKind.Array or JsonValueKind.Null) continue;
                    row.Values[Humanize(p.Name)] = FormatValue(p.Name, p.Value);
                }
                if (row.Values.Count > 0) result.Add(row);
            }
            return result;
        }

        private static string FormatValue(string propertyName, JsonElement value)
        {
            if (value.ValueKind == JsonValueKind.Null || value.ValueKind == JsonValueKind.Undefined)
                return string.Empty;

            var name = propertyName ?? string.Empty;
            var isDateField = name.Contains("Date", StringComparison.OrdinalIgnoreCase)
                || name.Contains("DOB", StringComparison.OrdinalIgnoreCase)
                || name.Contains("Timestamp", StringComparison.OrdinalIgnoreCase)
                || name.Contains("TimeStamp", StringComparison.OrdinalIgnoreCase);

            if (value.ValueKind == JsonValueKind.Number)
            {
                if (!value.TryGetDecimal(out var number)) return value.ToString();
                if (IsCurrencyField(name) || number % 1m != 0m)
                    return number.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                return number.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            var raw = Scalar(value);
            if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

            // All date/date-time fields in the employee profile are displayed as
            // date-only values in the requested dd-MMM-yyyy format.
            if (isDateField && DateTime.TryParse(raw,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AssumeLocal,
                    out var parsed))
            {
                return parsed.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            // Also normalize date-looking values when the database property name
            // is not explicit enough to identify it as a date field.
            if (raw.Length >= 8 && DateTime.TryParse(raw,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AssumeLocal,
                    out parsed) && (raw.Contains('-') || raw.Contains('/')))
            {
                return parsed.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return raw;
        }

        private static bool IsCurrencyField(string name)
        {
            string[] terms = { "Salary", "Amount", "Basic", "Allowance", "Deduction", "Reimburse",
                "Advance", "Gross", "Net", "Tax", "Bonus", "Rate", "CTC", "Wage", "Paid", "Payable" };
            return terms.Any(term => name.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        private static List<string> Values(JsonElement element)
        {
            var result = new List<string>();
            if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.String || item.ValueKind == JsonValueKind.Number)
                        result.Add(Scalar(item));
                    else if (item.ValueKind == JsonValueKind.Object)
                        result.AddRange(Fields(item).Select(x => x.Value));
                }
            }
            else if (element.ValueKind == JsonValueKind.Object)
                result.AddRange(Fields(element).Select(x => x.Value));
            return result.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        }

        private static string? BuildPhotoUrl(JsonElement element)
        {
            var base64 = Scalar(element);
            return string.IsNullOrWhiteSpace(base64) ? null : "data:image/jpeg;base64," + base64;
        }

        private static JsonElement Get(JsonElement element, string name) =>
            TryGet(element, name, out var value) ? value : default;

        private static bool TryGet(JsonElement element, string name, out JsonElement value)
        {
            if (element.ValueKind == JsonValueKind.Object)
                foreach (var p in element.EnumerateObject())
                    if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
                    { value = p.Value; return true; }
            value = default;
            return false;
        }

        private static string? FirstText(JsonElement element, params string[] names)
        {
            foreach (var name in names)
                if (TryGet(element, name, out var value))
                {
                    var text = Scalar(value);
                    if (!string.IsNullOrWhiteSpace(text)) return text;
                }
            return null;
        }

        private static string? Scalar(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.String) return element.GetString();
            if (element.ValueKind is JsonValueKind.Number or JsonValueKind.True or JsonValueKind.False)
                return element.ToString();
            return null;
        }

        private static string Humanize(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            // Central place to define employee-friendly column captions.
            // Add/adjust mappings here; the Index.cshtml preferredColumns arrays
            // should be used only to control column order.
            var captions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["FamMemName"] = "Member Name",
                ["FamMemDOB"] = "Date Of Birth",
                ["FamMemAge"] = "Age",
                ["FamMemRelationship"] = "Relationship",
                ["LastCompanyTitle"] = "Company Name",
                ["Address"] = "Company Address",
                ["StartDate"] = "Start Date",
                ["EndDate"] = "End Date",
                ["Comments"] = "More Details",
                ["DocCode"] = "Document Code",
                ["DocName"] = "Document Name",
                ["DocType"] = "Document Type",
                ["DocUploadDate"] = "Upload Date",
                ["LeaveTypeTitle"] = "Leave Type",
                ["TotalLeaves"] = "Total Leaves Allotted",
                ["TotalLeavesAvailable"] = "Total Leaves Available",
                ["TotalUtilisedLeaves"] = "Total Utilised Leaves",
                ["SalaryHeaderTitle"] = "Salary Header",
                ["SalHeaderTitle"] = "Salary Header",
                ["AllowanceAmount"] = "Allowance Amount",
                ["DeductionAmount"] = "Deduction Amount",
                ["ReimbursementAmount"] = "Reimbursement Amount"
            };

            if (captions.TryGetValue(value.Trim(), out var caption))
                return caption;

            value = Regex.Replace(value, "([a-z0-9])([A-Z])", "$1 $2");
            value = value.Replace("_", " ").Trim();
            return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }
    }
}
