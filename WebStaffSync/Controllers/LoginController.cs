using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebStaffSync.Models;
using WebStaffSync.Infrastructure;

namespace WebStaffSync.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;

        public LoginController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<LoginController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var clientSlug = TenantRouteHelper.GetConfiguredSlug(_configuration);
                if (string.IsNullOrWhiteSpace(clientSlug))
                    return StatusCode(500, "Tenant:ClientSlug is missing or invalid in WebStaffSync configuration.");
                return RedirectToAction("Index", "Home", new { client = clientSlug });
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                string apiBaseUrl = _configuration["IntegrationService:BaseUrl"];
                if (string.IsNullOrWhiteSpace(apiBaseUrl))
                    throw new InvalidOperationException("IntegrationService:BaseUrl is not configured.");

                var client = _httpClientFactory.CreateClient("IntegrationService");
                var response = await client.PostAsJsonAsync(
                    "api/auth/login",
                    new { UserName = model.UserName.Trim(), Password = model.Password });

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty,
                        response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                            ? "Sign-in is temporarily unavailable. Please try again later."
                            : "Unable to validate your sign-in details. Please try again.");
                    model.Password = string.Empty;
                    return View(model);
                }

                var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
                if (result == null || !result.Success || result.EmployeeId <= 0)
                {
                    ModelState.AddModelError(string.Empty,
                        result?.Message ?? "Username or password is incorrect, or the account is unavailable.");
                    model.Password = string.Empty;
                    return View(model);
                }

                // The employee identity is issued by the server in a protected,
                // HttpOnly authentication cookie. Never accept an EmployeeId from the browser.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Name, result.UserName ?? model.UserName.Trim())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                    });

                var clientSlug = TenantRouteHelper.GetConfiguredSlug(_configuration);
                if (string.IsNullOrWhiteSpace(clientSlug))
                    throw new InvalidOperationException("Tenant:ClientSlug is missing or invalid in WebStaffSync configuration.");
                return RedirectToAction("Index", "Home", new { client = clientSlug });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WebStaffSync could not complete the IntegrationService login request.");
                ModelState.AddModelError(string.Empty,
                    "We could not complete sign-in at this time. Please try again later.");
                model.Password = string.Empty;
                return View(model);
            }
        }

        private sealed class AuthenticationResult
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public int EmployeeId { get; set; }
            public string? UserName { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
    }
}
