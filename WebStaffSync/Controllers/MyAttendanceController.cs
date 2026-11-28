using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStaffSync.Infrastructure;
using WebStaffSync.Models;

namespace WebStaffSync.Controllers
{
    [Authorize]
    public sealed class MyAttendanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MyAttendanceController> _logger;

        public MyAttendanceController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, ILogger<MyAttendanceController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(string? client, string? month,
            CancellationToken cancellationToken)
        {
            if (!TryResolveTenant(client, out var clientSlug, out var redirect))
                return NotFound();
            if (redirect)
                return RedirectToAction(nameof(Index), new { client = clientSlug, month = month });
            ViewBag.ClientSlug = clientSlug;

            if (!TryGetEmployeeId(out int employeeId))
                return Challenge();

            DateTime selectedMonth;
            if (!DateTime.TryParseExact(month, new[] { "yyyy-MM", "yyyy-MM-dd" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedMonth))
            {
                selectedMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
            else
            {
                selectedMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
            }
            var model = new MyAttendanceViewModel
            {
                EmployeeId = employeeId,
                EmployeeName = User.Identity?.Name ?? "Employee",
                SelectedMonth = selectedMonth
            };

            try
            {
                using var response = await SendAttendanceRequest(
                    $"api/employee-attendance/{employeeId}/month?month={selectedMonth:yyyy-MM-dd}",
                    cancellationToken);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    model.ErrorMessage = "No attendance information was found for the selected month.";
                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    model.ErrorMessage = "The attendance service rejected the request. Verify the shared IntegrationService API key.";
                    return View(model);
                }
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Monthly attendance API returned HTTP {StatusCode}.", (int)response.StatusCode);
                    model.ErrorMessage = $"Attendance service returned HTTP {(int)response.StatusCode}.";
                    return View(model);
                }

                using var json = await JsonDocument.ParseAsync(
                    await response.Content.ReadAsStreamAsync(cancellationToken),
                    cancellationToken: cancellationToken);
                var root = json.RootElement;
                EnsureSuccessfulResponse(root);
                var summary = Get(root, "summary");
                model.WorkingDays = GetDecimal(Get(summary, "workingDays"), "TotalDaysInMonth");
                model.PresentDays = GetDecimal(Get(summary, "presentDays"), "TotalPresent");
                model.PaidLeaveDays = GetDecimal(Get(summary, "paidLeaveDays"), "TotalPaidLeave");
                model.LossOfPayDays = GetDecimal(Get(summary, "lossOfPayDays"), "TotalLossOfPay");
                MapMonthlyDays(Get(root, "monthlyRegister"), Get(root, "dailyAttendance"), selectedMonth, model);
                model.AttendancePercentage = model.WorkingDays > 0m
                    ? Math.Round((model.PresentDays / model.WorkingDays) * 100m, 2) : 0m;

                await PopulateMonthlyHistoryAsync(employeeId, model, cancellationToken);
                return View(model);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load attendance for employee {EmployeeId}.", employeeId);
                model.ErrorMessage = "Unable to retrieve attendance. Please verify IntegrationService availability and configuration.";
                return View(model);
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> DayDetails(string? client, DateTime date,
            CancellationToken cancellationToken)
        {
            if (!TryResolveTenant(client, out _, out _))
                return NotFound();
            if (!TryGetEmployeeId(out int employeeId))
                return Unauthorized();

            try
            {
                using var response = await SendAttendanceRequest(
                    $"api/employee-attendance/{employeeId}/day?date={date:yyyy-MM-dd}",
                    cancellationToken);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode,
                        new { success = false, message = "Unable to retrieve attendance details for this date." });

                using var json = await JsonDocument.ParseAsync(
                    await response.Content.ReadAsStreamAsync(cancellationToken),
                    cancellationToken: cancellationToken);
                var root = json.RootElement;
                EnsureSuccessfulResponse(root);
                // The IntegrationService now returns the date-specific clock row
                // in both properties. Keep the fallback for compatibility with
                // an older deployed response shape.
                var attendance = Get(root, "attendance");
                var clock = Get(root, "clock");
                if (clock.ValueKind != JsonValueKind.Object)
                    clock = attendance;
                if (attendance.ValueKind != JsonValueKind.Object)
                    attendance = clock;

                var leave = FirstArrayItem(Get(root, "leaveInformation"));
                var result = new MyAttendanceDayDetailsViewModel
                {
                    Date = date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    AttendanceStatus = Text(attendance, "AttStatus") ?? Text(clock, "AttStatus") ?? "—",
                    LeaveComments = Text(attendance, "LeaveComments") ?? "—",
                    ShiftTitle = Text(clock, "ShiftTitle") ?? "—",
                    ShiftLoginTime = FormatClockTime(clock, "ShiftLoginTime"),
                    ShiftLogoutTime = FormatClockTime(clock, "ShiftLogoutTime"),
                    GracePeriod = FormatClockTime(clock, "GracePeriod"),
                    LoginTime = FormatClockTime(clock, "LoginTime"),
                    LateLoginTime = FormatClockTime(clock, "LateLoginTime"),
                    LateLoginDuration = FormatDurationBetween(clock, "LoginTime", "LateLoginTime"),
                    LogoutTime = FormatClockTime(clock, "LogoutTime"),
                    LateLogoutTime = FormatClockTime(clock, "LateLogoutTime"),
                    LateLogoutDuration = FormatDurationBetween(clock, "LogoutTime", "LateLogoutTime"),
                    TotalTime = FormatElapsedTime(clock, "TotalTime"),
                    LeaveMode = Text(leave, "LeaveMode") ?? "—",
                    LeaveDuration = Text(leave, "LeaveDuration") ?? "—"
                };

                bool recordFound = Get(root, "recordFound").ValueKind == JsonValueKind.True;
                string message = recordFound
                    ? string.Empty
                    : "No attendance record was found for this employee on the selected date.";
                return Json(new { success = true, recordFound = recordFound, message = message, details = result });
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve daily attendance for employee {EmployeeId}, date {Date}.", employeeId, date);
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving attendance details." });
            }
        }


        private async Task PopulateMonthlyHistoryAsync(
            int employeeId, MyAttendanceViewModel model,
            CancellationToken cancellationToken)
        {
            var history = new System.Collections.Generic.List<MyAttendanceHistoryRowViewModel>();
            var behaviour = new MyAttendanceBehaviourSummaryViewModel();

            foreach (var day in model.Days.OrderBy(x => x.Date))
            {
                var row = new MyAttendanceHistoryRowViewModel
                {
                    Date = day.Date,
                    Weekday = day.Date.ToString("ddd", CultureInfo.InvariantCulture),
                    Status = day.Status,
                    LoginTime = "—",
                    LogoutTime = "—",
                    EarlyLogin = "—",
                    LateLogin = "—",
                    EarlyLogout = "—",
                    LateLogout = "—",
                    TotalTime = "—"
                };

                // Shift comparisons are meaningful only for days with a recorded
                // present attendance. Other statuses remain visible in the history.
                if (day.HasRecord && string.Equals(day.Status, "P", StringComparison.OrdinalIgnoreCase))
                {
                    using var response = await SendAttendanceRequest(
                        $"api/employee-attendance/{employeeId}/day?date={day.Date:yyyy-MM-dd}",
                        cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        using var json = await JsonDocument.ParseAsync(
                            await response.Content.ReadAsStreamAsync(cancellationToken),
                            cancellationToken: cancellationToken);
                        var root = json.RootElement;
                        var clock = Get(root, "clock");
                        if (clock.ValueKind != JsonValueKind.Object)
                            clock = Get(root, "attendance");

                        row.LoginTime = FormatClockTime(clock, "LoginTime");
                        row.LogoutTime = FormatClockTime(clock, "LogoutTime");
                        row.TotalTime = FormatElapsedTime(clock, "TotalTime");

                        if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLoginTime")), out var shiftLogin) &&
                            TryGetTimeOfDay(Scalar(Get(clock, "LoginTime")), out var actualLogin))
                        {
                            var grace = TryGetTimeOfDay(Scalar(Get(clock, "GracePeriod")), out var parsedGrace)
                                ? parsedGrace : TimeSpan.Zero;

                            var early = actualLogin < shiftLogin ? shiftLogin - actualLogin : TimeSpan.Zero;
                            var lateThreshold = shiftLogin + grace;
                            var late = actualLogin > lateThreshold ? actualLogin - lateThreshold : TimeSpan.Zero;

                            row.EarlyLoginMinutes = (decimal)early.TotalMinutes;
                            row.LateLoginMinutes = (decimal)late.TotalMinutes;
                            row.EarlyLogin = FormatDuration(early);
                            row.LateLogin = FormatDuration(late);

                            if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLogoutTime")), out var shiftEndForLogin))
                                row.EarlyLoginPercent = PercentageOfShift(early, shiftLogin, shiftEndForLogin);
                            if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLogoutTime")), out shiftEndForLogin))
                                row.LateLoginPercent = PercentageOfShift(late, shiftLogin, shiftEndForLogin);
                        }

                        if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLogoutTime")), out var shiftLogout) &&
                            TryGetTimeOfDay(Scalar(Get(clock, "LogoutTime")), out var actualLogout))
                        {
                            var grace = TryGetTimeOfDay(Scalar(Get(clock, "GracePeriod")), out var parsedGrace)
                                ? parsedGrace : TimeSpan.Zero;

                            var earlyThreshold = shiftLogout - grace;
                            var early = actualLogout < earlyThreshold ? earlyThreshold - actualLogout : TimeSpan.Zero;
                            var late = actualLogout > shiftLogout ? actualLogout - shiftLogout : TimeSpan.Zero;

                            row.EarlyLogoutMinutes = (decimal)early.TotalMinutes;
                            row.LateLogoutMinutes = (decimal)late.TotalMinutes;
                            row.EarlyLogout = FormatDuration(early);
                            row.LateLogout = FormatDuration(late);

                            if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLoginTime")), out var shiftStartForLogout))
                                row.EarlyLogoutPercent = PercentageOfShift(early, shiftStartForLogout, shiftLogout);
                            if (TryGetTimeOfDay(Scalar(Get(clock, "ShiftLoginTime")), out shiftStartForLogout))
                                row.LateLogoutPercent = PercentageOfShift(late, shiftStartForLogout, shiftLogout);
                        }
                    }

                    behaviour.EligibleDays++;
                    if (row.EarlyLoginMinutes > 0) behaviour.EarlyLoginDays++;
                    if (row.LateLoginMinutes > 0) behaviour.LateLoginDays++;
                    if (row.EarlyLogoutMinutes > 0) behaviour.EarlyLogoutDays++;
                    if (row.LateLogoutMinutes > 0) behaviour.LateLogoutDays++;

                    behaviour.EarlyLoginMinutes += row.EarlyLoginMinutes;
                    behaviour.LateLoginMinutes += row.LateLoginMinutes;
                    behaviour.EarlyLogoutMinutes += row.EarlyLogoutMinutes;
                    behaviour.LateLogoutMinutes += row.LateLogoutMinutes;
                }

                history.Add(row);
            }

            model.MonthlyHistory = history;
            BuildAttendanceInsights(history, behaviour);
            model.Behaviour = behaviour;
        }

        // Per-day percentages are the event duration divided by the scheduled
        // shift duration (shift end minus shift start), expressed as a percentage.
        private static decimal PercentageOfShift(TimeSpan eventDuration, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            var shiftDuration = shiftEnd - shiftStart;
            if (shiftDuration <= TimeSpan.Zero)
                shiftDuration += TimeSpan.FromDays(1);

            if (shiftDuration <= TimeSpan.Zero)
                return 0m;

            return Math.Round((decimal)eventDuration.TotalMinutes /
                (decimal)shiftDuration.TotalMinutes * 100m, 2);
        }

        private static void BuildAttendanceInsights(
            System.Collections.Generic.List<MyAttendanceHistoryRowViewModel> history,
            MyAttendanceBehaviourSummaryViewModel behaviour)
        {
            if (behaviour.EligibleDays == 0)
            {
                behaviour.Insights.Add("No present attendance days with shift details were available for analysis in this month.");
                return;
            }

            behaviour.Insights.Add(
                $"Late login was recorded on {behaviour.LateLoginDays} of {behaviour.EligibleDays} eligible present days ({behaviour.LateLoginPercent:0.00}%).");
            behaviour.Insights.Add(
                $"Early logout was recorded on {behaviour.EarlyLogoutDays} of {behaviour.EligibleDays} eligible present days ({behaviour.EarlyLogoutPercent:0.00}%).");

            var weekdayPattern = history
                .Where(x => x.Status == "P" && (x.LateLoginMinutes > 0 || x.EarlyLogoutMinutes > 0))
                .GroupBy(x => x.Weekday)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            if (weekdayPattern != null)
                behaviour.Insights.Add(
                    $"The most frequent weekday for late login or early logout was {weekdayPattern.Day} ({weekdayPattern.Count} occurrence(s)).");

            if (behaviour.LateLoginDays == 0 && behaviour.EarlyLogoutDays == 0)
                behaviour.Insights.Add("No late-login or early-logout pattern was detected in the available records.");
        }

        private async Task<HttpResponseMessage> SendAttendanceRequest(string relativeUrl,
            CancellationToken cancellationToken)
        {
            var apiKey = _configuration["IntegrationService:ProfileApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("IntegrationService:ProfileApiKey is not configured.");

            var http = _httpClientFactory.CreateClient("IntegrationService");
            using var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            request.Headers.Add("X-StaffSync-Profile-Key", apiKey);
            return await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }

        private bool TryResolveTenant(string? client, out string clientSlug, out bool redirect)
        {
            clientSlug = null;
            redirect = false;
            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
                return false;
            return TenantRouteHelper.TryResolve(client, _configuration, out clientSlug, out redirect);
        }

        private bool TryGetEmployeeId(out int employeeId)
        {
            return int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out employeeId)
                && employeeId > 0;
        }

        private static void EnsureSuccessfulResponse(JsonElement root)
        {
            if (root.ValueKind != JsonValueKind.Object ||
                (TryGet(root, "success", out var success) && success.ValueKind == JsonValueKind.False))
                throw new InvalidOperationException("IntegrationService returned an unsuccessful attendance response.");
        }


        private static void MapMonthlyDays(
            JsonElement register, JsonElement dailyAttendance,
            DateTime month, MyAttendanceViewModel model)
        {
            JsonElement row = register.ValueKind == JsonValueKind.Array && register.GetArrayLength() > 0
                ? register[0] : default;

            if (row.ValueKind == JsonValueKind.Object)
            {
                model.EmployeeCode = Text(row, "EmpCode") ?? "";
                model.EmployeeName = Text(row, "EmpName") ?? model.EmployeeName;
                model.Department = Text(row, "DepartmentTitle") ?? "";
                model.Designation = Text(row, "DesignationTitle") ?? "";
            }

            // The monthly query is retained for employee metadata and compatibility.
            // Daily attendance rows are the authoritative source for day-by-day status.
            var statusByDay = new System.Collections.Generic.Dictionary<int, string>();
            if (dailyAttendance.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement attendance in dailyAttendance.EnumerateArray())
                {
                    string dateText = Text(attendance, "AttDate");
                    string statusText = Text(attendance, "AttStatus");
                    if (DateTime.TryParse(dateText, CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind, out DateTime attendanceDate) &&
                        attendanceDate.Year == month.Year && attendanceDate.Month == month.Month)
                    {
                        statusByDay[attendanceDate.Day] = statusText ?? "";
                    }
                }
            }

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                string raw = statusByDay.TryGetValue(day, out string dailyStatus)
                    ? dailyStatus
                    : (row.ValueKind == JsonValueKind.Object ? Scalar(Get(row, "Day" + day)) : null);

                string status = NormalizeDayStatus(raw);
                string code = status.Trim().ToUpperInvariant();

                if (code == "P") model.PresentCount++;
                else if (code == "L") model.LeaveCount++;
                else if (code == "C") model.CancelledCount++;
                else if (code == "R") model.RejectedCount++;

                DateTime date = new DateTime(month.Year, month.Month, day);
                model.Days.Add(new MyAttendanceDayViewModel
                {
                    Date = date,
                    Weekday = date.ToString("ddd", CultureInfo.InvariantCulture),
                    Status = status,
                    HasRecord = !string.IsNullOrWhiteSpace(raw) && status != "—",
                    StatusClass = GetStatusClass(code)
                });
            }
        }


        private static string NormalizeDayStatus(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "—";
            string value = raw.Trim();
            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
                value = value.Substring(1, value.Length - 2).Trim();
            if (value.Equals("null", StringComparison.OrdinalIgnoreCase)) return "—";

            string upper = value.ToUpperInvariant();
            if (upper == "P" || upper == "PRESENT" || upper.StartsWith("PRESENT ")) return "P";
            if (upper == "L" || upper.Contains("LEAVE")) return "L";
            if (upper == "C" || upper.Contains("CANCEL")) return "C";
            if (upper == "R" || upper.Contains("REJECT")) return "R";
            if (upper.Contains("LOSS OF PAY")) return "LOP";
            if (upper.Contains("ABSENT")) return "A";
            return value;
        }

        private static string GetStatusClass(string code)
        {
            if (code == "P" || code.StartsWith("P ")) return "status-present";
            if (code == "L" || code.StartsWith("L ")) return "status-leave";
            if (code == "C" || code.StartsWith("C ")) return "status-cancelled";
            if (code == "R" || code.StartsWith("R ")) return "status-rejected";
            if (code == "LOP" || code == "A") return "status-rejected";
            return "status-none";
        }

        private static decimal GetDecimal(JsonElement element, string property)
        {
            var value = Get(element, property);
            if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var number)) return number;
            return decimal.TryParse(Scalar(value), NumberStyles.Any, CultureInfo.InvariantCulture, out number) ? number : 0m;
        }

        private static JsonElement FirstArrayItem(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Array && element.GetArrayLength() > 0)
                return element[0];
            return element;
        }

        private static string FormatClockTime(JsonElement element, string property)
        {
            string raw = Scalar(Get(element, property));
            if (string.IsNullOrWhiteSpace(raw)) return "—";

            if (DateTime.TryParse(raw, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out DateTime dateTime))
                return dateTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);

            if (TimeSpan.TryParse(raw, CultureInfo.InvariantCulture, out TimeSpan timeSpan))
                return DateTime.Today.Add(timeSpan).ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);

            return raw;
        }

        private static string FormatElapsedTime(JsonElement element, string property)
        {
            string raw = Scalar(Get(element, property));
            if (string.IsNullOrWhiteSpace(raw)) return "—";

            if (DateTime.TryParse(raw, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out DateTime dateTime))
                return dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

            if (TimeSpan.TryParse(raw, CultureInfo.InvariantCulture, out TimeSpan timeSpan))
                return FormatDuration(timeSpan);

            return raw;
        }

        private static string FormatDurationBetween(JsonElement element, string actualProperty, string referenceProperty)
        {
            string actualRaw = Scalar(Get(element, actualProperty));
            string referenceRaw = Scalar(Get(element, referenceProperty));
            if (!TryGetTimeOfDay(actualRaw, out TimeSpan actual) ||
                !TryGetTimeOfDay(referenceRaw, out TimeSpan reference))
                return "—";

            // The database stores the actual and late-time values as time values.
            // Their absolute difference is the late-login/late-logout duration.
            return FormatDuration((actual - reference).Duration());
        }

        private static bool TryGetTimeOfDay(string raw, out TimeSpan value)
        {
            value = TimeSpan.Zero;
            if (string.IsNullOrWhiteSpace(raw)) return false;

            if (DateTime.TryParse(raw, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out DateTime dateTime))
            {
                value = dateTime.TimeOfDay;
                return true;
            }

            if (TimeSpan.TryParse(raw, CultureInfo.InvariantCulture, out value))
                return true;

            return false;
        }

        private static string FormatDuration(TimeSpan duration)
        {
            if (duration < TimeSpan.Zero) duration = duration.Duration();
            return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}",
                (int)duration.TotalHours, duration.Minutes, duration.Seconds);
        }

        private static string? Text(JsonElement element, string property) => Scalar(Get(element, property));
        private static string? Scalar(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined || element.ValueKind == JsonValueKind.Null) return null;
            if (element.ValueKind == JsonValueKind.String) return element.GetString();
            if (element.ValueKind == JsonValueKind.Object || element.ValueKind == JsonValueKind.Array) return element.GetRawText();
            return element.ToString();
        }
        private static JsonElement Get(JsonElement element, string property)
        {
            if (element.ValueKind == JsonValueKind.Object)
                foreach (var item in element.EnumerateObject())
                    if (string.Equals(item.Name, property, StringComparison.OrdinalIgnoreCase)) return item.Value;
            return default;
        }
        private static bool TryGet(JsonElement element, string property, out JsonElement value)
        {
            value = Get(element, property);
            return value.ValueKind != JsonValueKind.Undefined;
        }
    }
}
