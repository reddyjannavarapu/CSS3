
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

namespace CSS2.Controllers
{
    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Web.Mvc;
    using System.IO;
    using System.Configuration;
    using Ionic.Zip;
    using System.Data;
    #endregion

    public class HomeController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return View();
        }

        public ActionResult About()
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                ViewBag.Message = "Welcome To CSS2";

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            if (UserLogin.ValidateUserRequest())
            {
                return View("Login");
            }

            return View();
        }

        public ActionResult Contact()
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (UserLogin.ValidateUserRequest())
                {

                    return View("Login");
                }

                ViewBag.Message = "Welcome To CSS2";

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);


            return View();
        }
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Login View </returns>
        public ActionResult Login()
        {
            string uatNo = ConfigurationManager.AppSettings["UATNo"].ToString();
            Session["uatNo"] = uatNo;
            return View();
        }
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  Sudheer ( Added Error message for other than invalid validation)
        /// Modified Date :  
        /// </summary>
        /// <returns>Login Button Click Post Login view</returns>
        [HttpPost]
        public ActionResult Login(UserLogin objuser)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (ModelState.IsValid)
                {
                    var data = UserLogin.CheckUsersLogin(objuser.LoginID, objuser.Password);
                    if (data.Count == 0)
                    {
                        ViewBag.invalidUser = "Invalid UserId/Password";
                        RedirectToAction("Login", "Home");
                    }
                    else
                    {

                        Session["LoginId"] = data[0].LoginID;
                        Session["UserID"] = data[0].UserID;
                        Session["UserName"] = data[0].UserName;
                        Session["CSS2SessionID"] = data[0].SessionID;
                        Session["IsSuperAdmin"] = data[0].IsSuperAdmin;
                        Session["isadmin"] = data[0].IsAdmin;
                        Session["Roles"] = data[0].Roles;

                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.invalidUser = "Login failed.";
                RedirectToAction("Login", "Home");
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return View();
        }
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Log Out  To disply Login view</returns>
        public ActionResult LogOut()
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["CSS2SessionID"])))
                {
                    UserLogin.UpdateSessionToken(Convert.ToString(Session["CSS2SessionID"]));
                }
                Session.Clear();

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 29 Dec 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Active Users List</returns>
        public ActionResult ActiveUsersList()
        {
            return View();
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 1 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Downloads Logs</returns>
        [HttpPost]
        public ActionResult ActiveUsersList(string command, FormCollection fc)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            if (command == "Download Logs")
            {
                #region Download as ZIP
                try
                {
                    string zipFloderName = "CSS2Logs";
                    string[] Files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Logs/"));
                    ZipFile createZipFile = new ZipFile();
                    string timestamp = DateTime.Now.ToString("yyyy-MMM-dd-HHmmss");
                    createZipFile.AddFiles(Files, zipFloderName + "_" + timestamp);
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.BufferOutput = false;
                    string zipName = String.Format(zipFloderName + "_{0}.zip", timestamp);
                    System.Web.HttpContext.Current.Response.ContentType = "application/zip";
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    createZipFile.Save(System.Web.HttpContext.Current.Response.OutputStream);
                    System.Web.HttpContext.Current.Response.End();
                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                }

                #endregion
            }
            else if (command == "Download")
            {
                #region Download TableData

                try
                {
                    DataSet dsTableData = UserLogin.GetTableDataByTableName(fc["ddlTableName"]);
                    if (dsTableData.Tables.Count > 0)
                    {
                        dsTableData.Tables[0].TableName = fc["ddlTableName"];
                    }
                    HelperClasses.DownloadSystemReport(dsTableData, fc["ddlTableName"]);
                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                }

                #endregion
            }

            log.Debug("End: " + methodBase.Name);

            return View();
        }


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 29 Dec 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Active Users List</returns>
        public JsonResult GetActiveUsersByDays(int NoOfDays)
        {
            var UsersData = UserLogin.GetActiveUsersByDays(NoOfDays);
            return Json(UsersData);
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 30 June 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        public JsonResult GetErrorLogs()
        {
            var Data = UserLogin.GetErrorLogs();
            return Json(Data);
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 1 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        public JsonResult GetCABErrors()
        {
            var Data = UserLogin.GetCABErrors();
            return Json(Data);
        }

        public JsonResult GetScheduleHistory()
        {
            var Data = UserLogin.GetScheduleHistory();
            return Json(Data);
        }


        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 11 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        public JsonResult GetAllTableNames()
        {
            var Data = UserLogin.GetAllTableNames();
            return Json(Data);
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 23 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        public JsonResult GetAllCss1IntegrationErrors()
        {
            var Data = UserLogin.GetCss1IntegrationErrors();
            return Json(Data);
        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 28 AUG 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> Ajax Call error Insert ErrorLog Table </returns>
        public JsonResult ErrorLogDetails(string ErrorMessage, string ErrorPage)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = HelperClasses.InsertErrorLogDetails(ErrorMessage, ErrorPage);
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
        /// Created By    : Pavan
        /// Created Date  : 15 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> To Bind Menu </returns>
        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 300, VaryByCustom = "User")]
        public JsonResult BindMenu()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                    return Json(checkSession);
                else
                {
                    var data = MenuBinding.GetMenuDataForUser(checkSession);
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
        /// Created By    : Pavan
        /// Created Date  : 18 March 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> To Bind Notifications</returns>
        [HttpPost]
        public JsonResult BindNotifications()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                    return Json(checkSession);
                else
                {
                    var data = Notifications.GetNotificationsData();
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

        #region DashBord
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 26 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> </returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetDashbordDetails()
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
                    Dashbord objDashbord = new Dashbord();
                    DashbordInfo DashbordInfo = new DashbordInfo();
                    int userID = Convert.ToInt32(Session["UserID"]);
                    var dataresult = objDashbord.GetDashbordDetails(userID);
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
        #endregion
    }
}
