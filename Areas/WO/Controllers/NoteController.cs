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


namespace CSS2.Areas.WO
{

    #region Usings
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using System;
    using System.Web.Mvc;
    using log4net;
    using System.Web;
    #endregion

    public class NoteController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(NoteController));

        /// <summary>
        /// Description   : Note Partial View Action
        /// Created By    : Pavan
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public ActionResult _Note()
        {
            return View();
        }

        /// <summary>
        /// Description   : To Insert Note
        /// Created By    : Pavan
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult InsertNote(string Type, int ReferId, string Description, int ID, string Action)
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
                    var data = new Note
                    {
                        Type = Type,
                        ReferId = ReferId,
                        Description = HttpUtility.UrlDecode(Description),
                        ID = ID,
                        Action = Action,
                        CreatedBy = checkSession
                    };
                    int output = data.InsertNote();
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
        /// Description   : To Get Note Details By ReferId and Type
        /// Created By    : Pavan
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        public JsonResult GetNoteData(int ReferId, string Type, int startpage, int rowsperpage)
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

                    var data = Note.GetNoteData(ReferId, Type, UserID, startpage + 1, rowsperpage + startpage);
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
        /// Description   : To Delete Note (Update IsDelete to 1)
        /// Created By    : Pavan
        /// Created Date  : 5 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        /// 
        [HttpPost]
        public JsonResult DeleteNote(int ID)
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
                    var data = new Note
                    {
                        ID = ID,
                        CreatedBy = checkSession
                    };
                    int output = data.DeleteNote();
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



    }
}