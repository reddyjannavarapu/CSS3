
# region Document Header
//Created By       : Anji 
//Created Date     : 05 May 2014
//Description      : To Manage Services
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

    public class ServiceController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(ServiceController));

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 14 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> If session is In-valid then redirects to login</returns>
        [CheckUrlAccessCustomFilter]
        public ActionResult Services()
        {
            return View();
        }

        /// <summary>
        /// Created By    : Anji
        /// Created Date  : 06 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Load the Service records which are avilable in database       
        /// </summary>
        /// <param name="searchText">search keyword</param>
        /// <param name="status">status of the service </param>
        /// <param name="startpage"> start index page</param>
        /// <param name="rowsperpage">number of records per page</param>
        /// <param name="OrderBy">order by column </param>
        /// <returns> it will return the service list</returns>
        [HttpPost]
        public JsonResult GetServiceData(string searchText, int status, int startpage, int rowsperpage, string OrderBy)
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
                    var services = Service.GetAllServices(startpage + 1, rowsperpage + startpage, searchText, status, OrderBy);
                    return Json(services);
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
        /// Created By    : Anji
        /// Created Date  : 06 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Load the Service Data which you want to edit      
        /// </summary>
        /// <param name="serviceid">service id which record do you wan to Edit</param>
        /// <returns>it will returns the service info </returns>
        [HttpPost]
        public JsonResult GetService(int serviceid)
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
                    var services = Service.GetServiceById(serviceid);
                    return Json(services);
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
        /// Created By    : Anji
        /// Created Date  : 06 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Delete the Service info from Database       
        /// </summary>
        /// <param name="id">service id which record do you wan to Delete</param>
        /// <returns>it will returns the status of event </returns>
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
                    int result = Service.DeleteServiceById(id);
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
        /// Created By    : Anji
        /// Created Date  : 06 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the service Info 
        /// </summary>
        /// <param name="serviceId">Service Id</param>
        /// <param name="code"> Code</param>
        /// <param name="desc"> Description </param>
        /// <param name="status"> Service Status</param>
        /// <returns>it will returns the status of event </returns>
        [HttpPost]
        public JsonResult CreateService(int serviceId, string code, string desc, bool status)
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
                    var data = new Service
                    {
                        Id = serviceId,
                        Code = code,
                        Description = desc,
                        Status = status,
                        CreatedBy = createdBy
                    };

                    int result = data.InsertService();
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