# region Document Header
//Created By       : Sudheer 
//Created Date     : 3rd Nov 2014
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.SystemReports.Controllers
{
    #region Usings
    using CSS2.Areas.SystemReports.Models;
    using CSS2.Models;
    using log4net;
    using OfficeOpenXml;
    using System;
    using System.Data;
    using System.IO;
    using CSS2.Areas.Billing.Models;
    using System.Web.Mvc;
    #endregion

    public class SystemReportsController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(SystemReportsController));
        //
        // GET: /SystemReports/SystemReports/
        [CheckUrlAccessCustomFilter]
        public ActionResult DisbursementItemsReport()
        {
            return View();
        }
        [CheckUrlAccessCustomFilter]
        public ActionResult InHouseDisbursementItemsReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult VendorVerificationReport()
        {
            return View();
        }
        [CheckUrlAccessCustomFilter]
        public ActionResult ClientFeeServiceReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult WOReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult NomineeDirectorReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult NomineeSecretaryReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult FeeAndDIReport()
        {
            return View();
        }


        [CheckUrlAccessCustomFilter]
        public ActionResult ScheduleFeeAndDIReport()
        {
            return View();
        }

        [CheckUrlAccessCustomFilter]
        public ActionResult FutureBillReport()
        {
            return View();
        }

        /// <summary>
        /// Description  : Get the MSchedule Information
        /// Created By   : Pavan
        /// Created Date : 19 January 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetMSchedule()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var MScheduleData = Billing.GetMSchedule();
                return Json(MScheduleData);
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
        /// Description  : To Download Excel report for Future Bill Report 
        /// Created By   : Pavan  
        /// Created Date : 19 January 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult FutureBillReport(string CompanyID, string Source, string BillingFreq, string FeeServiceType, string GroupId, string BillingDate, int BillingMonth)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objFutureBillDetails = new SystemReports()
                    {
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        FeeServiceType = FeeServiceType,
                        BillingFrequency = BillingFreq,
                        GroupCode = GroupId,
                        BillingDate = BillingDate,
                        Month = BillingMonth
                    };

                    DataSet dsDIItems = objFutureBillDetails.FutureBillReport();
                    if (dsDIItems.Tables.Count > 0)
                    {
                        dsDIItems.Tables[0].TableName = "Schedule Fee";
                    }
                    //dsDIItems.Tables[1].TableName = "DI Items";
                    //dsDIItems.Tables[2].TableName = "Fee Items";
                    HelperClasses.DownloadSystemReport(dsDIItems, "FutureBill");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Download Excel report for Disbursement Items Report 
        /// Created By   : Sudheer  
        /// Created Date : 3 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult DisbursementItemsReport(string Description, string FromDate, string ToDate, string ClientNo, string CompanyID, string Source, string GroupCode, string ItemNo)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objWoBonusDetails = new SystemReports()
                    {
                        Description = Description,
                        FromDate = FromDate,
                        ToDate = ToDate,
                        // CompanyName = CompanyName,
                        ClientNo = ClientNo,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        GroupCode = GroupCode,
                        ItemNo = ItemNo
                    };

                    DataSet dsDIItems = objWoBonusDetails.DIReport();
                    if (dsDIItems.Tables.Count > 0)
                    {
                        dsDIItems.Tables[0].TableName = "DI Items";
                    }
                    HelperClasses.DownloadSystemReport(dsDIItems, "DIReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Download Excel report for In-House Disbursement Items Report 
        /// Created By   : Shiva  
        /// Created Date : 4 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult InHouseDIReport(string Description, string FromDate, string ToDate, string GroupCode, string ItemNo, int CompanyID, string Source, bool InHouse = false)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objWoBonusDetails = new SystemReports()
                    {
                        Description = Description.Trim(),
                        FromDate = FromDate,
                        ToDate = ToDate,
                        GroupCode = GroupCode,
                        ItemNo = ItemNo,
                        InHouse = InHouse,
                        CompanyID = CompanyID,
                        Source = Source
                    };

                    DataSet dsInHouseDI = objWoBonusDetails.InHouseDIReport();
                    if (dsInHouseDI.Tables.Count > 0)
                    {
                        dsInHouseDI.Tables[0].TableName = "In-House DI Items";
                    }
                    HelperClasses.DownloadSystemReport(dsInHouseDI, "In-HouseDIReport");

                    #endregion

                    return View("InHouseDisbursementItemsReport");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View("InHouseDisbursementItemsReport");
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Vendor Verification Report
        /// Created By   : Sudheer  
        /// Created Date : 4 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult VendorVerificationReport(string FromDate, string ToDate, string DIitem, string VRID, string DIStatus)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {

                        FromDate = FromDate,
                        ToDate = ToDate,
                        DIitem = DIitem,
                        VRID = VRID,
                        DIStatus = DIStatus
                    };

                    DataSet dsVendorVerification = objSystemReports.VendorVerificationReport(objSystemReports);
                    if (dsVendorVerification.Tables.Count > 0)
                    {
                        dsVendorVerification.Tables[0].TableName = "Vendor Verification Items";
                    }
                    HelperClasses.DownloadSystemReport(dsVendorVerification, "VendorVerificationReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : ClientFeeServiceReport
        /// Created By   : Sudheer  
        /// Created Date : 5 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult ClientFeeServiceReport(string CompanyID, string Source, string GroupCode, string ClientNo, string FromDate, string ToDate, string FeeServiceType)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {
                        FromDate = FromDate,
                        ToDate = ToDate,
                        ClientNo = ClientNo,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        FeeServiceType = FeeServiceType,
                        GroupCode = GroupCode

                    };

                    DataSet dsClientFeeService = objSystemReports.ClientFeeServiceReport(objSystemReports);
                    if (dsClientFeeService.Tables.Count > 0)
                    {
                        dsClientFeeService.Tables[0].TableName = "Client Fee Service Items";
                    }
                    HelperClasses.DownloadSystemReport(dsClientFeeService, "FeeServiceReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Nominee Director Report
        /// Created By   : Sudheer  
        /// Created Date : 5 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult NomineeDirectorReport(string CompanyID, string Source, string GroupCode, string ClientNo, string FromDate, string ToDate, string NomineeDirector)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {
                        FromDate = FromDate,
                        ToDate = ToDate,
                        ClientNo = ClientNo,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        NomineeDirector = NomineeDirector,
                        GroupCode = GroupCode

                    };

                    DataSet dsNomineeDirector = objSystemReports.NomineeDirectorReport(objSystemReports);
                    if (dsNomineeDirector.Tables.Count > 0)
                    {
                        dsNomineeDirector.Tables[0].TableName = "Nominee Director Items";
                    }
                    HelperClasses.DownloadSystemReport(dsNomineeDirector, "NomineeDirector");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Nominee Secretary Report
        /// Created By   : Sudheer  
        /// Created Date : 5 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult NomineeSecretaryReport(string CompanyID, string Source, string GroupCode, string ClientNo, string FromDate, string ToDate, string NomineeSecretary)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {
                        FromDate = FromDate,
                        ToDate = ToDate,
                        ClientNo = ClientNo,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        NomineeSecretary = NomineeSecretary,
                        GroupCode = GroupCode

                    };

                    DataSet dsNomineeSecretary = objSystemReports.NomineeSecretaryReport(objSystemReports);
                    if (dsNomineeSecretary.Tables.Count > 0)
                    {
                        dsNomineeSecretary.Tables[0].TableName = "Nominee Secretary Items";
                        HelperClasses.DownloadSystemReport(dsNomineeSecretary, "NomineeSecretary");
                    }

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : get DI And Fee Items Billed through WO Report
        /// Created By   : Shiva  
        /// Created Date : 7 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult FEEAndDIItemsReport(string CompanyID, string Source, string FromDate, string ToDate, string GroupCode, string WOCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {

                        FromDate = FromDate,
                        ToDate = ToDate,
                        WoCode = WOCode,
                        GroupCode = GroupCode,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source
                    };

                    DataSet dsDIAndFeeItemsReport = objSystemReports.FEEAndDIItemsReport();
                    if (dsDIAndFeeItemsReport.Tables.Count > 0)
                    {
                        dsDIAndFeeItemsReport.Tables[0].TableName = "DI Items";
                        dsDIAndFeeItemsReport.Tables[1].TableName = "Fee Items";
                    }
                    HelperClasses.DownloadSystemReport(dsDIAndFeeItemsReport, "DIAndFeeItemsReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Download Excel report for Disbursement Items Report 
        /// Created By   : Shiva  
        /// Created Date : 5 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult WOReport(string WOStatus, string FromDate, string ToDate, string ClientNo, string CompanyID, string Source, string GroupCode, string Assignee, string Billable)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objWoBonusDetails = new SystemReports()
                    {
                        WOStatus = WOStatus,
                        FromDate = FromDate,
                        ToDate = ToDate,
                        ClientNo = ClientNo,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source,
                        GroupCode = GroupCode,
                        Assignee = Assignee,
                        Billable = Billable

                    };

                    DataSet dsWOReport = objWoBonusDetails.WOReport();
                    if (dsWOReport.Tables.Count > 0)
                    {
                        dsWOReport.Tables[0].TableName = "WO Items";
                    }
                    HelperClasses.DownloadSystemReport(dsWOReport, "WOReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : get Schedule DI And Fee Items Report
        /// Created By   : Shiva  
        /// Created Date : 25 November 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult ScheduleFEEAndDIReport(string CompanyID, string Source, string FromDate, string ToDate, string GroupCode, string WOCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int checkSession = UserLogin.AuthenticateRequest();
                if (checkSession == 0)
                {
                    return RedirectToAction("Login", "../Home"); ;
                }
                else
                {
                    #region Download

                    var objSystemReports = new SystemReports()
                    {

                        FromDate = FromDate,
                        ToDate = ToDate,
                        WoCode = WOCode,
                        GroupCode = GroupCode,
                        CompanyID = Convert.ToInt32(CompanyID == "" ? "0" : CompanyID),
                        Source = Source
                    };

                    DataSet dsScheduleDIAndFeeItemsReport = objSystemReports.ScheduleFEEAndDIItemsReport();
                    if (dsScheduleDIAndFeeItemsReport.Tables.Count > 0)
                    {
                        dsScheduleDIAndFeeItemsReport.Tables[0].TableName = "Schedule Fee Items";
                        dsScheduleDIAndFeeItemsReport.Tables[1].TableName = "DI Items";
                        dsScheduleDIAndFeeItemsReport.Tables[2].TableName = "Fee Items";
                    }

                    HelperClasses.DownloadSystemReport(dsScheduleDIAndFeeItemsReport, "ScheduleDIAndFeeItemsReport");

                    #endregion

                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return View();
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get Fee Details by FeeType
        /// Created By   : Pavan
        /// Created Date : 03 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetMFeesByFeeType(string FeeType)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var data = Masters.Models.Fee.GetFeeDetailsByFeeType(FeeType);
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

    }
}