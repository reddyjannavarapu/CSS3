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


namespace CSS2.Areas.VReports.Controllers
{
    #region Usings
    using CSS2.Areas.VReports.Models;
    using CSS2.Models;
    using log4net;
    using Newtonsoft.Json;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    #endregion

    public class VReportsController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(VReportsController));

        public const int ExcelStartRow = 1;


        // GET: /Reports/VendorReport/
        [CheckUrlAccessCustomFilter]
        public ActionResult VendorReport()
        {
            return View();
        }

        [HttpPost]
        public string GetData(int startpage, int rowsperpage, string OrderBy,string FromDate,string ToDate)
        {
            var v = new JavaScriptSerializer();
            var vendors = Vendor.GetAllVendorDetails(startpage + 1, rowsperpage + startpage, OrderBy,FromDate,ToDate);
            return v.Serialize(vendors);
        }

        [HttpPost]
        public JsonResult GetVendorReportType()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var VendorReportType = Vendor.GetVendorReportType();
                return Json(VendorReportType);
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


        [HttpPost]
        public JsonResult GetVendorUploadedDetails()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var VendorReportType = Vendor.GetVendorUploadedDetails();
                return Json(VendorReportType);
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
        /// Description  : To Get Vendor Upload Details By Type
        /// Created By   : Pavan  
        /// Created Date : 6 FEB 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetVendorUploadedDetailsByType(string Type)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var VendorReportType = Vendor.SpGetVendorUploadedDetailsByType(Type);
                return Json(VendorReportType);
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

        //GET: /Reports/VendorUpload/
        [CheckUrlAccessCustomFilter]
        public ActionResult VendorUpload()
        {

            return View();
        }

        /// <summary>
        /// Saved Uploaded file into Application
        /// </summary>
        /// <returns>UploadedFileName</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(string Title)
        {
            var ObjVendorInfo = new VendorInfo();
            if (Request.Files["file"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string path = string.Empty;
                if (extension.ToUpper() == ".XLSX" || extension.ToUpper() == ".XLS")
                {
                    try
                    {

                        string FileName = DateTime.Now.Ticks + extension;
                        path = string.Format("{0}{1}", Server.MapPath("~/VendorReportsExcelTemplate/"), FileName);

                        Request.Files["file"].SaveAs(path);

                        TempData["FileName"] = FileName + "|" + Title + "|" + Request.Files["file"].FileName;
                        TempData["Message"] = "FileUploaded";

                    }
                    catch (Exception)
                    {
                        TempData["AlertMessage"] = "error";
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }

                }
                else
                    TempData["Message"] = "FileNotValid";

            }

            return RedirectToAction("VendorUpload");


        }


        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 11 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Return Uploaded vendor lists(PERFECT MATCH,NO MATCH, Over Billed, Single Billed Discrepancy,
        ///  Single Unbilled Discrepancy,  Multiple Discrepancy </returns>
        /// </summary>

        [HttpPost]
        public JsonResult GetVendorUploadDetails(string fileName, string type,string file)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            string path = Path.Combine(Server.MapPath("~/VendorReportsExcelTemplate"), fileName);

            //var existingFile = new FileInfo(path);
            DataTable dtVendor = new DataTable();
            DataTable dtErrorRec = new DataTable();
            Vendor ObjVd = new Vendor();
            var VendorInfo = new VendorInfo();
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                try
                {
                    var workBook = pck.Workbook;
                    if (workBook != null)
                    {
                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            pck.Load(stream);
                        }
                        var currentWorksheet = pck.Workbook.Worksheets.First();

                        int Count=0; DataSet ds = new DataSet();
                        Count = ValidateExcel(type, currentWorksheet, Count, ds);
                        if (Count > 0)
                        {
                            string data = JsonConvert.SerializeObject(ds, Formatting.Indented);
                            return Json(data);
                        }


                        dtVendor = CreateVendorDataTable();
                        dtErrorRec = CreateErrorDataTable();
                        for (int rowNumber = ExcelStartRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                        // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                        {
                            //Column 4 : VendorRefNo    And     Column 5 : Amount    ARE MANDITORY
                            if (currentWorksheet.Cells[rowNumber, 4].Value != null && currentWorksheet.Cells[rowNumber, 5].Value != null)
                            {
                                try
                                {
                                    DataRow dr = dtVendor.NewRow();
                                    currentWorksheet.Cells[rowNumber, 1].Style.Numberformat.Format = "mm/dd/yyyy";
                                    dr["Date"] = (currentWorksheet.Cells[rowNumber, 1].Value == null ? DBNull.Value.ToString() : currentWorksheet.Cells[rowNumber, 1].Text);
                                    dr["UsedBy"] = (currentWorksheet.Cells[rowNumber, 2].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 2].Value);
                                    dr["Description"] = (currentWorksheet.Cells[rowNumber, 3].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 3].Value);
                                    dr["ReferenceNo"] = (currentWorksheet.Cells[rowNumber, 4].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 4].Value);
                                    dr["Amount"] = (currentWorksheet.Cells[rowNumber, 5].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 5].Value);
                                    dtVendor.Rows.Add(dr);
                                }
                                catch (Exception)
                                {
                                    DataRow dr = dtErrorRec.NewRow();
                                    currentWorksheet.Cells[rowNumber, 1].Style.Numberformat.Format = "mm/dd/yyyy";
                                    dr["Date"] = (currentWorksheet.Cells[rowNumber, 1].Value == null ? DBNull.Value.ToString() : currentWorksheet.Cells[rowNumber, 1].Text);
                                    dr["UsedBy"] = (currentWorksheet.Cells[rowNumber, 2].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 2].Value);
                                    dr["Description"] = (currentWorksheet.Cells[rowNumber, 3].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 3].Value);
                                    dr["ReferenceNo"] = (currentWorksheet.Cells[rowNumber, 4].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 4].Value);
                                    dr["Amount"] = (currentWorksheet.Cells[rowNumber, 5].Value == null ? DBNull.Value : currentWorksheet.Cells[rowNumber, 5].Value);
                                    dtErrorRec.Rows.Add(dr);
                                }
                            }
                           
                        }

                    }

                    ObjVd.dtvendor = dtVendor;
                    ObjVd.dtErrorRec = dtErrorRec;
                    ObjVd.RecordCount = dtVendor.Rows.Count;
                    ObjVd.Type = type;
                    ObjVd.Name = file;
                    string UserIDSession = Convert.ToString(System.Web.HttpContext.Current.Session["UserID"]);
                    bool checkForEmptyExcel = (dtVendor.Rows.Count == 0 && dtErrorRec.Rows.Count == 0) ? false : true;
                    if (!string.IsNullOrEmpty(UserIDSession))
                    {
                        if (checkForEmptyExcel)
                        {
                            ObjVd.UploadedBy = Convert.ToInt32(UserIDSession);
                            VendorInfo = ObjVd.InsertVendorRecords(ObjVd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                    VendorInfo.ExceptionMessage = "exception";
                }
                finally
                {
                    log.Debug("End: " + methodBase.Name);

                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
            }
            return Json(VendorInfo);
        }

        private int ValidateExcel(string type, ExcelWorksheet currentWorksheet, int Count, DataSet ds)
        {
            if (type.ToUpper() == "AC")
            {
                DataTable ACRAExcelTemplate = ACRAExcelHeaders();
                ds.Tables.Add(ACRAExcelTemplate);

                DataRow dr = ACRAExcelTemplate.NewRow();

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 1].Value)) != FormatString(ACRAExcelTemplate.Columns[0].ToString()))
                {
                    dr[ACRAExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;
                    Count++;
                }
                else
                    dr[ACRAExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 2].Value)) != FormatString(ACRAExcelTemplate.Columns[1].ToString()))
                {
                    dr[ACRAExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;
                    Count++;
                }
                else
                    dr[ACRAExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 3].Value)) != FormatString(ACRAExcelTemplate.Columns[2].ToString()))
                {
                    dr[ACRAExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;
                    Count++;
                }
                else
                    dr[ACRAExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 4].Value)) != FormatString(ACRAExcelTemplate.Columns[3].ToString()))
                {
                    dr[ACRAExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;
                    Count++;
                }
                else
                    dr[ACRAExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 5].Value)) != FormatString(ACRAExcelTemplate.Columns[4].ToString()))
                {
                    dr[ACRAExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;
                    Count++;
                }
                else
                    dr[ACRAExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;

                ACRAExcelTemplate.Rows.Add(dr);


            }
            else if (type.ToUpper() == "CO")
            {
                DataTable CourierExcelTemplate = CourierExcelHeaders();
                ds.Tables.Add(CourierExcelTemplate);

                DataRow dr = CourierExcelTemplate.NewRow();

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 1].Value)) != FormatString(CourierExcelTemplate.Columns[0].ToString()))
                {
                    dr[CourierExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;
                    Count++;
                }
                else
                    dr[CourierExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 2].Value)) != FormatString(CourierExcelTemplate.Columns[1].ToString()))
                {
                    dr[CourierExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;
                    Count++;
                }
                else
                    dr[CourierExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 3].Value)) != FormatString(CourierExcelTemplate.Columns[2].ToString()))
                {
                    dr[CourierExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;
                    Count++;
                }
                else
                    dr[CourierExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 4].Value)) != FormatString(CourierExcelTemplate.Columns[3].ToString()))
                {
                    dr[CourierExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;
                    Count++;
                }
                else
                    dr[CourierExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 5].Value)) != FormatString(CourierExcelTemplate.Columns[4].ToString()))
                {
                    dr[CourierExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;
                    Count++;
                }
                else
                    dr[CourierExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;

                CourierExcelTemplate.Rows.Add(dr);


            }
            
            
            else if (type.ToUpper() == "IR")
            {
                DataTable IRASExcelTemplate = IRASExcelHeaders();
                ds.Tables.Add(IRASExcelTemplate);

                DataRow dr = IRASExcelTemplate.NewRow();

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 1].Value)) != FormatString(IRASExcelTemplate.Columns[0].ToString()))
                {
                    dr[IRASExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;
                    Count++;
                }
                else
                    dr[IRASExcelTemplate.Columns[0].ToString()] = currentWorksheet.Cells[ExcelStartRow, 1].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 2].Value)) != FormatString(IRASExcelTemplate.Columns[1].ToString()))
                {
                    dr[IRASExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;
                    Count++;
                }
                else
                    dr[IRASExcelTemplate.Columns[1].ToString()] = currentWorksheet.Cells[ExcelStartRow, 2].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 3].Value)) != FormatString(IRASExcelTemplate.Columns[2].ToString()))
                {
                    dr[IRASExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;
                    Count++;
                }
                else
                    dr[IRASExcelTemplate.Columns[2].ToString()] = currentWorksheet.Cells[ExcelStartRow, 3].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 4].Value)) != FormatString(IRASExcelTemplate.Columns[3].ToString()))
                {
                    dr[IRASExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;
                    Count++;
                }
                else
                    dr[IRASExcelTemplate.Columns[3].ToString()] = currentWorksheet.Cells[ExcelStartRow, 4].Value;

                if (FormatString(Convert.ToString(currentWorksheet.Cells[ExcelStartRow, 5].Value)) != FormatString(IRASExcelTemplate.Columns[4].ToString()))
                {
                    dr[IRASExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;
                    Count++;
                }
                else
                    dr[IRASExcelTemplate.Columns[4].ToString()] = currentWorksheet.Cells[ExcelStartRow, 5].Value;

                IRASExcelTemplate.Rows.Add(dr);


            }
            return Count;
        }

        private string FormatString(string p)
        {
            return p.Replace(" ", "").Replace(".", "").ToUpper();
        }


        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 16 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Return Uploaded vendor lists(PERFECT MATCH,NO MATCH, Over Billed, Single Billed Discrepancy,
        ///  Single Unbilled Discrepancy,  Multiple Discrepancy </returns>
        /// </summary>        
        [HttpPost]
        public JsonResult LoadVendorUploadHistory(string VRID, string GroupCode, string ClientName, string VendorRef)
        {
            var objVendor = new Vendor();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            var VendorInfo = new VendorInfo();
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                try
                {
                    string UserIDSession = Convert.ToString(System.Web.HttpContext.Current.Session["UserID"]);
                    if (!string.IsNullOrEmpty(UserIDSession))
                    {
                        objVendor.VRID = Convert.ToInt32(VRID);
                        objVendor.GroupName=GroupCode;
                        objVendor.ClientName = ClientName;
                        objVendor.VenderReferenceID = HttpUtility.UrlDecode(VendorRef);
                        VendorInfo = objVendor.getVendorUploadedDetails(objVendor);
                    }

                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                    VendorInfo.ExceptionMessage = "exception";
                }

            }
            log.Debug("End: " + methodBase.Name);

            return Json(VendorInfo);

        }

        /// <summary>
        ///              
        /// Created By    : SUDHEER
        /// Created Date  : 16 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Summary       : Method is used to Verify Billed and Unbilled Discrepancy Values.
        /// <returns> Return Uploaded vendor lists(PERFECT MATCH,NO MATCH, Over Billed, Single Billed Discrepancy,
        ///  Single Unbilled Discrepancy,  Multiple Discrepancy </returns>
        /// </summary>        
        [HttpPost]
        public JsonResult UpdateDI(List<DIFields> DIFields, string VendorID, string Action, string DiscType)
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
                    Vendor objVd = new Vendor();
                    var VendorInfo = new VendorInfo();

                    DataTable dt = HelperClasses.ListToDataTable<DIFields>(DIFields);
                    objVd.dtvendor = dt;
                    objVd.UploadedBy = Convert.ToInt32(Session["UserID"]);
                    objVd.VRID = Convert.ToInt32(VendorID);
                    objVd.Name = Action;
                    objVd.Type = DiscType;
                    VendorInfo = objVd.UplateBilledUnBilledDiscrepancy(objVd);
                    return Json(VendorInfo);
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
        ///              
        /// Created By    : SUDHEER
        /// Created Date  : 16 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Summary       : Method is used to Verify Billed and Unbilled Discrepancy Values.
        /// <returns> Return Uploaded vendor lists(PERFECT MATCH,NO MATCH, Over Billed, Single Billed Discrepancy,
        ///  Single Unbilled Discrepancy,  Multiple Discrepancy </returns>
        /// </summary>        
        [HttpPost]
        public JsonResult UpdateMultipleDI(List<DIFields> DIFields, string VendorID, string Action)
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
                    Vendor objVd = new Vendor();
                    var VendorInfo = new VendorInfo();
                    DataTable dt = HelperClasses.ListToDataTable<DIFields>(DIFields);
                    objVd.dtvendor = dt;
                    objVd.VRID = Convert.ToInt32(VendorID);
                    objVd.UploadedBy = Convert.ToInt32(Session["UserID"]);
                    objVd.Name = Action;
                    VendorInfo = objVd.UplateMultipleDiscrepancy(objVd);
                    return Json(VendorInfo);
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
        /// Return Datatable with Vendor Details 
        /// </summary>
        /// <returns></returns>
        private static DataTable CreateVendorDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("UsedBy", typeof(string));
            dt.Columns.Add("ReferenceNo", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            return dt;
        }

        /// <summary>
        /// Return Datatable with Vendor Details 
        /// </summary>
        /// <returns></returns>
        private static DataTable CreateErrorDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("UsedBy", typeof(string));
            dt.Columns.Add("ReferenceNo", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            return dt;
        }



        /// <summary>
        /// Created By   : pavan
        /// Created Date : 6 June 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : GetVendorReport Details By VRID to Download
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVendorReportDetailsByVRID(int VRID)
        {
            int checkSession = UserLogin.AuthenticateRequest();
            if (checkSession == 0)
            {
                return Json(checkSession);
            }
            else
            {
                // DataTable dt = Vendor.GetVendorReportDetailsByVRID(VRID);
                //  File.Delete(newfilename);

                return Json("");
            }
        }


        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 10 June 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : GetVendorReport Details Download  
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadReport(int VRID)
        {
            DownloadVendorReport(VRID);
            return View("VendorReport");
        }

        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 10 June 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : GetVendorReport Details Download By VRID  
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public void DownloadVendorReport(int VRID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                DataSet dtVendorReportDataByVRID = Vendor.GetVendorReportDetailsByVRID(VRID);
                dtVendorReportDataByVRID.Tables[0].TableName = "VendorReport";
                HelperClasses.DownloadSystemReport(dtVendorReportDataByVRID, "VendorReportDetails");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
        }

        public class DIFields
        {
            public string DescAmount { get; set; }
            public string DI { get; set; }
            public string VRDID { get; set; }
            public string Status { get; set; }
        }

        /// <summary>
        /// Return Datatable with ACRA Excel Headers Details 
        /// </summary>
        /// <returns></returns>
        private static DataTable ACRAExcelHeaders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("SIC", typeof(string));
            dt.Columns.Add("Transaction Type", typeof(string));
            dt.Columns.Add("Receipt No.", typeof(string));
            dt.Columns.Add("Debit", typeof(string));
            return dt;
        }
        
        /// <summary>
        /// Return Datatable with Courier Excel Headers Details 
        /// </summary>
        /// <returns></returns>
        private static DataTable CourierExcelHeaders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("SIC", typeof(string));
            dt.Columns.Add("Destination", typeof(string));
            dt.Columns.Add("Tracking No.", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            return dt;
        }


        /// <summary>
        /// Return Datatable with Courier Excel Headers Details 
        /// </summary>
        /// <returns></returns>
        private static DataTable IRASExcelHeaders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Effective Date", typeof(string));
            dt.Columns.Add("SIC", typeof(string));
            dt.Columns.Add("Transaction Description", typeof(string));
            dt.Columns.Add("Doc Ref. No.", typeof(string));
            dt.Columns.Add("Liability", typeof(string));
            return dt;
        }

    }
}