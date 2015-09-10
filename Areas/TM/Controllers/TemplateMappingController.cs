# region Document Header
//Created By       : Sudheer 
//Created Date     : June 18th 2014
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.TM.Controllers
{
    #region Usings
    using CSS2.Areas.TM.Models;
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Mvc;
    using log4net;
    using System.Web.Script.Serialization;
    #endregion
    public class TemplateMappingController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(TemplateMappingController));

        #region Template Actions
        //
        // GET: /WO/TemplateMapping/
        [CheckUrlAccessCustomFilter]
        public ActionResult _TemplateMapping()
        {
            return View();
        }

        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Sudheer  
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult BindTemplateMapping(string WOID, string WOTYPE)
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
                    // int WoID = 6391; int WoTypeID = 1;
                    //int WoID = 5345; int WoTypeID = 6;
                    if (!string.IsNullOrEmpty(WOID) && !string.IsNullOrEmpty(WOTYPE))
                    {
                        var TemplateData = TemplateMapping.GetTemplateData(Convert.ToInt32(WOID), WOTYPE);

                        if (TemplateData.Count == 0)
                            return Json(0);
                        else
                            return Json(TemplateData);
                    }
                    else
                        return Json(-1);
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Sudheer  
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult InsertWOTemplateDetails(List<WOTemplateMapping> TemplateFields, string WOID, string WOTYPE)
        {
            int checkSession = UserLogin.AuthenticateRequest();
            if (checkSession == 0)
                return Json(checkSession);
            else
            {
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    DataTable dtTemplateDetails;
                    dtTemplateDetails = HelperClasses.ListToDataTable<WOTemplateMapping>(TemplateFields);

                    string UserIDSession = Convert.ToString(System.Web.HttpContext.Current.Session["UserID"]);

                    dtTemplateDetails.Columns.Remove("FileFullName");
                    dtTemplateDetails.Columns.Remove("FilePath");

                    var TemplateData = TemplateMapping.InsertWOTemplateDetails(dtTemplateDetails, UserIDSession, WOID);

                    if ((int)TemplateData == 0)
                        TemplateData = 1;

                    //DocumentGenerate objDocGenerate = new DocumentGenerate();
                    //objDocGenerate.GenrateDocuments(WOTYPE, Convert.ToInt32(WOID), TemplateFields);

                    return Json(TemplateData);

                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                }

                log.Debug("End: " + methodBase.Name);
                return Json(-2);
            }
        }

        #endregion

        #region Template Management

        [CheckUrlAccessCustomFilter]
        public ActionResult ManageTemplate()
        {
            return View();
        }

        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Sudheer  
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult BindTemplateDetails(string WOTYPE)
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
                    // int WoID = 6391; int WoTypeID = 1;
                    //int WoID = 5345; int WoTypeID = 6;
                    if (!string.IsNullOrEmpty(WOTYPE))
                    {
                        var TemplateData = TemplateMapping.GetTemplateData(WOTYPE);

                        if (TemplateData.Count == 0)
                            return Json(0);
                        else
                            return Json(TemplateData);
                    }
                    else
                        return Json(-1);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : Bind Template Set Dropdown
        /// Created By   : Sudheer  
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult BindTemplateSet(string WOTYPE)
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
                    if (!string.IsNullOrEmpty(WOTYPE))
                    {
                        var TemplateData = TemplateMapping.GetTemplateSetData(WOTYPE);
                        return Json(TemplateData);
                    }
                    else
                        return Json(-1);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        ///  Description : Saved Uploaded Template file into Application/DB
        /// Created By   : Sudheer  
        /// Created Date : 27th Aug 2014       
        /// </summary>
        /// <returns>UploadedFileName</returns>
        [HttpPost]
        public JsonResult BindMDocMultipleEntity(string wocode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var TemplateData = TemplateMapping.GetMDocMultipleEntity(wocode);
                return Json(TemplateData);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }

        }

        /// <summary>
        /// Description  : Bind Template Set Dropdown
        /// Created By   : Sudheer  
        /// Created Date : 17 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult DeleteTemplateDoc(string FileId, string FilePath)
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
                    if (!string.IsNullOrEmpty(FileId))
                    {
                        int Result = TemplateMapping.DeleteTemplateDoc(FileId);

                        if (Result == 1)
                        {
                            if (System.IO.File.Exists(Server.MapPath(FilePath)))
                                System.IO.File.Delete(Server.MapPath(FilePath));
                        }

                        return Json(Result);
                    }
                    else
                        return Json(-1);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Bind Template Set Dropdown
        /// Created By   : Sudheer  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>      
        [HttpPost]
        public JsonResult DeleteTemplateSet(string[] TemplateFiles, string SetID)
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
                    if (!string.IsNullOrEmpty(SetID))
                    {
                        int Result = TemplateMapping.DeleteTemplateSet(SetID);
                        if (Result > 0 && TemplateFiles != null)
                        {
                            foreach (string file in TemplateFiles)
                            {
                                if (System.IO.File.Exists(Server.MapPath(file)))
                                    System.IO.File.Delete(Server.MapPath(file));
                            }
                        }

                        return Json(Result);
                    }
                    else
                        return Json(-1);
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        ///  Description : Saved Uploaded Template file into Application/DB
        /// Created By   : Sudheer  
        /// Created Date : 27th Aug 2014       
        /// </summary>
        /// <returns>UploadedFileName</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(string Type, string Category, string TemplateDetails)
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

                string UserIDSession = Convert.ToString(System.Web.HttpContext.Current.Session["UserID"]);


                var json_serializer = new JavaScriptSerializer();
                Dictionary<string, object> routes_list = (Dictionary<string, object>)json_serializer.DeserializeObject(TemplateDetails);
                Dictionary<string, object> a = (Dictionary<string, object>)routes_list["TemplateDetails"];
                WOTemplateFileDetails arrFilesList = new WOTemplateFileDetails();

                arrFilesList.SetID = Convert.ToString(a["SetID"]);
                arrFilesList.DisplayName = Convert.ToString(a["DisplayName"]);
                arrFilesList.FilePath = Convert.ToString(a["FilePath"]);
                arrFilesList.Description = Convert.ToString(a["Description"]);
                arrFilesList.Status = Convert.ToString(a["Status"]);
                arrFilesList.IsDefault = Convert.ToBoolean(a["IsDefault"]);
                arrFilesList.IsMultiple = Convert.ToBoolean(a["IsMultiple"]);
                arrFilesList.MultipleEntity = (arrFilesList.IsMultiple == true ? Convert.ToString(a["MultipleEntity"]) : "");
                arrFilesList.FileName = Convert.ToString(a["FileName"]);

                string OldFilePath = string.Format("{0}{1}", Server.MapPath(arrFilesList.FilePath + "/"), arrFilesList.FileName);

                string flag = Convert.ToString(a["Flag"]);
                string fieldID = Convert.ToString(a["FieldID"]);

                if (Request.Files["file"].ContentLength > 0 && !string.IsNullOrEmpty(arrFilesList.FilePath))
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                    string path = string.Empty;
                    if (extension.ToUpper() == ".DOCX" || extension.ToUpper() == ".DOC")
                    {
                        try
                        {
                            //string FileName = System.IO.Path.GetFileNameWithoutExtension(Request.Files["file"].FileName) + DateTime.Now.Ticks + extension;
                            string FileName = System.IO.Path.GetFileNameWithoutExtension(arrFilesList.DisplayName) + "_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss") + extension;
                            path = string.Format("{0}{1}", Server.MapPath(arrFilesList.FilePath + "/"), FileName);
                            //File Name for Insert New record
                            arrFilesList.FileName = FileName;

                            Request.Files["file"].SaveAs(path);
                            TempData["FileName"] = FileName + "|" + Type;
                            TempData["Message"] = "FileUploaded" + "|" + Type + "|" + Category;
                            if (TemplateMapping.InsertTemplateFileDetails(arrFilesList, flag, fieldID, UserIDSession) == 1) //Success
                            {
                                if (System.IO.File.Exists(OldFilePath))
                                    System.IO.File.Delete(OldFilePath);
                            }
                            else
                            {
                                TempData["AlertMessage"] = "error";
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                            }

                        }
                        catch (Exception)
                        {
                            TempData["AlertMessage"] = "error";
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);
                        }

                    }
                    else
                        TempData["Message"] = "FileNotValid|" + Type + "|" + Category;

                }
                else if (flag == "Edit")
                {
                    TemplateMapping.InsertTemplateFileDetails(arrFilesList, flag, fieldID, UserIDSession);
                    TempData["Message"] = "FileUploaded|" + Type + "|" + Category;
                }
                else
                    TempData["Message"] = "FileNotValid|" + Type + "|" + Category;

                return RedirectToAction("ManageTemplate");

            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "error";
                log.Error("Error: " + ex);
                return RedirectToAction("ManageTemplate");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion


        #region Set


        [HttpPost]
        public JsonResult InsertSetDetails(string woCode, string setName, string description, bool setStatus, string SetID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var setData = new TemplateMapping
                {
                    SetID = (SetID == "0" ? 0 : Convert.ToInt32(SetID)),
                    WOCode = woCode,
                    DisplayName = setName,
                    TFileStatus = setStatus,
                    Description = description
                };

                var data1 = TemplateMapping.InsertSetDetails(setData);
                return Json(data1);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Json(-1);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        #endregion

        #region Genrate Docs

        public ActionResult Documents()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                string files = Request.QueryString["files"];
                ViewBag.Files = files;
                if (UserLogin.ValidateUserRequest())
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return View();
        }

        [HttpPost]
        public string Documents(string DocFiles)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var json_serializer = new JavaScriptSerializer();
                Dictionary<string, object> routes_list = (Dictionary<string, object>)json_serializer.DeserializeObject(DocFiles);

                string strFileIDs = string.Empty;

                object files = routes_list["TFiles"];
                object woid = routes_list["WOID"];
                object woCode = routes_list["WOTYPE"];

                object[] objFiles = (object[])files;

                ArrayList arrFilesList = new ArrayList();

                foreach (Dictionary<string, object> a in objFiles)
                {
                    //arrFilesList.Add(Convert.ToString(a["FileFullName"]));
                    strFileIDs += Convert.ToString(a["FID"]) + ",";
                }

                if (strFileIDs.Length > 0)
                    strFileIDs = strFileIDs.TrimEnd(',');

                DocumentGenerate objDocGenerate = new DocumentGenerate();

                objDocGenerate.GenrateDocuments(woCode.ToString(), Convert.ToInt32(woid), strFileIDs);
                int UserIDSession = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]);

                objDocGenerate.UpdateWOStatusInDoccuments(Convert.ToInt32(woid), UserIDSession);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ex.Message;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return string.Empty;
        }
        #endregion
    }
}