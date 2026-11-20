using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStaffSync.Infrastructure;
using WebStaffSync.Models;

namespace WebStaffSync.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(string? client)
        {
            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
                return StatusCode(500, "Tenant:ClientSlug is missing or invalid in WebStaffSync configuration.");

            if (!TenantRouteHelper.TryResolve(client, _configuration, out var clientSlug, out var redirect))
                return NotFound();

            if (redirect)
                return RedirectToAction(nameof(Index), new { client = clientSlug });

            // EmployeeId is taken from the authenticated server-issued claim.
            // Do not accept an EmployeeId from query string, route or form data.
            ViewBag.EmployeeName = User.Identity?.Name ?? "Employee";
            ViewBag.ClientSlug = clientSlug;
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
