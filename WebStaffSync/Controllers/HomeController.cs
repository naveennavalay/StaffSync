using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStaffSync.Infrastructure;
using WebStaffSync.Models;

namespace WebStaffSync.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(string? client)
        {
            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
                return StatusCode(500, "Tenant:ClientSlug is missing or invalid in WebStaffSync configuration.");

            if (!TenantRouteHelper.TryResolve(client, _configuration, out var clientSlug, out var redirect))
                return NotFound();

            if (redirect)
                return RedirectToAction(nameof(Index), new { client = clientSlug });

            ViewBag.EmployeeName = User.Identity?.Name ?? "Employee";
            ViewBag.ClientSlug = clientSlug;
            return View();
        }

        // Same-origin proxy: employee identity is read only from the protected cookie.
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> AttendanceState(string? client, CancellationToken cancellationToken)
        {
            if (!TryResolveEmployee(client, out var employeeId, out var error))
                return error!;

            return await ForwardAttendanceRequest(
                HttpMethod.Get, "State", employeeId, cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttendanceLogin(string? client, CancellationToken cancellationToken)
        {
            if (!TryResolveEmployee(client, out var employeeId, out var error))
                return error!;

            return await ForwardAttendanceRequest(
                HttpMethod.Post, "Login", employeeId, cancellationToken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttendanceLogout(string? client, CancellationToken cancellationToken)
        {
            if (!TryResolveEmployee(client, out var employeeId, out var error))
                return error!;

            return await ForwardAttendanceRequest(
                HttpMethod.Post, "Logout", employeeId, cancellationToken);
        }

        private bool TryResolveEmployee(string? client, out int employeeId, out IActionResult? error)
        {
            employeeId = 0;
            error = null;

            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
            {
                error = StatusCode(500, "Tenant configuration is missing or invalid.");
                return false;
            }

            if (!TenantRouteHelper.TryResolve(client, _configuration, out _, out var redirect))
            {
                error = NotFound();
                return false;
            }

            if (redirect)
            {
                error = BadRequest("Use the canonical tenant URL and retry.");
                return false;
            }

            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idText, out employeeId) || employeeId <= 0)
            {
                error = Challenge();
                return false;
            }

            return true;
        }

        private async Task<IActionResult> ForwardAttendanceRequest(
            HttpMethod method, string operation, int employeeId, CancellationToken cancellationToken)
        {
            var apiKey = _configuration["IntegrationService:ProfileApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                return StatusCode(500, "IntegrationService:ProfileApiKey is not configured.");

            var endpointTemplate = _configuration[$"IntegrationService:Attendance{operation}Endpoint"];
            if (string.IsNullOrWhiteSpace(endpointTemplate))
            {
                endpointTemplate = operation switch
                {
                    "State" => "api/employee-attendance/{employeeId}/state",
                    "Login" => "api/employee-attendance/{employeeId}/login",
                    "Logout" => "api/employee-attendance/{employeeId}/logout",
                    _ => throw new InvalidOperationException("Unsupported attendance operation.")
                };
            }

            var endpoint = endpointTemplate.Replace(
                "{employeeId}", employeeId.ToString(), StringComparison.OrdinalIgnoreCase);

            try
            {
                var client = _httpClientFactory.CreateClient("IntegrationService");
                using var request = new HttpRequestMessage(method, endpoint);
                request.Headers.Add("X-StaffSync-Profile-Key", apiKey);

                using var response = await client.SendAsync(
                    request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                return new ContentResult
                {
                    StatusCode = (int)response.StatusCode,
                    ContentType = "application/json; charset=utf-8",
                    Content = string.IsNullOrWhiteSpace(body) ? "{}" : body
                };
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WebStaffSync attendance {Operation} request failed for employee {EmployeeId}.",
                    operation, employeeId);

                return StatusCode((int)HttpStatusCode.BadGateway,
                    new { success = false, message = "Attendance service is currently unavailable. Please try again." });
            }
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
