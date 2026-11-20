using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using IntegrationService.Infrastructure;

namespace IntegrationService.Controllers
{
    [RoutePrefix("api/system")]
    public class SystemStatusController : ApiController
    {
        // GET api/system/trusted-time
        [HttpGet]
        [Route("trusted-time")]
        public IHttpActionResult GetTrustedTimeStatus()
        {
            try
            {
                TrustedTimeBootstrap.EnsureAccess();

                return Ok(new
                {
                    success = true,
                    trustedTimeAvailable = true,
                    message = "Trusted time verified."
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError("Trusted-time status check failed: {0}", ex);

                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    success = false,
                    trustedTimeAvailable = false,
                    message = "StaffSync cannot verify trusted date and time. Please try again later."
                });
            }
        }
    }
}
