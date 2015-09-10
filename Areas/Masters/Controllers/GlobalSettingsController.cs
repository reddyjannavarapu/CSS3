
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

    public class GlobalSettingsController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(GlobalSettingsController));

        /// <summary>
        /// Created By   : pavan
        /// Created Date : 14 May 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : If session is in valid then redirect to Login
        /// </summary>
        /// <returns></returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult GSettings()
        {
            return View();
        }

        /// <summary>
        /// Created By   : pavan
        /// Created Date : 13 May 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Bind List in GlobalSettings View from db
        /// </summary>
        /// <param name="searchText">search keyword</param>
        /// <param name="status">status of the service </param>
        /// <param name="startpage"> start index page</param>
        /// <param name="rowsperpage">number of records per page</param>
        /// <param name="OrderBy">order by column </param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGLSettingsData(string searchText, int status, int startPage, int rowsPerPage, string OrderBy)
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
                    var globalSettings = GlobalSetting.GetAllGlobalSettings(startPage + 1, rowsPerPage + startPage, searchText, status, OrderBy);
                    return Json(globalSettings);
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
        /// Description   : To Load the GlobalSetting Data which you want to edit      
        /// </summary>
        /// <param name="serviceid">gsettingsId which record do you wan to Edit</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGlobalSetting(int gSettingsId)
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
                    var globalSetting = GlobalSetting.GetGlobalSettingsById(gSettingsId);
                    return Json(globalSetting);
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
        /// Description   : To Delete the GlobalSetting record from Database       
        /// </summary>
        /// <param name="id">GlobalSetting id which record do you wan to Delete</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteData(int id)
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
                    int result = GlobalSetting.DeleteGlobalSettingsById(id);
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
        /// Description   : To Insert or Update the GlobalSetting Info   
        /// </summary>
        /// <param name="gsettingsId">GlobalSettings ID</param>
        /// <param name="code"> Code</param>
        /// <param name="desc"> Description</param>
        /// <param name="value"> Value</param>
        /// <param name="rangemin"> RangeMin</param>
        /// <param name="rangemax"> RangeMax</param>
        /// <param name="status"> GlobalSetting Status</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateGlobalSetting(int gSettingsId, string code, string desc, string value, string rangeMin, string rangeMax, bool status)
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
                    var data = new GlobalSetting
                    {
                        Id = gSettingsId,
                        Code = code,
                        Description = desc,
                        Value = value,
                        RangeMax = rangeMax,
                        RangeMin = rangeMin,
                        Status = status,
                        CreatedBy = createdBy
                    };

                    int result = data.InsertGlobalSettings();
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