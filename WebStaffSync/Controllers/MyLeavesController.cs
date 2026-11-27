using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStaffSync.Infrastructure;

namespace WebStaffSync.Controllers
{
    [Authorize]
    public class MyLeavesController : Controller
    {
        private readonly IConfiguration _configuration;

        public MyLeavesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(string? client)
        {
            if (TenantRouteHelper.GetConfiguredSlug(_configuration) is null)
                return StatusCode(500, "Tenant configuration is missing.");

            if (!TenantRouteHelper.TryResolve(
                client, _configuration, out var clientSlug, out var redirect))
                return NotFound();

            if (redirect)
                return RedirectToAction(nameof(Index), new { client = clientSlug });

            ViewBag.ClientSlug = clientSlug;

            return View();
        }
    }
}