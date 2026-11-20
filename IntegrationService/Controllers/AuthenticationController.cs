using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using IntegrationService.Infrastructure;

namespace IntegrationService.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthenticationController : ApiController
    {
        // POST api/auth/login
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            // Validate the request before attempting authentication.
            if (request == null ||
                string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return Content(HttpStatusCode.BadRequest, new LoginResponse
                {
                    Success = false,
                    Message = "Please enter both username and password."
                });
            }

            try
            {
                // Trusted-time validation is mandatory before authentication.
                TrustedTimeBootstrap.EnsureAccess();

                // IMPORTANT:
                // All database-related operations must go through DALStaffSync.
                // Do not create OleDbConnection/OleDbCommand here and do not
                // access dbStaffSync directly from this controller.
                var loginDal = new DALStaffSync.clsLogin();

                // Retrieve account details through the existing DAL method.
                var userInfo = loginDal.getSpecificUserInfo(request.UserName.Trim());

                // Reject unknown, deleted, inactive, locked or incomplete accounts.
                // The existing desktop AuthenticateUserInfo method does not
                // explicitly check IsDeleted, so check it before password validation.
                if (userInfo == null ||
                    userInfo.UserID <= 0 ||
                    userInfo.EmpID <= 0 ||
                    Convert.ToBoolean(userInfo.IsDeleted) ||
                    !Convert.ToBoolean(userInfo.IsActive) ||
                    Convert.ToBoolean(userInfo.IsLocked) ||
                    string.IsNullOrWhiteSpace(userInfo.EmpUserName))
                {
                    return InvalidCredentials();
                }

                // Use the existing DAL authentication method to validate the
                // password using StaffSync's current password compatibility logic.
                bool authenticated = loginDal.AuthenticateUserInfo(
                    request.UserName.Trim(),
                    request.Password);

                if (!authenticated)
                    return InvalidCredentials();

                // Do not call getLoggedInUserInfo() here.
                // That desktop method populates ModelStaffSync.CurrentUser,
                // which is shared static state and is not safe for concurrent
                // WebStaffSync employee sessions.

                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Login successful.",
                    EmployeeId = userInfo.EmpID,
                    UserName = userInfo.EmpUserName
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    "WebStaffSync authentication failed because a service operation failed: {0}",
                    ex);

                return Content(HttpStatusCode.ServiceUnavailable, new LoginResponse
                {
                    Success = false,
                    Message = "We could not complete sign-in at this time. Please try again later."
                });
            }
        }

        private IHttpActionResult InvalidCredentials()
        {
            // Keep the response generic; do not disclose whether a username
            // exists or whether an account is inactive, deleted or locked.
            return Ok(new LoginResponse
            {
                Success = false,
                Message = "Username or password is incorrect, or the account is unavailable."
            });
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
    }
}
