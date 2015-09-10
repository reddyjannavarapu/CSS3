using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSS2.Models;
using Newtonsoft.Json;
using log4net;

namespace CSS2.Areas.WO.Controllers
{
    public class Css1InfoController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(Css1InfoController));
        //
        // GET: /WO/Css1Info/
        public ActionResult _Css1Info(string PopupHeader)
        {
            ViewBag.PopupHeader = PopupHeader;
            return View();
        }

        /// <summary>
        /// Created By   : Sudheer
        /// Created Date : 21st Aug 2013
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get Css1Info Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Css1InfoDetails(string WOID, string WOType)
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
                    DataSet result = Masters.Models.Masters.Css1InfoDetail.GetCss1InfoDetails(WOID, WOType);
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
    }
}