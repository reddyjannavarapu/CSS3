
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

namespace CSS2.Areas.UserManagement.Controllers
{
    #region Usings
    using CSS2.Areas.UserManagement.Models;
    using CSS2.Models;
    using log4net;
    using System;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    public class UserDetailsController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(UserDetailsController));

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 14 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> If session is In-valid then redirects to login</returns>
        [CheckUrlAccessCustomFilter]
        [HttpGet]
        public ActionResult UserGroupDetails()
        {
            //if (UserLogin.ValidateUserRequest())
            //{
            //    return RedirectToAction("Login", "Home", new { area = "" });
            //}

            //string context = ControllerContext.HttpContext.Request.Path;
            //int UserID = Convert.ToInt32(Session["UserID"]);
            //string Role = MenuBinding.CheckURlAndGetUserRole(context, UserID);
            //if (Role == null)
            //{
            //    return RedirectToAction("Index", "Home", new { area = "" });
            //}
            //ViewBag.Role = Role;

            return View();
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 13 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get the UserInfo  from Database   
        /// </summary>
        /// <param name="startPage">Start index page</param>
        /// <param name="resultPerPage">Number of records per page</param>
        /// <param name="Search">Search keyword</param>
        /// <param name="OrderBy">Order by column</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserGroupDetails(int startPage, int resultPerPage, string Search, string Role, int Status, string OrderBy)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var users = UserDetails.GetAllUserDetails(startPage + 1, startPage + resultPerPage, HttpUtility.UrlDecode(Search), Role, Status, OrderBy);
                return Json(users);
                //, JsonRequestBehavior.DenyGet
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
        /// Created Date  : 13 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get the UserInfo  from Database   
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsManager"></param>
        /// <param name="IsAdmin"></param>
        /// <param name="IsSuperAdmin"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertANDUpdateUserDetails(int UserID, bool IsManager, bool IsAdmin, bool IsSuperAdmin, bool IsActive)
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
                    UpdatedBy = UserDetails.InsertANDUpdateUserDetails(UserID, IsManager, IsAdmin, IsSuperAdmin, IsActive,UpdatedBy);
                    return Json(UpdatedBy);
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