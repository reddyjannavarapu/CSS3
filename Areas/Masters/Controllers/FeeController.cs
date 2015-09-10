
# region Document Header
//Created By       : Pavan Kumar 
//Created Date     : 30 June 2014
//Description      : To Manage Fee
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.Masters.Controllers
{

    #region Usings
    using CSS2.Areas.Masters.Models;
    using CSS2.Models;
    using log4net;
    using System;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    public class FeeController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(FeeController));

        /// <summary>
        /// Created By   : pavan
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult Fee()
        {
            return View();
        }

        /// <summary>
        /// Description  : To Get All Fee Details
        /// Created By   : Pavan Kumar
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the Fee details available in database</returns>
        [HttpPost]
        public JsonResult GetFeeData(string searchText, int status, int startPage, int rowsPerPage, string OrderBy)
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
                    var Fees = Models.Fee.GetAllFees(startPage + 1, rowsPerPage + startPage, HttpUtility.UrlDecode(searchText), OrderBy, status);
                    return Json(Fees);
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
        /// Description  : To Create Fee Detail.
        /// Created By   : Pavan Kumar
        /// Created Date : 4 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns><returns>
        [HttpPost]
        public JsonResult CreateFee(Fee Fee)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                int createdBy = Convert.ToInt32(Session["UserID"]);
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    //var data = new Fee
                    //{
                    //    ID = FeeId,
                    //    Code = code,
                    //    Name = name,
                    //    ItemNumber = ItemNumber,
                    //    ACCPACCode = ACCPACCode,
                    //    Description = HttpUtility.UrlDecode(desc),
                    //    NeedSecurityDeposit = NeedSecurityDeposit,
                    //    Status = status,
                    //    CreatedBy = createdBy,
                    //    FeeType = FeeType
                    //};
                    Fee.CreatedBy = createdBy;
                    int result = Fee.InsertFee();
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
        /// Description  : To Delete Fee Detail.
        /// Created By   : Pavan Kumar
        /// Created Date : 4 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns><returns>
        [HttpPost]
        public JsonResult DeleteFee(int FeeId)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                int createdBy = Convert.ToInt32(Session["UserID"]);
                if (checkSession == 0)
                {
                    return Json(checkSession);
                }
                else
                {
                    int result = Models.Fee.DeleteFeeById(FeeId);
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



    }
}