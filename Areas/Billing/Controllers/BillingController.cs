
# region Document Header
//Created By       : Anji 
//Created Date     : 15 August 2014
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.Billing.Controllers
{
    #region Usings
    using CSS2.Areas.Billing.Models;
    using CSS2.Models;
    using DocumentFormat.OpenXml.Drawing;
    using Ionic.Zip;
    using log4net;
    using Newtonsoft.Json;
    using OfficeOpenXml;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    public class BillingController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(BillingController));

        /// <summary>
        /// Description  : If the session is valid then redirect to BillingSetting other wise login view.
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        // GET: /Billing/Billing/
        [CheckUrlAccessCustomFilter]
        public ActionResult BillingSetting()
        {
            return View();
        }

        /// <summary>
        /// Description  : If the session is valid then redirect to AddThirdParty other wise login view.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        // GET: /Billing/Billing/
        [CheckUrlAccessCustomFilter]
        public ActionResult AddThirdParty()
        {
            return View();
        }



        /// <summary>
        /// Description  : If the session is valid then redirect to ViewBillingData other wise login view.
        /// Created By   : Shiva
        /// Created Date : 8 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        // GET: /Billing/Billing/
        [CheckUrlAccessCustomFilter]
        public ActionResult ViewBillingData()
        {
            return View();
        }


        /// <summary>
        /// Description  : Display Invoice Preview Details.
        /// Created By   : Shiva
        /// Created Date : 30 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        // GET: /Billing/Billing/
        public ActionResult _InvoicePreviewDetails()
        {
            return View();
        }


        public ActionResult InvoiceErrors()
        {
            return View();
        }

        /// <summary>
        /// Description  : Get the MSchedule Information
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
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
        /// Description  : Get the MMonth Information
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetMMonth()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var MMonthData = Billing.GetMMonth();
                return Json(MMonthData);
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
        /// Description  : Get the MFEE Information
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetMFEE()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var MFEE = Billing.GetMFee();
                return Json(MFEE);
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
        /// Description  : Get the MFEE in billing settings page
        /// Created By   : Anji
        /// Created Date : 25 May 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetMFEEforClient(int ClientCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var MFEE = Billing.GetMFEEforClient(ClientCode);
                return Json(MFEE);
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
        /// Description  : Insert Or Update the ClientFeeSettings
        /// Created By   : Shiva
        /// Created Date : 2nd July 2014
        /// Modified By  : Shiva    
        /// Modified Date: 4 July 2014
        /// </summary>
        [HttpPost]
        public JsonResult InsertOrUpdateClientFeeSettings(int ClientCode, string SourceID, int BillTo, string BillingParty, bool IsClubFee, string AccountCode, bool Status)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {

                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.InsertOrUpdateClientFeeSettings(ClientCode, SourceID, BillTo, BillingParty, IsClubFee, AccountCode, Status, CreatedBy);
                    return Json(CreatedBy);
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
        /// Description  : Insert Or Update the ClientFeeMapping
        /// Created By   : Shiva
        /// Created Date : 2nd July 2014
        /// Modified By  : Shiva
        /// Modified Date: 4 July 2014
        /// </summary>
        [HttpPost]
        public JsonResult InsertOrUpdateClientFeeMapping(int ClientCode, string SourceID, string FeeCode, string BillingFrequency, string BillingMonth, bool IsBillArrears, bool Status, string SecurityDeposit, string SecurityDepositInvoiceNo, string FeeDueToNominee)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.InsertOrUpdateClientFeeMapping(ClientCode, SourceID, FeeCode, BillingFrequency, BillingMonth, IsBillArrears, Status, SecurityDeposit, HttpUtility.UrlDecode(SecurityDepositInvoiceNo), CreatedBy, FeeDueToNominee);
                    return Json(CreatedBy);
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
        /// Description  : Insert Or Update the ClientFeeSchedule
        /// Created By   : Shiva
        /// Created Date : 3rd June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult InsertOrUpdateClientFeeSchedule(int ID, int CFMID, int ClientCode, string SourceID, string FeeCode, string FromDate, string ToDate, decimal Amount, bool Status)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.InsertOrUpdateClientFeeSchedule(ID, CFMID, ClientCode, SourceID, FeeCode, FromDate, ToDate, Amount, Status, CreatedBy);
                    return Json(CreatedBy);
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
        /// Description  : Get the ClientFeeSetting and ClientFeeMapping Information by ClientCode
        /// Created By   : Shiva
        /// Created Date : 2nd July 2014
        /// Modified By  : Shiva
        /// Modified Date: 4 July 2014
        /// </summary>
        [HttpPost]
        public JsonResult GetFeeSettingAndMappingsByClientCode(int ClientCode, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GetFeeSettingAndMappings = Billing.GetFeeSettingAndMappingsByClientCode(ClientCode, SourceID);
                    return Json(GetFeeSettingAndMappings);
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
        /// Description  : Get the ClientFeeMapping Information by FeeCode
        /// Created By   : Shiva
        /// Created Date : 3rd July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetFeeMappingsByFeeAndClientCode(string FeeCode, int ClientCode, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GetFeeMappings = Billing.GetFeeMappingsByFeeAndClientCode(FeeCode, ClientCode, SourceID);
                    return Json(GetFeeMappings);
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
        /// Description  : Get the ClientFeeSchedule Information by FeeCode
        /// Created By   : Shiva
        /// Created Date : 3rd July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetClientFeeScheduleByClientAndFeeCode(string FeeCode, int ClientCode, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GetClientFeeSchedule = Billing.GetClientFeeScheduleByClientAndFeeCode(FeeCode, ClientCode, SourceID);
                    return Json(GetClientFeeSchedule);
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
        /// Description  : Delete ClientSchedule details by ID
        /// Created By   : Shiva
        /// Created Date : 4 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult DeleteClientScheduleByID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.DeleteClientScheduleByID(ID);
                    return Json(CreatedBy);
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
        /// Description  : Delete all settings by Client
        /// Created By   : Pavan
        /// Created Date : 08 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult DeleteAllSettingsByClient(int ClientCode, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.DeleteAllSettingsByClient(ClientCode, SourceID);
                    return Json(CreatedBy);
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
        /// Description  : Delete Fee settings by Client and Feecode
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult DeleteFeeSettings(int ClientCode, string SourceID, string FeeCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = Billing.DeleteFeeSettings(ClientCode, SourceID, FeeCode);
                    return Json(CreatedBy);
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

        #region CABInvoice Errorr

        /// <summary>
        /// Description  : Get All CAB Invoice Errors
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetAllCABInvoiceErrors(string ClientId, string SourceID, int startpage, int rowsperpage, string FromDate, string ToDate)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GetAllCABInvoiceErrors = Billing.GetAllCABInvoiceErrors(ClientId, SourceID, startpage + 1, rowsperpage + startpage, FromDate, ToDate);
                    return Json(GetAllCABInvoiceErrors);
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
        /// Description  : Get CAB Invoice Error Details By CABMasterID
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetCABInvoiceErrorByCABMasterID(int CABMasterID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GetCABInvoiceErrorByCabMasterID = Billing.GetCABInvoiceErrorByCABMasterID(CABMasterID);
                    string data = JsonConvert.SerializeObject(GetCABInvoiceErrorByCabMasterID, Formatting.Indented);
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
        /// Description  : Delete CAB Invoice Error Details By CABMasterID
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult DeleteCABInvoiceErrorByCABMasterID(int CABMasterID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var data = Billing.DeleteCABInvoiceErrorByCABMasterID(CABMasterID);
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

        #endregion


        /// <summary>
        /// Description  : Gap analysis by FeeCode and ClientCode.
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetClientScheduleGapByFeeAndClientCode(int ClientCode, string SourceID, string FeeCode)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var GapScheduleDataByCodes = Billing.GetClientScheduleGapByFeeAndClientCode(FeeCode, ClientCode, SourceID);
                    return Json(GapScheduleDataByCodes);
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
        /// Description  : To Save the Billing third party details.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult SaveBillingThirdPartyDetails(BillingThirdParty ThirdPartyDetails)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var objSaveThirdParty = new BillingThirdParty()
                    {
                        ID = ThirdPartyDetails.ID,
                        CompanyName = ThirdPartyDetails.CompanyName,
                        ACCPACCode = ThirdPartyDetails.ACCPACCode,
                        AddressLine1 = ThirdPartyDetails.AddressLine1,
                        AddressLine2 = ThirdPartyDetails.AddressLine2,
                        AddressLine3 = ThirdPartyDetails.AddressLine3,
                        CountryCode = ThirdPartyDetails.CountryCode,
                        PostalCode = ThirdPartyDetails.PostalCode,
                        Email = ThirdPartyDetails.Email,
                        ContactNo = ThirdPartyDetails.ContactNo,
                        Fax = ThirdPartyDetails.Fax,
                        SavedBy = CreatedBy
                    };
                    CreatedBy = objSaveThirdParty.SaveBillingThirdPartyDetails();
                    return Json(CreatedBy);
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
        /// Description  : To get all Billing third party details.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult BindThirdPartyBillingDetails(string CompanyName, string OrderBy, int StartPage, int RowsPerPage)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    var ThirdPartyBillingData = BillingThirdParty.BindThirdPartyBillingDetails(HttpUtility.UrlDecode(CompanyName), OrderBy, StartPage + 1, StartPage + RowsPerPage);
                    return Json(ThirdPartyBillingData);
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
        /// Description  : To Delete Billing third party details by ID.
        /// Created By   : Shiva
        /// Created Date : 26 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult DeleteBillingThirdPartyDetailsByID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int CreatedBy = UserLogin.AuthenticateRequest();
                if (CreatedBy == 0)
                {
                    return Json(CreatedBy);
                }
                else
                {
                    CreatedBy = BillingThirdParty.DeleteBillingThirdPartyDetailsByID(ID);
                    return Json(CreatedBy);
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
        /// Description  : To get Company from Billing third party details.
        /// Created By   : Shiva
        /// Created Date : 27 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetCompanyFromBillingThirdParty()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var CompanyData = Billing.GetCompanyFromBillingThirdParty();
                return Json(CompanyData);
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
        /// Description  : To get All Billing details by Company AND Fee.
        /// Created By   : Shiva
        /// Created Date : 7 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetAllBillingDetailsByCompanyAndFee(string CompanyID, string CompanySource, string FeeCode, string BillFromDate, string BillToDate, int StartPage, int ResultPerPage, int BillType)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var CABMasterData = Billing.GetAllBillingDetailsByCompanyAndFee(CompanyID, CompanySource, FeeCode, BillFromDate, BillToDate, StartPage + 1, ResultPerPage + StartPage, BillType);
                return Json(CABMasterData);
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
        /// Description  : To get CABFeeSchedule Report By CABMasterID.
        /// Created By   : Shiva
        /// Created Date : 9 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetCABFeeScheduleReportByMasterID(int CABMasterID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var CABFeeScheduleData = Billing.GetCABFeeScheduleReportByMasterID(CABMasterID);
                return Json(CABFeeScheduleData);
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
        /// Description  : To get CABFeeScheduleDetails Report By CABFeeScheduleID.
        /// Created By   : Shiva
        /// Created Date : 9 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetCABFeeScheduleDetailsReportByCABFeeScheduleID(int CABFeeScheduleID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var CABFeeScheduleDetailsData = Billing.GetCABFeeScheduleDetailsReportByCABFeeScheduleID(CABFeeScheduleID);
                return Json(CABFeeScheduleDetailsData);
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
        /// Description  : If the session is valid then redirect to Invoice other wise login view.
        /// Created By   : Pavan
        /// Created Date : 28 October 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public ActionResult InvoiceFromCSS2()
        {
            return View();
        }

        /// <summary>
        /// Description  : To Download and Read the Excel of Invoice From CSS2
        /// Created By   : Pavan
        /// Created Date : 28 October 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public ActionResult InvoiceFromCSS2(string command)
        {
            try
            {
                if (command == "Sent To ACCPAC")
                {
                    #region Download

                    DataSet dsInvoice = Billing.GetCABPrepareInvoiceFromCSS2();
                    if (dsInvoice.Tables[0].Rows.Count > 0)
                    {
                        var fileName = "INVOICE_FROM_CSS2_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";


                        //var outputDir = HttpContext.Server.MapPath("~/CSS2ACCPACINTEGRATION/FromCSS2/");
                        //System.IO.FileInfo file = new FileInfo(outputDir + fileName);

                        string path = ConfigurationManager.AppSettings["AccpacFolderPath"].ToString() + "FromCSS2/";
                        System.IO.FileInfo file = new FileInfo(path + fileName);

                        using (var excely = new ExcelPackage(file))
                        {

                            #region WorkSheet 1

                            ExcelWorksheet worksheet = excely.Workbook.Worksheets.Add("INVOICE_FROM_CSS2");
                            //for Columns Names
                            worksheet.Cells[1, 1].LoadFromDataTable(dsInvoice.Tables[0], true);
                            //For Data
                            for (int i = 0; i < dsInvoice.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < dsInvoice.Tables[0].Columns.Count; j++)
                                {
                                    string cellvalue = dsInvoice.Tables[0].Rows[i][j].ToString();
                                    worksheet.Cells[i + 2, j + 1].Value = cellvalue;
                                }
                            }

                            #endregion

                            #region Work Sheet 2

                            ExcelWorksheet worksheet1 = excely.Workbook.Worksheets.Add("INVOICE_DETAILS_FROM_CSS2");

                            //for Columns Names
                            worksheet1.Cells[1, 1].LoadFromDataTable(dsInvoice.Tables[1], true);
                            //For Data
                            for (int i = 0; i < dsInvoice.Tables[1].Rows.Count; i++)
                            {
                                for (int j = 0; j < dsInvoice.Tables[1].Columns.Count; j++)
                                {
                                    string cellvalue = dsInvoice.Tables[1].Rows[i][j].ToString();
                                    worksheet1.Cells[i + 2, j + 1].Value = cellvalue;
                                }
                            }


                            #endregion
                           

                            #region Update SentStatus

                            string CABMasterIDs = string.Empty;
                           
                            foreach (DataRow dr in dsInvoice.Tables[0].Rows)
                            {
                                CABMasterIDs += dr["CNTITEM"].ToString() + ",";
                            }

                            Billing.CABUpdateSentStatus(CABMasterIDs.TrimEnd(','));

                            #endregion

                            #region Invoice Log

                            string Direction = "InvoiceFromCSS2";
                            int InvoiceCount = dsInvoice.Tables[0].Rows.Count;
                            int DetailsCount = dsInvoice.Tables[1].Rows.Count;
                            int Result = Billing.InsertCABInvoiceLog(Direction, InvoiceCount, DetailsCount, fileName);

                            #endregion

                            excely.Save();
                            worksheet.Protection.IsProtected = true;
                            worksheet1.Protection.IsProtected = true;

                        }
                    }

                    #endregion
                }
                else if (command == "Read From ACCPAC")
                {
                    #region Read Excel

                    //var ExcelFiles = Directory.EnumerateFiles(HttpContext.Server.MapPath("~/CSS2ACCPACINTEGRATION/FromACCPAC/"), "*.xlsx");

                    string path = ConfigurationManager.AppSettings["AccpacFolderPath"].ToString() + "FromACCPAC/";
                    var ExcelFiles = Directory.EnumerateFiles(path, "*.xlsx");

                    foreach (string currentFile in ExcelFiles)
                    {
                        using (var pck = new ExcelPackage())
                        {
                            using (var stream = System.IO.File.OpenRead(currentFile))
                            {
                                pck.Load(stream);
                            }

                            int StartRow = 2;
                            int sheetCount = 1;
                            DataSet ds = new DataSet();

                            foreach (var currentWorksheet in pck.Workbook.Worksheets)
                            {
                                DataTable dtSheet = new DataTable(currentWorksheet.Name);

                                #region Sheet 1

                                if (sheetCount == 1)
                                {
                                    dtSheet.Columns.Add("CNTITEM");
                                    dtSheet.Columns.Add("INVOICENO");
                                    dtSheet.Columns.Add("HSCODE_FROMACCPAC");

                                    for (int rowNumber = StartRow; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value != null)
                                        {
                                            DataRow dr = dtSheet.NewRow();
                                            dr["CNTITEM"] = currentWorksheet.Cells[rowNumber, 1].Value;
                                            dr["INVOICENO"] = currentWorksheet.Cells[rowNumber, 2].Value;
                                            dr["HSCODE_FROMACCPAC"] = currentWorksheet.Cells[rowNumber, 3].Value;
                                            dtSheet.Rows.Add(dr);
                                        }
                                    }
                                }

                                #endregion

                                #region Sheet 2

                                else if (sheetCount == 2)
                                {
                                    dtSheet.Columns.Add("CNTITEM");
                                    dtSheet.Columns.Add("CNTSOURCE");
                                    dtSheet.Columns.Add("CNTLINE");
                                    dtSheet.Columns.Add("HSCODE_FROMACCPAC");

                                    for (int rowNumber = StartRow; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        DataRow dr = dtSheet.NewRow();
                                        dr["CNTITEM"] = currentWorksheet.Cells[rowNumber, 1].Value;
                                        dr["CNTSOURCE"] = currentWorksheet.Cells[rowNumber, 2].Value;
                                        dr["CNTLINE"] = currentWorksheet.Cells[rowNumber, 3].Value;
                                        dr["HSCODE_FROMACCPAC"] = currentWorksheet.Cells[rowNumber, 4].Value;
                                        dtSheet.Rows.Add(dr);
                                    }
                                }

                                #endregion

                                ds.Tables.Add(dtSheet);

                                sheetCount++;
                            }

                            #region Update ReceiveStatus

                            int ReceiveStatus = Billing.CABUpdateReceiveStatus(ds.Tables[0], ds.Tables[1]);

                            #endregion

                            #region Invoice Log

                            string Direction = "InvoiceFromACCPAC";
                            int InvoiceCount = ds.Tables[0].Rows.Count;
                            int DetailsCount = ds.Tables[1].Rows.Count;
                            int Result = Billing.InsertCABInvoiceLog(Direction, InvoiceCount, DetailsCount, System.IO.Path.GetFileName(currentFile));

                            #endregion

                        }

                        #region Move Files To Archive

                        string Destination = currentFile;
                        Destination = Destination.Replace("FromACCPAC", @"FromACCPAC\Archive\");
                        System.IO.File.Move(currentFile, Destination);

                        #endregion
                    }

                    #endregion
                }
                //else if (command == "CSS2 to CSS1 Integration")
                //{
                //    try
                //    {
                //        var InvoicePreviewData = Billing.InsertCSS2toCC1Data();
                //        HelperClasses.InsertScheduleHistory("Run from UI", "ALL", "CSS2 to CSS1 Integration", false);
                //        return View();
                //    }
                //    catch (Exception ex)
                //    {
                //        log.Error("Error: " + ex);
                //        return View();// return Json("");
                //    }

                //}
            }
            catch
            {
            }
            return View();
        }


        /// <summary>
        /// Description  : To get CABMaster Invoice Preview By MasterID.
        /// Created By   : Shiva
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult GetInvoicePreviewDetailsByCabMasterID(int CABMasterID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var InvoicePreviewData = Billing.GetCABMasterInvoicePreviewByMasterID(CABMasterID);
                return Json(InvoicePreviewData);
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
        /// Description  : To Save Invoice From Css2.
        /// Created By   : Shiva
        /// Created Date : 2 Jan 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult ToSaveInvoiceFromCss2(string InvoiceSelection, string Date, int Month, string ClientCode, string SourceID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int status = Billing.ToSaveInvoiceFromCss2(InvoiceSelection, Date, Month, ClientCode, SourceID);
                HelperClasses.InsertScheduleHistory("Running bill from UI", "For Client " + ClientCode + " , Schedule Code " + InvoiceSelection + " , Billing date on " + Date + " and Month is " + Month + "", "Preparing billing data manually ", false);
                return Json(status);
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
        /// Description  : Insert CSS2 to CSS1 Data
        /// Created By   : Shiva
        /// Created Date : 23 May 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        [HttpPost]
        public JsonResult InsertCSS2toCC1Data()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int status = Billing.InsertCSS2toCC1Data();
                HelperClasses.InsertScheduleHistory("Run from UI", "ALL", "CSS2 to CSS1 Integration", false);
                return Json(status);
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