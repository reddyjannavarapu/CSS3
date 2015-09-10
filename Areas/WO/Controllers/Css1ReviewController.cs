using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using CSS2.Models;
using System.Data;
using Newtonsoft.Json;

namespace CSS2.Areas.WO.Controllers
{
    public class Css1ReviewController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(Css1ReviewController));

        public ActionResult _Css1Review(string ReviewPopupHeader)
        {
            ViewBag.ReviewPopupHeader = ReviewPopupHeader;
            return View();
        }


        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 18th May 2015
        /// Modified By  :
        /// Modified Date:
        /// Description  : To Get Css1Info Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Css1ReviewDetails(string WOID, string WOType)
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
                    DataSet result = Masters.Models.Masters.Css1ReviewDetails.GetCss1ReviewDetails(WOID, WOType);
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