using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using DALStaffSync;
using ModelStaffSync;

namespace IntegrationService.Controllers
{
    /// <summary>
    /// Employee self-service attendance clock API.
    /// The employee ID is supplied by the trusted WebStaffSync server only.
    /// All attendance persistence is delegated to DALStaffSync.
    /// </summary>
    [RoutePrefix("api/employee-attendance")]
    public sealed class EmployeeAttendanceController : ApiController
    {
        private static readonly TimeSpan MaximumClockSkew = TimeSpan.FromSeconds(8);
        private const string ApiKeyHeader = "X-StaffSync-Profile-Key";

        [HttpGet]
        [Route("{employeeId:int}/state")]
        public async Task<IHttpActionResult> GetState(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");

            if (!IsTrustedWebStaffSyncRequest())
                return UnauthorizedResponse();

            try
            {
                DateTime now = await TrustedClock.GetIndiaLocalTimeAsync();
                var attendance = LoadAttendance(employeeId, now);

                if (attendance == null)
                    return Content(HttpStatusCode.NotFound, new
                    {
                        success = false,
                        message = "No assigned shift information was found for this employee."
                    });

                attendance.Success = true;
                attendance.Status = GetStatus(attendance);
                attendance.Message = string.Empty;
                return Ok(attendance);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "Attendance state retrieval failed for EmpID {0}: {1}", employeeId, ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{employeeId:int}/login")]
        public async Task<IHttpActionResult> Login(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");

            if (!IsTrustedWebStaffSyncRequest())
                return UnauthorizedResponse();

            try
            {
                DateTime now = await TrustedClock.GetIndiaLocalTimeAsync();
                var attendance = LoadAttendance(employeeId, now);

                if (attendance == null)
                    return ConflictResponse("No assigned shift information was found for this employee.");

                if (attendance.LoginTime.HasValue)
                    return ConflictResponse("Today's attendance has already been started.");

                // The desktop workflow updates an existing daily attendance row.
                // Do not silently create/alter leave or attendance rows here.
                if (attendance.AttID <= 0)
                    return ConflictResponse(
                        "Today's attendance record has not been generated yet. Please contact your administrator.");

                TimeSpan grace = attendance.GracePeriod.GetValueOrDefault().TimeOfDay;
                TimeSpan lateDuration = TimeSpan.Zero;

                if (attendance.ShiftLoginTime.HasValue)
                {
                    DateTime shiftStart = now.Date.Add(attendance.ShiftLoginTime.Value.TimeOfDay);
                    DateTime permittedLogin = shiftStart.Add(grace);
                    if (now > permittedLogin)
                        lateDuration = now - permittedLogin;
                }

                // Keep the existing Desktop DAL contract: the duration is
                // passed as a DateTime anchored to the actual login date.
                DateTime lateLoginValue = now.Date.Add(lateDuration);

                var dal = new clsAttendanceMas();
                int affected = dal.UpdateDailyLogInTime(
                    employeeId, now, now, lateLoginValue);

                if (affected <= 0)
                    return Content(HttpStatusCode.Conflict, new
                    {
                        success = false,
                        message = "Login could not be saved. Verify that today's attendance record exists."
                    });

                var updated = LoadAttendance(employeeId, now);
                if (updated == null)
                    throw new InvalidOperationException("Attendance was saved, but the updated attendance state could not be reloaded.");

                updated.Success = true;
                updated.Status = GetStatus(updated);
                updated.Message = "Login recorded successfully.";
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "Attendance login failed for EmpID {0}: {1}", employeeId, ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{employeeId:int}/logout")]
        public async Task<IHttpActionResult> Logout(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");

            if (!IsTrustedWebStaffSyncRequest())
                return UnauthorizedResponse();

            try
            {
                DateTime now = await TrustedClock.GetIndiaLocalTimeAsync();
                var attendance = LoadAttendance(employeeId, now);

                if (attendance == null)
                    return ConflictResponse("Today's attendance information could not be found.");

                if (!attendance.LoginTime.HasValue)
                    return ConflictResponse("Please Login before recording Logout.");

                if (attendance.LogoutTime.HasValue)
                    return ConflictResponse("Today's Logout has already been recorded.");

                TimeSpan grace = attendance.GracePeriod.GetValueOrDefault().TimeOfDay;
                TimeSpan earlyLogoutDuration = TimeSpan.Zero;

                if (attendance.ShiftLogoutTime.HasValue)
                {
                    DateTime shiftEnd = now.Date.Add(attendance.ShiftLogoutTime.Value.TimeOfDay);
                    DateTime permittedLogout = shiftEnd.Subtract(grace);

                    if (now < permittedLogout)
                        earlyLogoutDuration = permittedLogout - now;
                }

                TimeSpan totalDuration = now - attendance.LoginTime.Value;
                if (totalDuration < TimeSpan.Zero)
                    return ConflictResponse("The recorded Login time is later than the current trusted time. Please contact your administrator.");

                DateTime totalTimeValue = now.Date.Add(totalDuration);
                DateTime earlyLogoutValue = now.Date.Add(earlyLogoutDuration);

                var dal = new clsAttendanceMas();
                int affected = dal.UpdateDailyLogOutTime(
                    employeeId, now, now, totalTimeValue, earlyLogoutValue);

                if (affected <= 0)
                    return Content(HttpStatusCode.Conflict, new
                    {
                        success = false,
                        message = "Logout could not be saved. Please refresh and try again."
                    });

                var updated = LoadAttendance(employeeId, now);
                if (updated == null)
                    throw new InvalidOperationException("Logout was saved, but the updated attendance state could not be reloaded.");

                updated.Success = true;
                updated.Status = GetStatus(updated);
                updated.Message = "Logout recorded successfully. Today's attendance is complete.";
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "Attendance logout failed for EmpID {0}: {1}", employeeId, ex);
                return InternalServerError();
            }
        }

        // Read-only monthly attendance history for the authenticated employee.
        [HttpGet]
        [Route("{employeeId:int}/month")]
        public IHttpActionResult GetMonthlyHistory(int employeeId, DateTime? month = null)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");

            if (!IsTrustedWebStaffSyncRequest())
                return UnauthorizedResponse();

            try
            {
                DateTime selectedMonth = month.HasValue
                    ? new DateTime(month.Value.Year, month.Value.Month, 1)
                    : new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime dateFrom = selectedMonth;
                DateTime dateToExclusive = selectedMonth.AddMonths(1);

                var dal = new clsAttendanceMas();
                var monthlyRegister = dal.EmployeeSpecificMonthlyAttendanceInfo(employeeId, selectedMonth)
                    ?? new List<MonthlyAttendanceInfo>();
                var employeeHeader = dal.GetEmployeeAttendanceHeaderInfo(employeeId);
                if (employeeHeader != null && employeeHeader.EmpID > 0)
                {
                    if (monthlyRegister.Count == 0)
                    {
                        monthlyRegister.Add(employeeHeader);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(monthlyRegister[0].EmpCode))
                            monthlyRegister[0].EmpCode = employeeHeader.EmpCode;
                        if (string.IsNullOrWhiteSpace(monthlyRegister[0].EmpName))
                            monthlyRegister[0].EmpName = employeeHeader.EmpName;
                    }
                }
                var monthlyTotals = dal.GetEmployeeMonthlyWorkingDays(
                    employeeId, dateFrom, dateToExclusive);
                // Read the actual daily rows as the authoritative source for each calendar cell.
                var dailyAttendance = dal.GetDefaultEmployeeAttendanceInfo(employeeId, selectedMonth)
                    ?? new List<EmployeeAttendanceInfo>();

                return Ok(new
                {
                    success = true,
                    employeeId = employeeId,
                    month = selectedMonth.ToString("yyyy-MM"),
                    summary = new
                    {
                        workingDays = monthlyTotals,
                        presentDays = monthlyTotals,
                        paidLeaveDays = monthlyTotals,
                        lossOfPayDays = monthlyTotals
                    },
                    monthlyRegister = monthlyRegister,
                    dailyAttendance = dailyAttendance
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "Monthly attendance retrieval failed for EmpID {0}: {1}", employeeId, ex);
                return InternalServerError();
            }
        }

        // Read-only daily attendance details. This endpoint never updates attendance.
        [HttpGet]
        [Route("{employeeId:int}/day")]
        public IHttpActionResult GetDailyHistory(int employeeId, DateTime? date = null)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");
            if (!date.HasValue)
                return BadRequest("An attendance date is required.");
            if (!IsTrustedWebStaffSyncRequest())
                return UnauthorizedResponse();

            try
            {
                DateTime selectedDate = date.Value.Date;
                var dal = new clsAttendanceMas();

                // Use the dedicated date-scoped query. It reads the attendance
                // table directly and does not depend on the employee's latest
                // AttID or on an inner join to the current shift assignment.
                var clock = dal.GetEmployeeDailyAttendanceClockInfo(employeeId, selectedDate);
                var leaveInformation = dal.IsEmployeeOnLeave(employeeId, selectedDate);

                return Ok(new
                {
                    success = true,
                    employeeId = employeeId,
                    attendanceDate = selectedDate,
                    attendance = clock,
                    clock = clock,
                    leaveInformation = leaveInformation,
                    recordFound = clock != null && clock.AttID > 0
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "Daily attendance retrieval failed for EmpID {0}, Date {1}: {2}",
                    employeeId, date.Value, ex);
                return InternalServerError();
            }
        }

        private static EmployeeAttendanceClockInfo LoadAttendance(int employeeId, DateTime now)
        {
            var dal = new clsAttendanceMas();

            // The date-specific method returns the daily attendance row joined
            // to the assigned shift. If no row exists, use the default method
            // to still show the assigned shift and a NotLoggedIn state.
            List<EmployeeAttendanceClockInfo> rows =
                dal.getDailyLoginLogoutInformation(employeeId, now);

            EmployeeAttendanceClockInfo result =
                rows == null ? null : rows.FirstOrDefault();

            if (result == null)
            {
                List<EmployeeAttendanceClockInfo> defaults =
                    dal.getDailyLoginLogoutDefaultInformation(employeeId);

                result = defaults == null ? null : defaults.FirstOrDefault();
                if (result != null)
                {
                    result.AttDate = now.Date;
                    result.LoginTime = null;
                    result.LogoutTime = null;
                    result.TotalTime = null;
                    result.AttID = 0;
                }
            }

            return result;
        }

        private static string GetStatus(EmployeeAttendanceClockInfo item)
        {
            if (item == null || !item.LoginTime.HasValue)
                return "NotLoggedIn";

            return item.LogoutTime.HasValue ? "LoggedOut" : "LoggedIn";
        }

        private IHttpActionResult UnauthorizedResponse()
        {
            return Content(
                HttpStatusCode.Unauthorized,
                new
                {
                    success = false,
                    message = "The attendance request is not authorised."
                });
        }

        private IHttpActionResult ConflictResponse(string message)
        {
            return Content(HttpStatusCode.Conflict, new { success = false, message = message });
        }

        private bool IsTrustedWebStaffSyncRequest()
        {
            string configuredKey = ConfigurationManager.AppSettings["WebStaffSyncProfileApiKey"];
            string suppliedKey = null;

            if (Request.Headers.Contains(ApiKeyHeader))
                suppliedKey = Request.Headers.GetValues(ApiKeyHeader).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(configuredKey) ||
                configuredKey == "REPLACE_WITH_A_LONG_RANDOM_SECRET" ||
                string.IsNullOrWhiteSpace(suppliedKey))
                return false;

            using (var sha = SHA256.Create())
            {
                byte[] expected = sha.ComputeHash(Encoding.UTF8.GetBytes(configuredKey));
                byte[] actual = sha.ComputeHash(Encoding.UTF8.GetBytes(suppliedKey));
                int difference = expected.Length ^ actual.Length;
                for (int i = 0; i < expected.Length; i++)
                    difference |= expected[i] ^ actual[i];
                return difference == 0;
            }
        }

        /// <summary>
        /// Server-side trusted clock. It obtains HTTP Date samples from
        /// independent providers and requires two agreeing samples.
        /// The browser clock is never used for attendance persistence.
        /// </summary>
        private static class TrustedClock
        {
            private static readonly string[] Sources =
            {
                "https://www.google.com/generate_204",
                "https://www.microsoft.com",
                "https://www.cloudflare.com"
            };

            public static async Task<DateTime> GetIndiaLocalTimeAsync()
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(8);
                    var tasks = Sources.Select(async url =>
                    {
                        try
                        {
                            using (var request = new System.Net.Http.HttpRequestMessage(
                                System.Net.Http.HttpMethod.Get, url))
                            {
                                request.Headers.CacheControl =
                                    new System.Net.Http.Headers.CacheControlHeaderValue { NoCache = true };
                                var watch = System.Diagnostics.Stopwatch.StartNew();
                                using (var response = await client.SendAsync(
                                    request, System.Net.Http.HttpCompletionOption.ResponseHeadersRead))
                                {
                                    watch.Stop();
                                    if (!response.IsSuccessStatusCode || !response.Headers.Date.HasValue)
                                        return (DateTimeOffset?)null;

                                    return response.Headers.Date.Value.ToUniversalTime().AddMilliseconds(
                                        watch.Elapsed.TotalMilliseconds / 2.0);
                                }
                            }
                        }
                        catch
                        {
                            return (DateTimeOffset?)null;
                        }
                    }).ToArray();

                    DateTimeOffset?[] samples = await Task.WhenAll(tasks);
                    var valid = samples.Where(x => x.HasValue)
                        .Select(x => x.Value).OrderBy(x => x).ToList();

                    if (valid.Count < 2)
                        throw new InvalidOperationException(
                            "Trusted time could not be verified from at least two time sources. Attendance has not been changed.");

                    // Select the largest group of samples within the permitted skew.
                    var agreed = valid
                        .Select(reference => valid.Where(sample =>
                            Math.Abs((sample - reference).TotalSeconds) <= MaximumClockSkew.TotalSeconds).ToList())
                        .OrderByDescending(group => group.Count)
                        .First();

                    if (agreed.Count < 2)
                        throw new InvalidOperationException(
                            "Trusted time sources did not agree. Attendance has not been changed.");

                    long ticks = agreed.Select(x => x.UtcDateTime.Ticks).OrderBy(x => x).ElementAt(agreed.Count / 2);
                    var trustedUtc = new DateTimeOffset(ticks, TimeSpan.Zero);

                    TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    return TimeZoneInfo.ConvertTime(trustedUtc, zone).DateTime;
                }
            }
        }
    }
}
