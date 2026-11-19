using Microsoft.AspNetCore.Mvc;

namespace WebStaffSync.Controllers
{
    public class IntegrationTestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IntegrationTestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Ping()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync("https://localhost:44326/api/values/ping");

                var responseContent = await response.Content.ReadAsStringAsync();

                return Content($"HTTP Status: {(int)response.StatusCode}\n\n" + responseContent,"text/plain");
            }
            catch (Exception ex)
            {
                return Content($"Communication failed:\n{ex.Message}", "text/plain");
            }
        }
    }
}
