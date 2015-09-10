
# region Document Header
//Created By       : Anji 
//Created Date     : 
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.WO.Controllers
{
    #region Usings
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Data;
    using Newtonsoft.Json;
    using System.Web;
    #endregion

    public class WODIController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(WODIController));
        //
        // GET: /Masters/WorkOrderAndDisbursementItems/

        #region WorkOrder Actions

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 16 July 2014
        /// Modified By   : Shiva 
        /// Modified Date : 20 May 2014
        /// </summary>
        /// <returns> If session is In-valid then redirects to login from Create WO</returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult CreateWorkOrder()
        {
            return View();
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   : Shiva 
        /// Modified Date : 20 May 2014
        /// </summary>
        /// <returns> If session is In-valid then redirects to login from  WO</returns>
        /// 
        public ActionResult WorkOrder(string ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                //string context = ControllerContext.HttpContext.Request.Path;
                string context = "/WO/WODI/SearchWorkOrder";
                int UserID = Convert.ToInt32(Session["UserID"]);
                string Role = MenuBinding.CheckURlAndGetUserRole(context, UserID);
                if (Role == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                bool ActionStatus = DIActionsPermissions();
                ViewBag.ActionStatusOnDIItems = ActionStatus;

                ViewBag.Role = Role;
                ViewBag.WOID = ID;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   : Shiva 
        /// Modified Date : 20 May 2014
        /// </summary>
        /// <returns> If session is In-valid then redirects to login from Search WO</returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult SearchWorkOrder()
        {
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoAllotmentDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoInCorpDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WOEGMAcquisitionDisposal(string WoId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.WOID = WoId;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WOExistingClientEngaging(string WoId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.WOID = WoId;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 17th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WOTakeOver(string WoId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.WOID = WoId;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WOEGMChangeOfName(string WoId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.WOID = WoId;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoAGMDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 7 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoTransferDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }


        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 23 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoBonusIssue(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 7 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoAppointmentOfOfficerDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 7 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoCessationOfficerDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 7 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoInterimDividendDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 7 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoAppointmentOrCessationOfAuditorsDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 3 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _WoDuplicateDetails(string WoId)
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                ViewBag.WOID = WoId;
            }
            return View();
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 10 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        public ActionResult WoFee()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 3 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WODuplicateDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWODuplicateDetails(WODuplicateDetails WODuplicateDetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = WODuplicateDetails.InsertWODuplicateDetails(WODuplicateDetails, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 3 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WODuplicate SherHolder Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWODuplicateShareHolderDetails(int WOID, int personId, string sourcecode, string CertNo, string NoOfShares, string DateOfIssue, int NoOfNewCertToBeIssued)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = WODuplicateDetails.InsertWODuplicateShareHolderDetails(WOID, personId, sourcecode, CertNo, NoOfShares, NoOfNewCertToBeIssued, DateOfIssue, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 3 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WODuplicateDetails by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWODuplicateDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = WODuplicateDetails.GetWODuplicateDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 4 November 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WODuplicateShareHoldersDetails by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWODuplicateShareHoldersDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = WODuplicateDetails.GetWODuplicateShareHoldersDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 4 November 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : to Delete Duplicate Share Holder By ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWODuplicateShareholderDetailsByID(int DuplicateID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = WODuplicateDetails.DeleteWODuplicateShareholderDetailsByID(DuplicateID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 23 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get AuditorDetails By WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAuditorDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.Auditors.GetAuditorDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 23 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get AuditorDetails By WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetModeOfAppointmentDetails()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.ModeOfAppointment.GetModeOfAppointmentDetails();
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 23 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOAppointmentOrCessationOfAuditors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOAppointmentOrCessationDetails(WOAppointmentOrCessationOfAuditors AppointmentOrCessationOfAuditors)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = WOAppointmentOrCessationOfAuditors.InsertWOAppointmentOrCessationDetails(AppointmentOrCessationOfAuditors, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 23 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOAppointmentOrCessationOfAuditors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOAppointmentOrCessationDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = WOAppointmentOrCessationOfAuditors.GetWOAppointmentOrCessationDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 25 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOAGMDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOAGMDetails(AGMDetails AGMDetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = AGMDetails.InsertWOAGMDetails(AGMDetails, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOAGMDetails by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOAGMDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = AGMDetails.GetWOAGMDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer  
        /// Created Date : 12th Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WO ShareHoldersDetails  by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetShareholdersByWOID(int WOID, int ShareClassID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.ShareHoldingStructure.GetShareholdersByWOID(WOID, ShareClassID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer  
        /// Created Date : 12th Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Insert ShareHoldersDetails  by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertShareholdersByWOID(List<Masters.Models.Masters.ShareHolderDevidentDetails> ShareDividendFields, bool IsDivident, string DividendPerShare, string DividentShareCurrency, string TotalNetAmount, string TotalNoOfShares, int WOID, string WOTypeName, int ClassofShare)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = Masters.Models.Masters.ShareHoldingStructure.InsertShareholdersDetailsWOID(ShareDividendFields, IsDivident, DividendPerShare, DividentShareCurrency, TotalNetAmount, TotalNoOfShares, WOID, createdBy, WOTypeName, ClassofShare);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer  
        /// Created Date : 12th Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Delete ShareHoldersDetails  by WOID,PersonID,PersonSource
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteShareholdersByWOID(string WOShareHoldersDividentID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = Masters.Models.Masters.ShareHoldingStructure.DeleteShareholdersDetails(WOShareHoldersDividentID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 11 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get GetAll Auditors Status  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllAuditorsStatus()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.AuditorsStatus.GetAuditorsStatusDetails();
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 11 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get GetAll Share Holding Structure  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllShareHoldingStructures()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.ShareHoldingStructure.GetShareHoldingStructureDetails();
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 22 August 2014
        /// Modified By  : Shiva
        /// Modified Date: 5th Sep 2014
        /// Description  : To Insert WOTransferDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOTransferDetails(WOTransferDetails SaveTransferData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = new WOTransferDetails
                    {
                        WOID = SaveTransferData.WOID,
                        IssuedAndPaidUpCapitalClassOfShare = SaveTransferData.IssuedAndPaidUpCapitalClassOfShare,
                        IsPreEmptionRights = SaveTransferData.IsPreEmptionRights,
                        LettertoIRAS = SaveTransferData.LettertoIRAS,
                        LettertoIRASSource = SaveTransferData.LettertoIRASSource,
                        CreatedBy = checkSession
                    };

                    checkSession = data.InsertWOTransferDetails();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 5th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Insert WO TransferT ransaction Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOTransferTransactionDetails(WOTransferDetails SaveTransferTransactionData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = new WOTransferDetails
                    {
                        WOID = SaveTransferTransactionData.WOID,
                        Transferor = SaveTransferTransactionData.Transferor,
                        TransferorSource = SaveTransferTransactionData.TransferorSource,
                        Transferee = SaveTransferTransactionData.Transferee,
                        TransfereeSource = SaveTransferTransactionData.TransfereeSource,
                        SharesTransfered = SaveTransferTransactionData.SharesTransfered,
                        Consideration = SaveTransferTransactionData.Consideration,
                        IssuedAndPaidUpCapitalCurrency = SaveTransferTransactionData.IssuedAndPaidUpCapitalCurrency,
                        CreatedBy = checkSession
                    };

                    checkSession = data.InsertWOTransferTransactionDetails();
                }
                return Json(checkSession);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 5th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Delete WO Transfer Transaction details by ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOTransferTransactionDetailsByID(int TransferID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOTransferDetails.DeleteWOTransferTransactionDetailsByID(TransferID);
                }
                return Json(checkSession);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOAGMDetails by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOTransferDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = WOTransferDetails.GetWOTransferDetailsByWOID(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 5 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOTransferTransactionDetails by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOTransferTransactionDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var result = new WOTransferDetails.WOTransferDetailsInfo();
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = WOTransferDetails.GetWOTransferTransactionDetailsByWOID(WOID);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Insert the Type, Description, Status to Work order  </returns>
        /// </summary>
        [HttpPost]
        public JsonResult InsertWorkOrder(string Type, string Desc, bool Billable, int ClientOrCustomerID, string SourceID, string GroupCode, bool IsAdhoc, string StatusCode, bool IsPostedToCss1)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var data = new DisbursementItem
                    {
                        Type = Type,
                        Description = HttpUtility.UrlDecode(Desc),
                        CreatedBy = createdBy,
                        Billable = Billable,
                        ClientOrCustomerID = ClientOrCustomerID,
                        SourceID = SourceID,
                        GroupCode = GroupCode,
                        IsAdhoc = IsAdhoc,
                        StatusCode = StatusCode,
                        IsPostedToCss1 = IsPostedToCss1,
                    };
                    var output = data.InsertWorkOrder();
                    return Json(output);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 26 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> </returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetSerchWODataBind(string ClientId, string SourceID, string WorkOrderID, string statusCode, int startpage, int rowsperpage, string Type, string OrderBy, string FromDate, string ToDate, string IsAdhoc, string AssignedTo)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var dataresult = WO.Models.WorkOrders.GetSerchWOData(ClientId, SourceID, HttpUtility.UrlDecode(WorkOrderID), statusCode, startpage + 1, rowsperpage + startpage, Type, OrderBy, FromDate, ToDate, createdBy, IsAdhoc, AssignedTo);
                    return Json(dataresult);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To delete WorkOrder 
        /// Created By   : Pavan
        /// Created Date : 30 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWorkOrderById(int ID, string StatusCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    WorkOrders objWO = new WorkOrders();
                    int result = objWO.DeleteWorkOrderById(ID, StatusCode);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 27 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns>Get and Search WorkOrder Detalis from database. </returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetWorkOrderDetailsById(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var dataresult = WO.Models.WorkOrders.GetWorkOrderDetailsById(ID);
                    return Json(dataresult);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 27 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns>Get and Search WorkOrder Detalis from database. </returns>
        /// </summary>
        [HttpPost]
        public JsonResult ValidateWorkOrederById(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    var dataresult = WO.Models.WorkOrders.ValidateWorkOrederById(ID, UserID);
                    return Json(dataresult);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 27 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get the WorkOrderType table ID and Name for Type dropdown in Search work order view.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetWorkOrderType()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = WorkOrders.GetWorkOrderType();
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get Code and Name from MWOCategory from database for Category dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date :15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 300, VaryByCustom = "User")]
        public JsonResult GetMWOCategory()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var Category = WorkOrders.GetMWOCategory();
                return Json(Category);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get MWOType by Category Code.
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMWOTypeByCategoryCode(string CategoryCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var WoTypeByCategory = WorkOrders.GetMWOTypeByCategoryCode(CategoryCode);
                return Json(WoTypeByCategory);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : GetMWOStatus from MWOStatus table 
        /// Created By   : Shiva
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMWOStatus()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var MWOStatus = WOStatusAndAssignment.GetMWOStatus();
                return Json(MWOStatus);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        /// <summary>
        /// Description  : Get UserID, Name from CSS1 for AssignedTo dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCSS1UserDetailsForWOAssignment()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var WOAssignment = WOStatusAndAssignment.GetCSS1UserDetailsForWOAssignment();
                return Json(WOAssignment);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save the Group in WO and WOAssignment.
        /// Created By   : SHIVA
        /// Created Date : 10 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Saved status</returns>
        [HttpPost]
        public JsonResult SaveWOAssignmentGroup(string WOCode, int WOID, string AssignedGroup, bool Billable)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    string strWOCode = string.Empty;
                    strWOCode = WOStatusAndAssignment.SaveWOAssignmentGroup(WOID, WOCode, AssignedGroup, checkSession, Billable);
                    return Json(strWOCode);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save the Adhoc in WO .
        /// Created By   : Pavan
        /// Created Date : 30 Jan 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Saved status</returns>
        [HttpPost]
        public JsonResult SaveWOAdhoc(int WorkOrderID, bool Adhoc)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int Status = 0;
                    Status = WOStatusAndAssignment.SaveWOAdhoc(WorkOrderID, Adhoc, checkSession);
                    return Json(Status);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save AssignedTo WOAssignment and WO.
        /// Created By   : SHIVA
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Saved status</returns>
        [HttpPost]
        public JsonResult SaveWOAssignmentAssignedTo(int WorkOrderID, string AssignedTo, bool Billable)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    string strWOCode = string.Empty;

                    if (AssignedTo == "Self")
                        AssignedTo = Convert.ToString(Session["UserID"]);

                    strWOCode = WOStatusAndAssignment.SaveWOAssignmentAssignedTo(WorkOrderID, AssignedTo, checkSession, Billable);
                    return Json(strWOCode);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Insert or Update the WOStatus
        /// Created By   : SHIVA
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Inserted or Updated status</returns>
        [HttpPost]
        public JsonResult InsertWOStatus(string StatusCode, string WorkOrderID, string Comment)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOStatusAndAssignment.InsertWOStatus(StatusCode, WorkOrderID, HttpUtility.UrlDecode(Comment), checkSession);
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : Update Billing Party By WOID in WO table
        /// Created By   : Sudheer
        /// Created Date : 14 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Updated Clientcode status</returns>
        [HttpPost]
        public JsonResult UpdateBillingPartyByWOID(int WOID, int ClientOrCustomer, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOStatusAndAssignment.UpdateBillingPartyByWOID(WOID, ClientOrCustomer, SourceID);
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get WOStatus History by WOID
        /// Created By   : SHIVA
        /// Created Date : 25 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOStatus History by WOID</returns>
        [HttpPost]
        public JsonResult GetWOStatusHistoryByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var StatusHistory = WOStatusAndAssignment.GetWOStatusHistoryByWOID(WOID);
                    return Json(StatusHistory);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get WOAssignment History by WOID
        /// Created By   : SHIVA
        /// Created Date : 25 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOAssignment History by WOID</returns>
        [HttpPost]
        public JsonResult GetWOAssignmentHistoryByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var AssignmentHistory = WOStatusAndAssignment.GetWOAssignmentHistoryByWOID(WOID);
                    return Json(AssignmentHistory);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get WOAssignment History by WOID
        /// Created By   : SHIVA
        /// Created Date : 22 Dec 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WO Group History by WOID</returns>
        [HttpPost]
        public JsonResult GetWOGroupHistoryByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var AssignmentHistory = WOStatusAndAssignment.GetWOGroupHistoryByWOID(WOID);
                    return Json(AssignmentHistory);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }







        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOINCORPDetails(InCorpDetails incorpdetails)
        {
            int checkSession = UserLogin.AuthenticateRequest();
            if (checkSession == 0)
            {
                return Json(checkSession);
            }
            else
            {
                try
                {
                    incorpdetails.FinancialYearEnd = (incorpdetails.FinancialYearEnd != null ? HelperClasses.ConvertDateFormatDDMM(incorpdetails.FinancialYearEnd) : "");
                    incorpdetails.FirstFinancialYearEnd = (incorpdetails.FirstFinancialYearEnd != null ? Convert.ToDateTime(incorpdetails.FirstFinancialYearEnd).ToString("MM/dd/yyyy") : "");

                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = InCorpDetails.InsertWOInCorpDetails(incorpdetails, createdBy);
                    return Json(result);
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("The DateTime represented by the string is not supported in calendar System.Globalization.GregorianCalendar") > -1 ||
                        ex.Message.IndexOf("String was not recognized as a valid DateTime.") > -1)
                        return Json(-2);
                    else
                        return Json(-1);
                }
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOINCORPFirstSubscribers(string WOID, string personid, string sourcecode, string occupation, string NoOfSharesHeld, string TotalAmountPaid)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = InCorpDetails.InsertWOINCORPFirstSubscribers(WOID, personid, sourcecode, occupation, NoOfSharesHeld, TotalAmountPaid, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOINCORPAuthorizedPersonFS(string WOID, string personid, string sourcecode, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = InCorpDetails.InsertWOINCORPAuthorizedPersonFS(WOID, personid, sourcecode, FSID, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOINCORPAuthorizedPersonPrincipalDetails(string WOID, string personid, string sourcecode, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = InCorpDetails.InsertWOINCORPAuthorizedPersonPrincipalDetails(WOID, personid, sourcecode, FSID, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOINCORPPrincipalDetails(string WOID, string personid, string sourcecode, string NDPersonId, string NDSourceCode, string ContactPerson)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = InCorpDetails.InsertWOINCORPPrincipalDetails(WOID, personid, sourcecode, NDPersonId, NDSourceCode, ContactPerson, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 30 April 2015
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get NomineeDirectors In Principal Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetNomineeDirectorsInPrincipalDetails(int WOID, string InfoCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = Masters.Models.Masters.NomineeDirectors.GetNomineeDirectorsDetails(WOID, InfoCode);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert TakeOver Director
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertTakeOverDirectorDetails(string WOID, string DirectorName)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = WOTakeOver.InsertWOTakeOverDirectorDetails(WOID, DirectorName, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 29th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To get wo TakeOver shareHolder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOTakeOverShareholderDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DataSet result = WOTakeOver.GetWOTakeOverShareholderDetails(WOID);
                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert TakeOver Shareholder Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertTakeOverShareholderDetails(string WOID, string ShareholderName)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = WOTakeOver.InsertTakeOverShareholderDetails(WOID, ShareholderName, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOINCORPFirstSubscribers(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DataSet result = InCorpDetails.GetWOINCORPFirstSubscribers(WOID);
                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date :4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOINCORPAuthorisedFirstSubscribers(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DataSet result = InCorpDetails.GetWOINCORPAuthorisedFirstSubscribers(WOID);
                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date :4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOINCORPAuthorizedPersonPrincipalDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DataSet result = InCorpDetails.GetWOINCORPAuthorizedPersonPrincipalDetails(WOID);
                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date :4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To get wo incorp PrincipalDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOINCORPPrincipalDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DataSet result = InCorpDetails.GetWOINCORPPrincipalDetails(WOID);
                    string data = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date :4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetddlFirstSubscriber(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.FirstSubscriber.GetddlFirstSubscriber(Convert.ToInt32(WOID));
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date :4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetddlPrincipalDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Masters.Models.Masters.FirstSubscriber.GetddlPrincipalDetails(Convert.ToInt32(WOID));
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOINCORPFirstSubscribers(string WOID, string PERSONID, string PERSONSOURCE, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = 0;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(PERSONID) && !string.IsNullOrEmpty(PERSONSOURCE))
                        result = InCorpDetails.DeleteWOINCORPFirstSubscribers(WOID, PERSONID, PERSONSOURCE, FSID);

                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOINCORPAuthorizedPersonFSDetails(string WOID, string PERSONID, string PERSONSOURCE, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = 0;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(PERSONID) && !string.IsNullOrEmpty(PERSONSOURCE))
                        result = InCorpDetails.DeleteWOINCORPAuthorizedPersonFSDetails(WOID, PERSONID, PERSONSOURCE, FSID);

                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 5th Sep
        /// Modified By  :
        /// Modified Date:
        /// Description  : To delete incorp principal details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOINCORPPrincipalDetails(string WOID, string PERSONID, string PERSONSOURCE, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = 0;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(PERSONID) && !string.IsNullOrEmpty(PERSONSOURCE))
                        result = InCorpDetails.DeleteWOINCORPPrincipalDetails(WOID, PERSONID, PERSONSOURCE, FSID);

                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 5th Sep
        /// Modified By  :
        /// Modified Date:
        /// Description  : To delete incorp principal details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOINCORPAuthorizedPersonPrincipalDetails(string WOID, string PERSONID, string PERSONSOURCE, string FSID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = 0;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(PERSONID) && !string.IsNullOrEmpty(PERSONSOURCE))
                        result = InCorpDetails.DeleteWOINCORPAuthorizedPersonPrincipalDetails(WOID, PERSONID, PERSONSOURCE, FSID);

                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Insert WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertWOAllotmentDetails(AllotmentDetails allotmentDetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = AllotmentDetails.InsertWOAllotmentDetails(allotmentDetails, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Bind WOIncorpDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindWOIncorpDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var result = InCorpDetails.GetWOIncorpDetails(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 1 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Bind Alloatment details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindWOAllotmentDetails(string WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var result = AllotmentDetails.GetWOAllotmentDetails(WOID);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva  
        /// Created Date : 22 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Save All WOInterimDividend Details.
        /// </summary>
        /// <returns>WOInterimDividend Saved status.</returns>
        [HttpPost]
        public JsonResult SaveWoInterimDividendDetails(int WOID, string FinancialPeriod, string DateOfDeclaration, string DateOfPayment, int DividendDirector, string DividendSource)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    var objInterimDividend = new _InterimDividend()
                    {
                        WOID = WOID,
                        FinancialPeriod = FinancialPeriod,
                        //ClassOfShare = ClassOfShare,
                        DateOfDeclaration = DateOfDeclaration,
                        DateOfPayment = DateOfPayment,
                        DividendDirector = DividendDirector,
                        DividendSource = DividendSource,
                        SavedBy = createdBy,
                    };

                    checkSession = objInterimDividend.SaveWoInterimDividendDetails();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva  
        /// Created Date : 22 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To get WOInterimDividend Details by WOID.
        /// </summary>
        /// <returns>WOInterimDividend Saved status.</returns>
        [HttpPost]
        public JsonResult GetWOInerimDividendDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var InerimDividendData = _InterimDividend.GetWOInerimDividendDetailsByWOID(WOID);
                    return Json(InerimDividendData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        /// <summary>
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To get the Company Details by WOID
        /// </summary>
        /// <returns>Company Addresses</returns>
        [HttpPost]
        public JsonResult GetCompanyAddressesByWOID(int WOID, bool IsFMGAddress)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var CompanyDetails = WOEGMAcquisitionDisposal.GetCompanyDetailsByWOID(WOID, IsFMGAddress);
                    return Json(CompanyDetails);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To insert 
        /// </summary>
        /// <returns>Company Addresses</returns>
        [HttpPost]
        public JsonResult SaveWOEGMDetails(WOEGMAcquisitionDisposal EGMData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var objEGMData = new WOEGMAcquisitionDisposal()
                    {
                        MeetingNotice = EGMData.MeetingNotice,
                        MeetingNoticeSource = EGMData.MeetingNoticeSource,
                        MeetingMinutes = EGMData.MeetingMinutes,
                        MeetingMinutesSource = EGMData.MeetingMinutesSource,
                        OthersMeetingMinutes = EGMData.OthersMeetingMinutes,
                        Designation = EGMData.Designation,
                        ShareHoldingStructure = EGMData.ShareHoldingStructure,
                        ConsiderationCurrency = EGMData.ConsiderationCurrency,
                        ConsiderationAmount = EGMData.ConsiderationAmount,
                        NameVendor = EGMData.NameVendor,
                        IsROPlaceOfMeeting = EGMData.IsROPlaceOfMeeting,
                        MeetingAddressLine1 = EGMData.MeetingAddressLine1,
                        MeetingAddressLine2 = EGMData.MeetingAddressLine2,
                        MeetingAddressLine3 = EGMData.MeetingAddressLine3,
                        MeetingAddressCountry = EGMData.MeetingAddressCountry,
                        MeetingAddressPostalCode = EGMData.MeetingAddressPostalCode,
                        PropertyAddressLine1 = EGMData.PropertyAddressLine1,
                        PropertyAddressLine2 = EGMData.PropertyAddressLine2,
                        PropertyAddressLine3 = EGMData.PropertyAddressLine3,
                        PropertyAddressCountry = EGMData.PropertyAddressCountry,
                        PropertyAddressPostalCode = EGMData.PropertyAddressPostalCode,
                        SavedBy = checkSession,
                        WOID = EGMData.WOID,
                        TypeOfTransaction = EGMData.TypeOfTransaction
                    };
                    checkSession = objEGMData.SaveWOEGMDetails();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Get EGM Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Details.</returns>
        [HttpPost]
        public JsonResult GetEGMDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var CompanyDetails = WOEGMAcquisitionDisposal.GetEGMDetailsByWOID(WOID);
                    return Json(CompanyDetails);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save ECE Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>ECE Status.</returns>
        [HttpPost]
        public JsonResult SaveWOECEDetailsByWOID(WOExistingClientEngaging ECEData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var CompanyDetails = new WOExistingClientEngaging()
                    {
                        WOID = ECEData.WOID,
                        Currency = ECEData.Currency,
                        ClassOfShare = ECEData.ClassOfShare,
                        NewAllottedShares = ECEData.NewAllottedShares,
                        EachShare = ECEData.EachShare,
                        AmountPaidToEachShare = ECEData.AmountPaidToEachShare,
                        TotalConsideration = ECEData.TotalConsideration,
                        NoOfIssuedShares = ECEData.NoOfIssuedShares,
                        IssuedCapital = ECEData.IssuedCapital,
                        ResultantPaidupCapital = ECEData.ResultantPaidupCapital,
                        MeetingNoticeSource = ECEData.MeetingNoticeSource,
                        MeetingNotice = ECEData.MeetingNotice,
                        MeetingMinutesSource = ECEData.MeetingMinutesSource,
                        MeetingMinutes = ECEData.MeetingMinutes,
                        OthersMeetingMinutes = ECEData.OthersMeetingMinutes,
                        Designation = ECEData.Designation,
                        NoticeResolutionSource = ECEData.NoticeResolutionSource,
                        NoticeResolution = ECEData.NoticeResolution,
                        F24F25Source = ECEData.F24F25Source,
                        F24F25ID = ECEData.F24F25ID,
                        ShareHoldingStructure = ECEData.ShareHoldingStructure,

                        IsROPlaceOfMeeting = ECEData.IsROPlaceOfMeeting,
                        MAddressLine1 = ECEData.MAddressLine1,
                        MAddressLine2 = ECEData.MAddressLine2,
                        MAddressLine3 = ECEData.MAddressLine3,
                        MAddressCountry = ECEData.MAddressCountry,
                        MAddressPostalCode = ECEData.MAddressPostalCode,

                        SavedBy = checkSession
                    };
                    checkSession = CompanyDetails.SaveWOECEDetails();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save TakeOver Details by WOID.
        /// Created By   : Sudheer  
        /// Created Date : 20th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>TakeOver Status.</returns>
        [HttpPost]
        public JsonResult SaveWOTakeOverDetailsByWOID(WOTakeOver TakeOverData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var CompanyDetails = new WOTakeOver()
                    {
                        WOID = TakeOverData.WOID,
                        CompanyName = TakeOverData.CompanyName,
                        RegistrationNo = TakeOverData.RegistrationNo,
                        DateAccBeLaid = TakeOverData.DateAccBeLaid,
                        //  AllotmentShares = TakeOverData.AllotmentShares,
                        Currency = TakeOverData.Currency,
                        ClassOfShare = TakeOverData.ClassOfShare,
                        NewAllottedShares = TakeOverData.NewAllottedShares,
                        EachShare = TakeOverData.EachShare,
                        AmountPaidToEachShare = TakeOverData.AmountPaidToEachShare,
                        TotalConsideration = TakeOverData.TotalConsideration,
                        NoOfIssuedShares = TakeOverData.NoOfIssuedShares,
                        IssuedCapital = TakeOverData.IssuedCapital,
                        ResultantPaidupCapital = TakeOverData.ResultantPaidupCapital,
                        IsFMRegisteredAddress = TakeOverData.IsFMRegisteredAddress,
                        AddressLine1 = TakeOverData.AddressLine1,
                        AddressLine2 = TakeOverData.AddressLine2,
                        AddressLine3 = TakeOverData.AddressLine3,
                        AddressCountry = TakeOverData.AddressCountry,
                        AddressPostalCode = TakeOverData.AddressPostalCode,
                        Auditor = TakeOverData.Auditor,
                        OutGoingAuditor = TakeOverData.OutGoingAuditor,
                        MeetingNotice = TakeOverData.MeetingNotice,
                        MeetingMinutes = TakeOverData.MeetingMinutes,
                        Designation = TakeOverData.Designation,
                        NoticeResolution = TakeOverData.NoticeResolution,
                        F24F25ID = TakeOverData.F24F25ID,
                        ShareHoldingStructure = TakeOverData.ShareHoldingStructure,
                        SavedBy = checkSession
                    };
                    checkSession = CompanyDetails.SaveWOTakeOverDetails();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Get ECE Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>ECE Details.</returns>
        [HttpPost]
        public JsonResult GetWOECEDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var WOECEData = WOExistingClientEngaging.GetWOECEDetailsByWOID(WOID);
                    return Json(WOECEData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Get TakeOver Details by WOID.
        /// Created By   : Sudheer  
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>TakeOver Details.</returns>
        [HttpPost]
        public JsonResult GetWOTakeOverDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var WOECEData = WOTakeOver.GetWOTakeOverDetailsByWOID(WOID);
                    return Json(WOECEData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save EGM Change Of Name Details.
        /// Created By   : Shiva  
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Change Of Name Status.</returns>
        [HttpPost]
        public JsonResult SaveWOEGMChangeOfNameDetailsByWOID(WOEGMChangeOfName EGMChangeOfName)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var EGMChangeOfNameDetails = new WOEGMChangeOfName()
                    {
                        WOID = EGMChangeOfName.WOID,
                        NewName = EGMChangeOfName.NewName,
                        IsROPlaceOfMeeting = EGMChangeOfName.IsROPlaceOfMeeting,
                        MAddressLine1 = EGMChangeOfName.MAddressLine1,
                        MAddressLine2 = EGMChangeOfName.MAddressLine2,
                        MAddressLine3 = EGMChangeOfName.MAddressLine3,
                        MAddressCountry = EGMChangeOfName.MAddressCountry,
                        MAddressPostalCode = EGMChangeOfName.MAddressPostalCode,
                        MeetingNoticeSource = EGMChangeOfName.MeetingNoticeSource,
                        MeetingNotice = EGMChangeOfName.MeetingNotice,
                        MeetingMinutesSource = EGMChangeOfName.MeetingMinutesSource,
                        MeetingMinutes = EGMChangeOfName.MeetingMinutes,
                        OthersMeetingMinutes = EGMChangeOfName.OthersMeetingMinutes,
                        Designation = EGMChangeOfName.Designation,
                        NoticeResolutionSource = EGMChangeOfName.NoticeResolutionSource,
                        NoticeResolution = EGMChangeOfName.NoticeResolution,
                        ShareHoldingStructure = EGMChangeOfName.ShareHoldingStructure,
                        SavedBy = checkSession
                    };

                    checkSession = EGMChangeOfNameDetails.SaveWOEGMChangeOfNameDetailsByWOID();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Get EGM Change Of Name Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Change Of Name Details.</returns>
        [HttpPost]
        public JsonResult GetWOEGMChangeOfNameDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var WOEGMChangeOfNameData = WOEGMChangeOfName.GetWOEGMChangeOfNameDetailsByWOID(WOID);
                    return Json(WOEGMChangeOfNameData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save Wo bonus Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 24th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WO bonus status.</returns>
        [HttpPost]
        public JsonResult SaveWOBonusIssueDetailsByWOID(WOBonusIssue WOBonusData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var objWoBonusDetails = new WOBonusIssue()
                    {
                        WOID = WOBonusData.WOID,
                        BonusIssuePerShare = WOBonusData.BonusIssuePerShare,
                        RegisterOfMembersOn = WOBonusData.RegisterOfMembersOn,
                        AmountPaidOnEachShare = WOBonusData.AmountPaidOnEachShare,
                        TotalNoOfIssuedShares = WOBonusData.TotalNoOfIssuedShares,
                        ResultantIssuedCapital = WOBonusData.ResultantIssuedCapital,
                        //ClassOfShare = WOBonusData.ClassOfShare,
                        ResultantPaidUpCapital = WOBonusData.ResultantPaidUpCapital,
                        IsRegisteredAddressAsPlaceOfMeeting = WOBonusData.IsRegisteredAddressAsPlaceOfMeeting,
                        MeetingAddressLine1 = WOBonusData.MeetingAddressLine1,
                        MeetingAddressLine2 = WOBonusData.MeetingAddressLine2,
                        MeetingAddressLine3 = WOBonusData.MeetingAddressLine3,
                        MeetingAddressCountry = WOBonusData.MeetingAddressCountry,
                        MeetingAddressPostalCode = WOBonusData.MeetingAddressPostalCode,
                        MeetingNotice = WOBonusData.MeetingNotice,
                        MeetingNoticeSource = WOBonusData.MeetingNoticeSource,
                        MeetingMinutes = WOBonusData.MeetingMinutes,
                        MeetingMinutesSource = WOBonusData.MeetingMinutesSource,
                        OthersMeetingMinutes = WOBonusData.OthersMeetingMinutes,
                        Designation = WOBonusData.Designation,
                        NoticeOfResolution = WOBonusData.NoticeOfResolution,
                        NoticeOfResolutionSource = WOBonusData.NoticeOfResolutionSource,
                        LetterOfAllotment = WOBonusData.LetterOfAllotment,
                        LetterOfAllotmentSource = WOBonusData.LetterOfAllotmentSource,
                        ReturnOfAllotment = WOBonusData.ReturnOfAllotment,
                        ReturnOfAllotmentSource = WOBonusData.ReturnOfAllotmentSource,
                        ShareHoldingStructure = WOBonusData.ShareHoldingStructure,
                        SavedBy = checkSession
                    };
                    checkSession = objWoBonusDetails.SaveWOBonusIssueDetailsByWOID();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Get EGM Change Of Name Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Change Of Name Details.</returns>
        [HttpPost]
        public JsonResult GetWOBonusIssueDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var BonusData = WOBonusIssue.GetWOBonusIssueDetailsByWOID(WOID);
                    return Json(BonusData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Shiva  
        /// Created Date : 25th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Insert ShareHoldersDetails and Bonus Issue data by WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertFromWoBonusIssueAndShareholdersDetailsWOID(List<Masters.Models.Masters.ShareHolderDevidentDetails> ShareDividendFields, string ConsiderationOfEachShare, string TotalNoOfNewSharesToBeAllotted, string Currency, string TotalNetAmount, int WOID, int ClassofShare)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = Masters.Models.Masters.ShareHoldingStructure.InsertFromWoBonusIssueAndShareholdersDetailsWOID(ShareDividendFields, ConsiderationOfEachShare, TotalNoOfNewSharesToBeAllotted, Currency, TotalNetAmount, WOID, checkSession, ClassofShare);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan  
        /// Created Date : 15th May 2015
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Manditory Fields For Incorp
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetValidationForIncorp(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = WOIncorpValidation.GetValidationForIncorp(WOID);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion


        #region Partial DisbursementItems Actions

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> load partial view.</returns>
        /// </summary>
        public ActionResult _DisbursementItems()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            bool ActionStatus = DIActionsPermissions();
            ViewBag.ActionStatusOnDIItems = ActionStatus;
            log.Debug("End: " + methodBase.Name);
            return View();
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 18 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> load List partial view.</returns>
        /// </summary>
        public ActionResult _DisbursementList()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return View();
        }




        public bool DIActionsPermissions()
        {
            bool IsAction = false;
            if (Convert.ToBoolean(Session["IsSuperAdmin"]) == true || Convert.ToBoolean(Session["isadmin"]) == true)
            {
                IsAction = true;
            }
            return IsAction;
        }







        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get the DIType table ID and Code for Type dropdown in partial view.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetMDIType()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = DisbursementItem.GetMDIType(string.Empty);
                return Json(data);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> To get the MDIType Details By ItemNumber for the partial view.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetMDITypeDetailsByItemNumber(int DIID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = DisbursementItem.GetMDITypeDetailsByItemNumber(DIID);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Insert and update the Disbursement Items data </returns>
        /// </summary>
        [HttpPost]
        public JsonResult InsertDisbursementItems(int ID, int ItemNumber, int Quantity, int WOID, decimal Amount, bool IsAdhocBilling, bool IsArchived, string VenderRefID, string Description, decimal UnitPrice, string DateInoccured, bool NeedVerification, string InHouseComment)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int UpdatedBy = UserLogin.AuthenticateRequest();
                if (UpdatedBy == 0)
                {
                    return Json(UpdatedBy);
                }
                else
                {
                    int CreatedBy = Convert.ToInt32(Session["UserID"]);
                    int result = 0;
                    result = DisbursementItem.InsertAndUpdateDisbursementItemsData(ID, ItemNumber, Quantity, WOID, Amount, IsAdhocBilling, IsArchived, HttpUtility.UrlDecode(VenderRefID), HttpUtility.UrlDecode(Description), UnitPrice, DateInoccured, CreatedBy, NeedVerification, HttpUtility.UrlDecode(InHouseComment));
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get the current WorkOrderID Disbursement Items data </returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetDisbursementItemsData(int WID, int startpage, int rowsperpage)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var disbursementItemsData = DisbursementItem.GetDisbursementItemsData(WID, startpage + 1, rowsperpage + startpage);
                    return Json(disbursementItemsData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region Partial DisbursementItems List Actions

        /// <summary>
        /// Created By    : pavan
        /// Created Date  : 20 May 2014
        /// Modified By   :  
        /// Modified Date : 
        /// </summary>
        /// <returns>Default action of Search Disbursement Items</returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult SearchDisbursementItems()
        {
            bool ActionStatus = DIActionsPermissions();
            ViewBag.ActionStatusOnDIItems = ActionStatus;
            return View();
        }


        /// <summary>
        /// Description  : Get Disbursement details by ID  
        /// Created By   : Shiva
        /// Created Date : 6 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDisbursementItemsByID(int ID, string VenderRefID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = DisbursementItem.GetDisbursementItemsByID(ID, VenderRefID);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Update IsArchived To Active or In-Active For Checked Id's Of Disburesement Items 
        /// Created By   : Shiva
        /// Created Date : 16 June 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ArchivedActionOnDisbursementItems(string DisbursementIds, string InHouseComment, int Archived, string ForState)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DisbursementItem objDisbursementItem = new DisbursementItem();
                    int result = objDisbursementItem.ArchivedActionOnDisbursementItems(DisbursementIds, HttpUtility.UrlDecode(InHouseComment), Archived, ForState, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Update IsAdhoc To Active or In-Active For Checked Id's Of Disburesement Items 
        /// Created By   : Shiva
        /// Created Date : 16 June 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AdhocActionOnDisbursementItems(string DisbursementIds, int Adhoc, string ForState)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    DisbursementItem objDisbursementItem = new DisbursementItem();
                    int result = objDisbursementItem.AdhocActionOnDisbursementItems(DisbursementIds, Adhoc, ForState, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Update AccpacStatus of Disbursement details  
        /// Created By   : Pavan
        /// Created Date : 13 March 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateAccpacStatusByDIID(int DIID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = DisbursementItem.UpdateAccpacStatusByDIID(DIID, checkSession);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Delete Disbursement by ID  
        /// Created By   : HUSSAIN
        /// Created Date : 11 June 2014
        /// Modified By  : SHIVA
        /// Modified Date: 16 June 2014
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public JsonResult DeleteDisbursementItemsByID(string DIDs)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int returnDeletedStatus = DisbursementItem.DeleteDisbursementItemsByID(DIDs);
                    return Json(returnDeletedStatus);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get and Search Disbursement Items from database.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetSearchDisbursementItemsData(string clientID, string SourceID, string WO, string venderRefId, string Type, string IsVerified, string IsBilled, string IsArchived, string IsAdhoc, string OrderBy, int startpage, int rowsperpage, string FromDate, string ToDate, int ACCPACStatus)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var SearchDisbursementItems = DisbursementItem.GetAllSearchDisbursementItems(clientID, SourceID, HttpUtility.UrlDecode(WO), HttpUtility.UrlDecode(venderRefId), Type, IsVerified, IsBilled, IsArchived, IsAdhoc, OrderBy, startpage + 1, rowsperpage + startpage, FromDate, ToDate, ACCPACStatus);
                    return Json(SearchDisbursementItems);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : Get DisbursementItems Invoice preview by ID  
        /// Created By   : SHIVA
        /// Created Date : 30 Oct 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="ID">Get DisbursementItems Invoice preview details based on DID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoicePreviewDataByDI(string DIDs)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var InvoicePreview = DisbursementItem.GetInvoicePreviewDataByDI(DIDs);
                    return Json(InvoicePreview);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }




        /// <summary>
        /// Description  : Get FeeItems Invoice preview by ID's  
        /// Created By   : SHIVA
        /// Created Date : 4 Feb 2015
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="ID">Get FeeItems Invoice preview details based on FeeID's</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoicePreviewDataByFee(string FeeIDs)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var InvoicePreview = DisbursementItem.GetInvoicePreviewDataByFeeID(FeeIDs);
                    return Json(InvoicePreview);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion

        #region New DI
        [CheckUrlAccessCustomFilter]
        public ActionResult CreateDI(string VRFID, string Amount)
        {
            ViewBag.VRFID = VRFID;
            ViewBag.Amount = Amount;
            bool ActionStatus = DIActionsPermissions();
            ViewBag.ActionStatusOnDIItems = ActionStatus;
            return View();
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 12 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Insert the Disbursement Items data for Client (with out Work Order)</returns>
        /// </summary>
        [HttpPost]
        public JsonResult InsertDIForClient(int ItemNumber, int Quantity, decimal Amount, bool IsAdhocBilling, bool IsArchived, string VenderRefID, string Description, decimal UnitPrice, string DateIncurred, bool NeedVerification, int CompanyID, string CompanySource, string InHouseComment)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int UpdatedBy = UserLogin.AuthenticateRequest();
                if (UpdatedBy == 0)
                {
                    return Json(UpdatedBy);
                }
                else
                {
                    int CreatedBy = Convert.ToInt32(Session["UserID"]);
                    int result = 0;
                    result = DisbursementItem.InsertDIForClient(ItemNumber, Quantity, Amount, IsAdhocBilling, IsArchived, VenderRefID, HttpUtility.UrlDecode(Description), UnitPrice, DateIncurred, NeedVerification, CompanyID, CompanySource, CreatedBy, HttpUtility.UrlDecode(InHouseComment));
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        #endregion

        #region Fee Details
        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 12 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get the FeeType data from MFeeType table.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetMFeeType()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = WOFee.GetMFeeType();
                return Json(data);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 12 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> To get the MFeeType Details By ItemNumber.</returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetMFeeTypeDetailsByItemNumber(int FeeID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var data = WOFee.GetMDITypeDetailsByItemNumber(FeeID);
                    return Json(data);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Insert or Update Fee Item.
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee saved Status</returns>
        [HttpPost]
        public JsonResult InsertOrUpdateFeeItem(WOFee FeeDetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    //var objFee = new WOFee()
                    //{
                    //    ID = FeeDetails.ID,
                    //    WOID = FeeDetails.WOID,
                    //    FeeType = FeeDetails.FeeType,
                    //    Units = FeeDetails.Units,
                    //    Amount = FeeDetails.Amount,
                    //    Description = FeeDetails.Description,
                    //    IsAdhoc = FeeDetails.IsAdhoc,
                    //    IsArchived = FeeDetails.IsArchived,
                    //    UnitPrice = FeeDetails.UnitPrice,
                    //    FeeInhouseComment = FeeDetails.FeeInhouseComment,
                    //    SavedBy = checkSession
                    //};
                    //checkSession = objFee.InsertOrUpdateFeeItem();
                    checkSession = FeeDetails.InsertOrUpdateFeeItem();
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : To get Fee Items by WOID.
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Data</returns>
        [HttpPost]
        public JsonResult GetFeeItemsByWOID(int WOID, int startPage, int resultPerPage)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var FeeData = WOFee.GetFeeItemsByWOID(WOID, startPage + 1, resultPerPage + startPage);
                    return Json(FeeData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get Fee Item by ID.
        /// Created By   : Shiva
        /// Created Date : 15 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Data</returns>
        [HttpPost]
        public JsonResult GetFeeItemByFeeID(int FeeID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var FeeData = WOFee.GetFeeItemByFeeID(FeeID);
                    return Json(FeeData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get Fee Item by ID.
        /// Created By   : Shiva
        /// Created Date : 15 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Data</returns>
        [HttpPost]
        public JsonResult UpdateFeeAccpacStatusByFeeID(int FeeID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int FeeData = WOFee.UpdateFeeAccpacStatusByFeeID(FeeID, checkSession);
                    return Json(FeeData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Delete Fee Item by ID.
        /// Created By   : Shiva
        /// Created Date : 15 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Delete status</returns>
        [HttpPost]
        public JsonResult DeleteFeeItemByFeeID(string FeeIDs)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOFee.DeleteFeeItemByFeeID(FeeIDs);
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : Update Archived To Active or In-Active For Checked Id's Of Fee Items 
        /// Created By   : Shiva
        /// Created Date : 16 Sep 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="FeeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ArchivedActionOnFeeItems(string FeeIds, int Archived, string ForState, string Comment)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOFee.ArchivedActionOnFeeItems(FeeIds, Archived, ForState, checkSession, HttpUtility.UrlDecode(Comment));
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Update IsAdhoc To Active or In-Active For Checked Id's Of Fee Items 
        /// Created By   : Shiva
        /// Created Date : 16 Sep 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="FeeIds"></param>
        /// <returns>Updated status</returns>
        [HttpPost]
        public JsonResult AdhocActionOnFeeItems(string FeeIds, int Adhoc, string ForState)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    checkSession = WOFee.AdhocActionOnFeeItems(FeeIds, Adhoc, ForState, checkSession);
                    return Json(checkSession);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region Cessation Details
        [HttpPost]
        public JsonResult GetCessationDirectors(int WOID, int DirectorID, string DirectorSource, int NatureAppoint)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var result = Cessionofficers.GetCessationDirectors(WOID, DirectorID, DirectorSource, NatureAppoint);
                    return Json(result.DirectorsList);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Insert Cessation Directors.
        /// Created By   : Sudheer
        /// Created Date : 20 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        public JsonResult InsertCessationDirectors(Cessionofficers cessationdetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = Cessionofficers.InsertCessationDirectors(cessationdetails, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }

        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WO Cessation Officer Details By WOID 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOCessationOfficerDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var result = new CessionofficersInfo();
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Cessionofficers.GetWOCessationDetailsByWOID(WOID);
                }
                return Json(result.DirectorsList);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Appointment Officer Details By WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOCessionofficersPossionDetailsByCessId(int CessID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var result = new CessionofficersInfo();
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Cessionofficers.GetWOCessionofficersPossionDetails(CessID);
                }
                return Json(result.DirectorsList);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Delete Cessation Officer Details By CessionID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOCessationOfficerDetailsByWOID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int result = -1;
            try
            {

                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Cessionofficers.DeleteCessationOfficerDetails(ID);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(result);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        #endregion


        #region Appointment officer Details

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Appointment Officer Details By WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOAppointmentOfficerDetailsByWOID(int WOID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var result = new AppoinmentofficersInfo();
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Appoinmentofficers.GetWOApptOfficersDetailsByWOID(WOID);
                }
                return Json(result.DirectorsList);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Appointment Officer Details By WOID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWOAppointmentPossionDetailsByApptId(int ApptId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var result = new AppoinmentofficersInfo();
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Appoinmentofficers.GetWOApptOfficersPossionDetails(ApptId);
                }
                return Json(result.DirectorsList);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOTransferTransactionDetails by Appt officer by IDs
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteWOApptOfficerDetailsByWOID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int result = -1;
            try
            {

                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    result = Appoinmentofficers.DeleteAppointmentOfficerDetails(ID);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(result);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get Appoinment officers Details from database.
        /// Created By   : Sudheer
        /// Created Date : 20 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        public JsonResult InsertApptOfcrDetails(Appoinmentofficers ApptOfcrdetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int createdBy = Convert.ToInt32(Session["UserID"]);
                    int result = Appoinmentofficers.InsertAppoinmentDirectors(ApptOfcrdetails, createdBy);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }

        }


        #endregion

        #region SearchFEE

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 23 April 2015
        /// Modified By   : 
        /// Modified Date : 
        /// </summary>
        /// <returns> If session is In-valid then redirects to login</returns>
        //[CheckUrlAccessCustomFilter]
        public ActionResult SearchFeeItems()
        {
            bool ActionStatus = DIActionsPermissions();
            ViewBag.ActionStatusOnDIItems = ActionStatus;
            return View();
        }

        /// <summary>
        /// Description  : To get All Fee Items
        /// Created By   : Pavan
        /// Created Date : 23 Apr 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Data</returns>
        [HttpPost]
        public JsonResult GetAllFeeItems(string clientID, string SourceID, string WO, string Type, string IsBilled, string IsArchived, string IsAdhoc, string OrderBy, int startPage, int resultPerPage, string FromDate, string ToDate, int ACCPACStatus)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    var FeeData = WOFee.GetAllFeeItems(startPage + 1, resultPerPage + startPage, clientID, SourceID, HttpUtility.UrlDecode(WO), Type, IsBilled, IsArchived, IsAdhoc, OrderBy, FromDate, ToDate, ACCPACStatus);
                    return Json(FeeData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json("");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

    }
}