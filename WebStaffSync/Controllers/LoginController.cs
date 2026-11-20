using Microsoft.AspNetCore.Mvc;

namespace WebStaffSync.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
