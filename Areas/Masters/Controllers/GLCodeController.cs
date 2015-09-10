
# region Document Header
//Created By       : Anji 
//Created Date     : 05 May 2014
//Description      : To Manage GLCodes
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
    using System.Web.Mvc;
    #endregion

    public class GLCodeController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(GLCodeController));

        /// <summary>
        /// Created By   : pavan
        /// Created Date : 14 May 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult GLCodes()
        {
            return View();
        }

        /// <summary>
        /// Created By   : pavan
        /// Created Date : 13 May 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Bind List in GLCodes View from db
        /// </summary>
        /// <param name="searchText">search keyword</param>
        /// <param name="status">status of the service </param>
        /// <param name="startpage"> start index page</param>
        /// <param name="rowsperpage">number of records per page</param>
        /// <param name="OrderBy">order by column </param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGLCodesData(string searchText, int status, int startPage, int rowsPerPage, string OrderBy)
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
                    var GLCodes = GLCode.GetAllGLCodes(startPage + 1, rowsPerPage + startPage, searchText, status, OrderBy);
                    return Json(GLCodes);
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
        /// Created By    : pavan
        /// Created Date  : 13 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the GLCode Info   
        /// </summary>
        /// <param name="GLCodeId"> GLCode ID</param>
        /// <param name="code"> Code</param>
        /// <param name="desc"> Description</param>
        /// <param name="status"> GLCode Status</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateGLCode(int GLCodeId, string code, string desc, bool status)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                Int32 returnValue = -2;
                int checkSession = UserLogin.AuthenticateRequest();
                int createdBy = Convert.ToInt32(Session["UserID"]);
                if (checkSession == 0)
                {
                    return Json(returnValue);
                }
                else
                {
                    var data = new GLCode
                    {
                        Id = GLCodeId,
                        Code = code,
                        Description = desc,
                        Status = status,
                        CreatedBy = createdBy
                    };

                    int result = data.InsertGLCode();
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
        /// Created By    : pavan
        /// Created Date  : 13 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Load the GLCode Data which you want to edit      
        /// </summary>
        /// <param name="serviceid">GLCodeId which record do you wan to Edit</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGLCode(int GLCodeId)
        {
            int checkSession = UserLogin.AuthenticateRequest();
            if (checkSession == 0)
            {
                return Json(checkSession);
            }
            else
            {
                var GlCode = GLCode.GetGLCodeById(GLCodeId);
                return Json(GlCode);
            }
        }

        /// <summary>
        /// Created By    : pavan
        /// Created Date  : 13 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Delete the GLCode record from Database       
        /// </summary>
        /// <param name="id">GLCode id which record do you wan to Delete</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteData(int id)
        {
            int checkSession = UserLogin.AuthenticateRequest();
            if (checkSession == 0)
            {
                return Json(checkSession);
            }
            else
            {
                int result = GLCode.DeleteGLCodeById(id);
                return Json(result);
            }
        }

    }
}