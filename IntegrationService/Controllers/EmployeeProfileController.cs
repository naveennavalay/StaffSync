using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using DALStaffSync;
using ModelStaffSync;

namespace IntegrationService.Controllers
{
    /// <summary>
    /// Read-only employee profile API for WebStaffSync.
    /// All database operations are performed through DALStaffSync.
    /// </summary>
    [RoutePrefix("api/employee-profile")]
    public class EmployeeProfileController : ApiController
    {
        [HttpGet]
        [Route("{employeeId:int}")]
        public IHttpActionResult GetEmployeeProfile(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest("A valid employee identity is required.");

            if (!IsTrustedWebStaffSyncRequest())
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new { success = false, message = "The profile request is not authorised." }));

            try
            {
                // Reuse the same DAL classes and retrieval methods used by
                // frmEmployeeMaster.UpdateUIWithSelectedEmployeeDetails().
                var employeeDal = new clsEmployeeMaster();
                var personalDal = new clsEmployeePersonalInfo();
                var addressDal = new clsAddressInfo();
                var photoDal = new clsPhotoMas();
                var employmentDal = new clsEmploymentTypeInfo();
                var shiftDal = new clsShiftMas();
                var contactDal = new clsEmpContactPersonMas();
                var nomineeDal = new clsEmpNomineeMas();
                var statutoryDal = new clsEmployeePersonalIDInfo();
                var familyDal = new clsEmpPersonalFamilyMemberInfo();
                var educationDal = new clsEmpEduQualMas();
                var skillsDal = new clsEmpSkillsMas();
                var experienceDal = new clsEmpWorkExperienceInfo();
                var taxDal = new clsTaxMas();
                var weeklyOffDal = new clsWeeklyOffInfo();
                var leaveDal = new clsLeaveTRList();
                var leaveEntitlementDal = new clsEmpLeaveEntitlementInfo();
                var documentsDal = new clsUploadDocuments();
                var bankDal = new clsBankMas();
                var salaryDal = new clsSalaryProfile();

                var employee = employeeDal.GetSelectedEmployeeInfo(employeeId);
                if (employee == null)
                    return NotFound();

                var personal = personalDal.GetEmpPersonalPersonalInfo(employeeId);
                if (personal == null)
                    return Ok(new { success = true, employee = employee });

                var currentAddress = personal.CurAddressID > 0
                    ? addressDal.GetAddressInfo(Convert.ToInt32(personal.CurAddressID)) : null;
                var permanentAddress = personal.PerAddressID > 0
                    ? addressDal.GetAddressInfo(Convert.ToInt32(personal.PerAddressID)) : null;

                var reportingManager = employee.EmpRepManID > 0
                    ? employeeDal.GetReportingManagerInfo(employee.EmpRepManID) : null;

                var photo = photoDal.getEmployeePhoto(employeeId);
                var contact = personal.ContactID1 > 0
                    ? contactDal.GetContactInfo(personal.ContactID1) : null;
                var nominee = nomineeDal.GetNomineeInfo(employeeId);
                var statutory = statutoryDal.GetEmpPersonalIDInfo(personal.PersonalInfoID);

                var family = familyDal.GetEmpPersonalFamilyMemberInfo(personal.PersonalInfoID);
                var education = educationDal.GetEduQualListInfo(employeeId);
                var skills = skillsDal.GetSkillsListInfo(employeeId);
                var experience = experienceDal.GetWorkExpListInfo(employeeId);
                var employment = employmentDal.getEmployeeSpecificEmploymentTypeInfo(employeeId);
                var shift = shiftDal.getEmployeeSpecificShiftInfo(employeeId);
                var taxScheme = taxDal.getEmployeeSpecificTaxSchemeModel(employeeId);
                var weeklyOffHistory = weeklyOffDal.getEmployeeSpecificWeeklyOffMasterInfo(employeeId);
                var weeklyOff = weeklyOffHistory == null ? null :
                    weeklyOffHistory.OrderByDescending(x => x.EffectDateFrom).FirstOrDefault();

                int leaveMasId = leaveDal.getMaxLeaveMasID(employeeId);
                var leaveEntitlements = leaveMasId > 0
                    ? leaveEntitlementDal.getEmployeeLeaveEntitilementList(employeeId, leaveMasId)
                    : null;
                var leaveTransactions = leaveDal.getEmployeeLeaveTRList(employeeId);
                var documents = documentsDal.getEmployeeSpecificDocumentsList(employeeId);
                var bank = bankDal.GetEmployeeSpecificBankInfo(employeeId);
                var salaryProfile = salaryDal.getEmployeeSpecificSalaryProfile(employeeId);
                var salaryComponents = salaryDal.GetEmployeeSpecificSalaryProfileInfo(employeeId);

                string photoBase64 = null;
                if (photo != null && photo.EmpPhoto != null && photo.EmpPhoto.Length > 0)
                    photoBase64 = Convert.ToBase64String(photo.EmpPhoto);

                return Ok(new
                {
                    success = true,
                    employeeId = employeeId,
                    employee = employee,
                    personal = personal,
                    currentAddress = currentAddress,
                    permanentAddress = permanentAddress,
                    reportingManager = reportingManager,
                    photoBase64 = photoBase64,
                    contactPerson = contact,
                    nominee = nominee,
                    statutory = statutory,
                    familyMembers = family,
                    education = education,
                    skills = skills,
                    previousExperience = experience,
                    employmentType = employment,
                    shift = shift,
                    taxScheme = taxScheme,
                    weeklyOff = weeklyOff,
                    weeklyOffHistory = weeklyOffHistory,
                    leaveSummary = new
                    {
                        totalBalance = leaveDal.getBalanceLeave(employeeId),
                        leaveEntitlements = leaveEntitlements,
                        leaveTransactions = leaveTransactions
                    },
                    documents = documents,
                    bank = bank,
                    salaryProfile = salaryProfile,
                    salaryComponents = salaryComponents
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError("Employee profile retrieval failed for EmpID {0}: {1}", employeeId, ex);
                return Content(HttpStatusCode.InternalServerError, new
                {
                    success = false,
                    message = "Employee profile information could not be retrieved. Please check the IntegrationService and DALStaffSync configuration."
                });
            }
        }

        private bool IsTrustedWebStaffSyncRequest()
        {
            string configuredKey = ConfigurationManager.AppSettings["WebStaffSyncProfileApiKey"];
            string suppliedKey = null;

            if (Request.Headers.Contains("X-StaffSync-Profile-Key"))
                suppliedKey = Request.Headers.GetValues("X-StaffSync-Profile-Key").FirstOrDefault();

            if (string.IsNullOrWhiteSpace(configuredKey) ||
                configuredKey == "REPLACE_WITH_A_LONG_RANDOM_SECRET" ||
                string.IsNullOrWhiteSpace(suppliedKey))
                return false;

            // Compare fixed-length SHA-256 digests to avoid a simple
            // character-by-character secret comparison.
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
    }
}
