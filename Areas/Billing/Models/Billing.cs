
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

namespace CSS2.Areas.Billing.Models
{
    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    public class Billing
    {
        private static ILog log = LogManager.GetLogger(typeof(Billing));

        #region Properties

        public int ID { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public bool NeedSecurityDeposit { set; get; }
        public int IsExists { set; get; }
        public string FeeType { get; set; }

        #endregion

        public class FeeSetting
        {
            public int BillTo { set; get; }
            public string BillingPartyCode { set; get; }
            public bool IsClubFee { set; get; }
            public string AccountCode { set; get; }
            public int ID { set; get; }
            public string CompanyName { set; get; }

            public FeeSetting FetchClientFeeSettingInfo(FeeSetting getClientFeeSettingInfo, SafeDataReader dr)
            {
                getClientFeeSettingInfo.BillTo = dr.GetInt32("BillTo");
                getClientFeeSettingInfo.BillingPartyCode = dr.GetString("BillingPartyCode");
                getClientFeeSettingInfo.IsClubFee = dr.GetBoolean("IsClubFee");
                getClientFeeSettingInfo.AccountCode = dr.GetString("AccountCode");
                return getClientFeeSettingInfo;
            }
            public FeeSetting FetCompanyFromBillingData(FeeSetting objBillingData, SafeDataReader dr)
            {
                objBillingData.ID = dr.GetInt32("ID");
                objBillingData.CompanyName = dr.GetString("CompanyName");
                return objBillingData;
            }

        }

        public class FeeMapping
        {
            #region Properties

            public int CFMID { set; get; }
            public string BillingFrequency { set; get; }
            public string BillingMonth { set; get; }
            public bool IsBillArrears { set; get; }
            public string FeeCode { set; get; }
            public string Status { set; get; }
            public decimal SecurityDeposit { set; get; }
            public string SecurityDepositInvoiceNo { set; get; }
            public string FeeDueToNominee { get; set; }


            #endregion

            #region Fetch Method
            public FeeMapping FetchClientFeeMappingInfo(FeeMapping getClientFeeMappingInfo, SafeDataReader dr)
            {
                getClientFeeMappingInfo.CFMID = dr.GetInt32("ID");
                getClientFeeMappingInfo.BillingFrequency = dr.GetString("BillingFrequency");
                getClientFeeMappingInfo.BillingMonth = dr.GetString("BillingMonth");
                getClientFeeMappingInfo.IsBillArrears = dr.GetBoolean("IsBillArrears");
                getClientFeeMappingInfo.Status = dr.GetString("Status");
                getClientFeeMappingInfo.FeeCode = dr.GetString("FeeCode");
                getClientFeeMappingInfo.SecurityDeposit = dr.GetDecimal("SecurityDeposit");
                getClientFeeMappingInfo.SecurityDepositInvoiceNo = dr.GetString("SecurityDepositInvoiceNo");
                getClientFeeMappingInfo.FeeDueToNominee = dr.GetString("FeeDueToNominee");
                return getClientFeeMappingInfo;
            }

            #endregion
        }

        public class FeeSchedule
        {
            public int ID { set; get; }
            //  public int CFMID { set; get; }
            public string ClientCode { set; get; }
            public string FeeCode { set; get; }
            public string FromDate { set; get; }
            public string ToDate { set; get; }
            public decimal Amount { set; get; }
            public string Status { set; get; }
            public string FromDateStatus { set; get; }
            public bool FDateStatus { set; get; }
            public string ToDateStatus { set; get; }
            public bool TDateStatus { set; get; }
            public int SNO { set; get; }
            public int Duration { set; get; }

            public FeeSchedule FetchClientFeeScheduleInfo(FeeSchedule getClientFeeScheduleInfo, SafeDataReader dr)
            {
                getClientFeeScheduleInfo.ID = dr.GetInt32("ID");
                //  getClientFeeScheduleInfo.CFMID = dr.GetInt32("CFMID");
                getClientFeeScheduleInfo.ClientCode = dr.GetString("ClientCode");
                getClientFeeScheduleInfo.FeeCode = dr.GetString("FeeCode");
                getClientFeeScheduleInfo.FromDate = dr.GetString("FromDate");
                getClientFeeScheduleInfo.ToDate = dr.GetString("ToDate");
                getClientFeeScheduleInfo.Amount = dr.GetDecimal("Amount");
                getClientFeeScheduleInfo.Status = dr.GetString("Status");
                getClientFeeScheduleInfo.FromDateStatus = dr.GetString("FromDateStatus");
                getClientFeeScheduleInfo.ToDateStatus = dr.GetString("ToDateStatus");
                if (getClientFeeScheduleInfo.FromDateStatus == "1")
                {
                    getClientFeeScheduleInfo.FDateStatus = true;
                }
                else getClientFeeScheduleInfo.FDateStatus = false;

                if (getClientFeeScheduleInfo.ToDateStatus == "1")
                {
                    getClientFeeScheduleInfo.TDateStatus = true;
                }
                else getClientFeeScheduleInfo.TDateStatus = false;
                return getClientFeeScheduleInfo;
            }

            public FeeSchedule FetchClientScheduleGapInfo(FeeSchedule getClientScheduleGapInfo, SafeDataReader dr)
            {
                getClientScheduleGapInfo.SNO = dr.GetInt32("new_id");
                getClientScheduleGapInfo.FromDate = dr.GetDateTime("start_date").ToString("dd MMM yyyy");
                getClientScheduleGapInfo.ToDate = dr.GetDateTime("end_date").ToString("dd MMM yyyy");
                getClientScheduleGapInfo.Duration = dr.GetInt32("NoofDays");
                return getClientScheduleGapInfo;
            }
        }

        public class CABMaster
        {
            #region Properties

            public int ID { set; get; }
            public int BillToClientCode { set; get; }
            public string BillToSourceID { set; get; }
            public string BillToName { set; get; }
            public string BillToAccpacCode { set; get; }
            public int BillForClientCode { set; get; }
            public string BillForSourceID { set; get; }
            public string BillForName { set; get; }
            public string BillForAccpacCode { set; get; }
            public int IsBillToThirdParty { set; get; }
            public string CreatedDate { set; get; }
            public bool ISAdhoc { set; get; }
            public decimal Amount { set; get; }
            public string InvoiceNumber { set; get; }
            public bool IsInvoice { set; get; }
            public string BillType { get; set; }
            public int HSReceivedStatus { get; set; }
            public string Description { get; set; }
            public string Explaination { get; set; }
            public string HSSentDate { get; set; }
            public string HSReceivedDate { get; set; }
            public int InvoiceErrorsCount { get; set; }
            #endregion


            #region Fetch Billing Data
            internal static CABMaster FetchCABMasterReport(CABMaster CABMaster, SafeDataReader dr)
            {
                CABMaster.ID = dr.GetInt32("ID");
                CABMaster.BillToClientCode = dr.GetInt32("BillToClientCode");
                CABMaster.BillToSourceID = dr.GetString("BillToSourceID");
                CABMaster.BillToName = dr.GetString("BillToName");
                CABMaster.BillToAccpacCode = dr.GetString("BillToAccpacCode");
                CABMaster.BillForClientCode = dr.GetInt32("BillForClientCode");
                CABMaster.BillForSourceID = dr.GetString("BillForSourceID");
                CABMaster.BillForName = dr.GetString("BillForName");
                CABMaster.BillForAccpacCode = dr.GetString("BillForAccpacCode");
                CABMaster.IsBillToThirdParty = dr.GetInt32("IsBillToThirdParty");
                CABMaster.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt"); ;
                CABMaster.ISAdhoc = dr.GetBoolean("ISAdhoc");
                CABMaster.Amount = dr.GetDecimal("Amount");
                CABMaster.InvoiceNumber = dr.GetString("InvoiceNumber");
                CABMaster.BillType = dr.GetString("BillType");
                if (string.IsNullOrEmpty(CABMaster.InvoiceNumber))
                    CABMaster.IsInvoice = false;
                else CABMaster.IsInvoice = true;

                CABMaster.HSReceivedStatus = dr.GetInt32("HSReceivedStatus");
                CABMaster.Description = dr.GetString("Description");
                CABMaster.Explaination = dr.GetString("Explaination");

                return CABMaster;
            }

            internal static CABMaster FetchCABInvoiceErrors(CABMaster CABMaster, SafeDataReader dr)
            {
                CABMaster.ID = dr.GetInt32("ID");
                CABMaster.BillForName = dr.GetString("BillForName");
                CABMaster.BillForAccpacCode = dr.GetString("BillForAccpacCode");
                CABMaster.BillToName = dr.GetString("BillToName");
                CABMaster.BillToAccpacCode = dr.GetString("BillToAccpacCode");
                CABMaster.BillType = dr.GetString("BillType");
                CABMaster.Description = dr.GetString("Description");
                CABMaster.Explaination = dr.GetString("Explaination");
                CABMaster.CreatedDate = dr.GetString("CreatedDate");
                CABMaster.HSSentDate = dr.GetString("HSSentDate");
                CABMaster.HSReceivedDate = dr.GetString("HSReceivedDate");

                return CABMaster;
            }

            #endregion

        }

        public class CABFeeSchedule
        {

            #region Properties

            public int ID { set; get; }
            public string ClientCode { set; get; }
            public string SourceID { set; get; }
            public string FeeCode { set; get; }
            public string FromDate { set; get; }
            public string ToDate { set; get; }
            public decimal Amount { set; get; }
            public bool IsArrear { set; get; }
            public string CreatedDate { set; get; }
            public bool IsClubFee { set; get; }
            public bool IsClubFeeBillGenerated { set; get; }
            public int ClubFeeBillID { set; get; }
            public int CABMasterID { set; get; }

            #endregion

            #region Fetch Method
            internal static CABFeeSchedule FetchCABFeeScheduleReportByMasterID(CABFeeSchedule CABFeeSchedule, SafeDataReader dr)
            {
                CABFeeSchedule.ID = dr.GetInt32("ID");
                CABFeeSchedule.ClientCode = dr.GetString("ClientCode");
                CABFeeSchedule.SourceID = dr.GetString("SourceID");
                CABFeeSchedule.FeeCode = dr.GetString("FeeCode");
                CABFeeSchedule.FromDate = dr.GetDateTime("FromDate").ToString("dd/MMM/yyyy");
                CABFeeSchedule.ToDate = dr.GetDateTime("ToDate").ToString("dd/MMM/yyyy");
                CABFeeSchedule.Amount = dr.GetDecimal("Amount");
                CABFeeSchedule.IsArrear = dr.GetBoolean("IsArrear");
                CABFeeSchedule.CreatedDate = dr.GetString("CreatedDate");
                CABFeeSchedule.IsClubFee = dr.GetBoolean("IsClubFee");
                CABFeeSchedule.IsClubFeeBillGenerated = dr.GetBoolean("IsClubFeeBillGenerated");
                CABFeeSchedule.ClubFeeBillID = dr.GetInt32("ClubFeeBillID");
                CABFeeSchedule.CABMasterID = dr.GetInt32("CABMasterID");
                return CABFeeSchedule;
            }

            #endregion

        }

        public class CABFeeScheduleDetails
        {
            #region Properties


            public int ID { set; get; }
            public string ClientCode { set; get; }
            public string SourceID { set; get; }
            public string FeeCode { set; get; }
            public string BilledFrom { set; get; }
            public string BilledTo { set; get; }
            public decimal Amount { set; get; }
            public decimal Prorate { set; get; }
            public decimal BillableAmount { set; get; }
            public bool IsArrear { set; get; }
            public string CreatedDate { set; get; }
            public int CABFeeScheduleID { set; get; }


            #endregion

            #region Fetch Method

            internal static CABFeeScheduleDetails FetchCABFeeScheduleDetailsReportByCABFeeScheduleID(CABFeeScheduleDetails CABFeeScheduleDetails, SafeDataReader dr)
            {
                CABFeeScheduleDetails.ID = dr.GetInt32("ID");
                CABFeeScheduleDetails.ClientCode = dr.GetString("ClientCode");
                CABFeeScheduleDetails.SourceID = dr.GetString("SourceID");
                CABFeeScheduleDetails.FeeCode = dr.GetString("FeeCode");
                CABFeeScheduleDetails.BilledFrom = dr.GetDateTime("BilledFrom").ToString("dd/MMM/yyyy"); ;
                CABFeeScheduleDetails.BilledTo = dr.GetDateTime("BilledTo").ToString("dd/MMM/yyyy"); ;
                CABFeeScheduleDetails.Amount = dr.GetDecimal("Amount");
                CABFeeScheduleDetails.Prorate = dr.GetDecimal("Prorate");
                CABFeeScheduleDetails.BillableAmount = dr.GetDecimal("BillableAmount");
                CABFeeScheduleDetails.IsArrear = dr.GetBoolean("IsArrear");
                CABFeeScheduleDetails.CreatedDate = dr.GetString("CreatedDate");
                CABFeeScheduleDetails.CABFeeScheduleID = dr.GetInt32("CABFeeScheduleID");
                return CABFeeScheduleDetails;
            }


            #endregion
        }

        public class CABMasterInvoicePreview
        {

            #region Properties

            public decimal Amount { set; get; }
            public string ACCPACCode { set; get; }
            public string Description { set; get; }
            public string Name { set; get; }
            public string AddressLine1 { set; get; }
            public string AddressLine2 { set; get; }
            public string AddressLine3 { set; get; }

            #endregion

            #region FetchMethod
            public CABMasterInvoicePreview FetchCABMasterInvoicePreview(CABMasterInvoicePreview getCABMasterInvoicePreview, SafeDataReader dr)
            {
                getCABMasterInvoicePreview.Amount = dr.GetDecimal("Amount");
                getCABMasterInvoicePreview.ACCPACCode = dr.GetString("ACCPACCode");
                getCABMasterInvoicePreview.Description = dr.GetString("Description");
                return getCABMasterInvoicePreview;
            }
            public CABMasterInvoicePreview FetchCABMasterInvoicePreviewAddresses(CABMasterInvoicePreview getCABMasterInvoicePreview, SafeDataReader dr)
            {
                getCABMasterInvoicePreview.Name = dr.GetString("Name");
                getCABMasterInvoicePreview.AddressLine1 = dr.GetString("AddressLine1");
                getCABMasterInvoicePreview.AddressLine2 = dr.GetString("AddressLine2");
                getCABMasterInvoicePreview.AddressLine3 = dr.GetString("AddressLine3");
                getCABMasterInvoicePreview.Amount = dr.GetDecimal("Amount");
                return getCABMasterInvoicePreview;
            }

            #endregion

        }





        #region Fetch Billing Data


        private Billing FetchBillingFeeInfo(Billing getBillingFrequencyAndMonth, SafeDataReader dr)
        {
            getBillingFrequencyAndMonth.ID = dr.GetInt32("ID");
            getBillingFrequencyAndMonth.Code = dr.GetString("Code");
            getBillingFrequencyAndMonth.Name = dr.GetString("Name");
            getBillingFrequencyAndMonth.NeedSecurityDeposit = dr.GetBoolean("NeedSecurityDeposit");
            return getBillingFrequencyAndMonth;
        }

        private Billing FetchBillingFeeInfoClient(Billing getBillingFrequencyAndMonth, SafeDataReader dr)
        {
            getBillingFrequencyAndMonth.ID = dr.GetInt32("ID");
            getBillingFrequencyAndMonth.Code = dr.GetString("Code");
            getBillingFrequencyAndMonth.Name = dr.GetString("Name");
            getBillingFrequencyAndMonth.NeedSecurityDeposit = dr.GetBoolean("NeedSecurityDeposit");
            getBillingFrequencyAndMonth.IsExists = dr.GetInt32("IsExists");
            getBillingFrequencyAndMonth.FeeType = dr.GetString("FeeType");
            return getBillingFrequencyAndMonth;
        }

        private Billing FetchBillingFrequencyOrMonthOrFeeInfo(Billing getBillingFrequencyAndMonth, SafeDataReader dr)
        {
            getBillingFrequencyAndMonth.ID = dr.GetInt32("ID");
            getBillingFrequencyAndMonth.Code = dr.GetString("Code");
            getBillingFrequencyAndMonth.Name = dr.GetString("Name");
            return getBillingFrequencyAndMonth;
        }

        #endregion

        #region Billing Database methods
        /// <summary>
        /// Description  : Get the MSchedule Information
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static BillingInfo GetMSchedule()
        {
            var GetMSchedule = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMSchedule");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var MSchedule = new Billing();
                    MSchedule.FetchBillingFrequencyOrMonthOrFeeInfo(MSchedule, safe);
                    GetMSchedule.BillingList.Add(MSchedule);
                }
                return GetMSchedule;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetMSchedule;
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
        public static BillingInfo GetMMonth()
        {
            var GetMMonth = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetMonth");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var MMonth = new Billing();
                    MMonth.FetchBillingFrequencyOrMonthOrFeeInfo(MMonth, safe);
                    GetMMonth.BillingList.Add(MMonth);
                }
                return GetMMonth;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetMMonth;
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
        public static BillingInfo GetMFee()
        {
            var GetMFee = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMFee");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var MFee = new Billing();
                    MFee.FetchBillingFeeInfo(MFee, safe);
                    GetMFee.BillingList.Add(MFee);
                }
                return GetMFee;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetMFee;
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
        public static BillingInfo GetMFEEforClient(int ClientCode)
        {
            var GetMFee = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMFeeForClient", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var MFee = new Billing();
                    MFee.FetchBillingFeeInfoClient(MFee, safe);
                    GetMFee.BillingList.Add(MFee);
                }
                return GetMFee;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetMFee;
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
        public static int InsertOrUpdateClientFeeSettings(int ClientCode, string SourceID, int BillTo, string BillingParty, bool IsClubFee, string AccountCode, bool Status, int CreatedBy)
        {
            int returnedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@BillTo", BillTo);
                sqlParams[2] = new SqlParameter("@SourceID", SourceID);
                sqlParams[3] = new SqlParameter("@BillingParty", BillingParty);
                sqlParams[4] = new SqlParameter("@IsClubFee", IsClubFee);
                sqlParams[5] = new SqlParameter("@AccountCode", AccountCode);
                sqlParams[6] = new SqlParameter("@Status", Status);
                sqlParams[7] = new SqlParameter("@CreatedBy", CreatedBy);
                returnedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPInsertOrUpdateClentFeeSettings", sqlParams);
                return returnedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnedStatus;
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
        public static int InsertOrUpdateClientFeeMapping(int ClientCode, string SourceID, string FeeCode, string BillingFrequency, string BillingMonth, bool IsBillArrears, bool Status, string SecurityDeposit, string SecurityDepositInvoiceNo, int CreatedBy, string FeeDueToNominee)
        {
            int returnedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", SourceID);
                sqlParams[2] = new SqlParameter("@FeeCode", FeeCode);
                sqlParams[3] = new SqlParameter("@BillingFrequency", BillingFrequency);
                sqlParams[4] = new SqlParameter("@BillingMonth", BillingMonth);
                sqlParams[5] = new SqlParameter("@IsBillArrears", IsBillArrears);
                sqlParams[6] = new SqlParameter("@Status", Status);
                sqlParams[7] = new SqlParameter("@SecurityDeposit", SecurityDeposit);
                sqlParams[8] = new SqlParameter("@SecurityDepositInvoiceNo", SecurityDepositInvoiceNo);
                sqlParams[9] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[10] = new SqlParameter("@FeeDueToNominee", FeeDueToNominee);
                returnedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertOrUpadateClientFeeMapping", sqlParams);
                return returnedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnedStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Insert Or Update the ClientFeeSchedule
        /// Created By   : Shiva
        /// Created Date : 3rd July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int InsertOrUpdateClientFeeSchedule(int ID, int CFMID, int ClientCode, string SourceID, string FeeCode, string FromDate, string ToDate, decimal Amount, bool Status, int CreatedBy)
        {
            int returnedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@ID", ID);
                // sqlParams[1] = new SqlParameter("@CFMID", CFMID);
                sqlParams[2] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[3] = new SqlParameter("@FeeCode", FeeCode);
                sqlParams[4] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[5] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));
                sqlParams[6] = new SqlParameter("@Amount", Amount);
                sqlParams[7] = new SqlParameter("@Status", Status);
                sqlParams[8] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[9] = new SqlParameter("@SourceID", SourceID);

                returnedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertOrUpdateClientFeeSchedule", sqlParams);
                return returnedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnedStatus;
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
        public static BillingInfo GetFeeSettingAndMappingsByClientCode(int ClientCode, string SourceID)
        {
            var GetFeeSettingAndMappingInfo = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", SourceID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetFeeSettingAndMappingsByClientCode", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var feeSetting = new FeeSetting();
                    feeSetting.FetchClientFeeSettingInfo(feeSetting, safe);
                    GetFeeSettingAndMappingInfo.ClientFeeSettingList.Add(feeSetting);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var feeMapping = new FeeMapping();
                    feeMapping.FetchClientFeeMappingInfo(feeMapping, safe);
                    GetFeeSettingAndMappingInfo.ClientFeeMappingList.Add(feeMapping);
                }
                return GetFeeSettingAndMappingInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetFeeSettingAndMappingInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get the ClientFeeMapping Information by FeeCode
        /// Created By   : Shiva
        /// Created Date : 2nd July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static BillingInfo GetFeeMappingsByFeeAndClientCode(string FeeCode, int ClientCode, string SourceID)
        {
            var GetFeeMappingInfo = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", SourceID);
                sqlParams[2] = new SqlParameter("@FeeCode", FeeCode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetFeeMappingsByFeeAndClientCode", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var feeMapping = new FeeMapping();
                    feeMapping.FetchClientFeeMappingInfo(feeMapping, safe);
                    GetFeeMappingInfo.ClientFeeMappingList.Add(feeMapping);
                }
                return GetFeeMappingInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetFeeMappingInfo;
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
        public static int DeleteClientScheduleByID(int ID)
        {
            int deleteStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);
                deleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteClientScheduleByID", sqlParams);
                return deleteStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return deleteStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Delete All Settings by Client
        /// Created By   : Pavan
        /// Created Date : 08 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int DeleteAllSettingsByClient(int ClientCode, string SourceID)
        {
            int deleteStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", SourceID);
                deleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteAllBillingSettingsByClient", sqlParams);
                return deleteStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return deleteStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Delete All Settings by Client
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int DeleteFeeSettings(int ClientCode, string SourceID, string FeeCode)
        {
            int deleteStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", SourceID);
                sqlParams[2] = new SqlParameter("@FeeCode", FeeCode);
                deleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteFeeSettings", sqlParams);
                return deleteStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return deleteStatus;
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
        public static BillingInfo GetClientFeeScheduleByClientAndFeeCode(string FeeCode, int ClientCode, string SourceID)
        {
            var GetClientFeeScheduleByClientAndFeeCodeInfo = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@FeeCode", FeeCode);
                sqlParams[1] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[2] = new SqlParameter("@SourceID", SourceID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetClientFeeScheduleByFeeCodeAndClientCode", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var feeSchedule = new FeeSchedule();
                    feeSchedule.FetchClientFeeScheduleInfo(feeSchedule, safe);
                    GetClientFeeScheduleByClientAndFeeCodeInfo.ClientFeeScheduleList.Add(feeSchedule);
                }
                return GetClientFeeScheduleByClientAndFeeCodeInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetClientFeeScheduleByClientAndFeeCodeInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Gap analysis by FeeCode and ClientCode.
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static BillingInfo GetClientScheduleGapByFeeAndClientCode(string FeeCode, int ClientCode, string SourceID)
        {
            var GetGapInfo = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                sqlParams[1] = new SqlParameter("@SourceID", @SourceID);
                sqlParams[2] = new SqlParameter("@FeeCode", FeeCode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetClientScheduleMapGapAnalysisByCodes_Club", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var GapInfo = new FeeSchedule();
                    GapInfo.FetchClientScheduleGapInfo(GapInfo, safe);
                    GetGapInfo.ClientFeeScheduleList.Add(GapInfo);
                }
                return GetGapInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetGapInfo;
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
        public static BillingInfo GetCompanyFromBillingThirdParty()
        {
            var billingDataInfo = new BillingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCompanyFromBillingThirdParty");
                var safe = new SafeDataReader(reader);

                while (reader.Read())
                {
                    var billing = new FeeSetting();
                    billing.FetCompanyFromBillingData(billing, safe);
                    billingDataInfo.ClientFeeSettingList.Add(billing);
                }
                return billingDataInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return billingDataInfo;
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
        static internal BillingInfo GetAllBillingDetailsByCompanyAndFee(string CompanyID, string CompanySource, string FeeCode, string BillFromDate, string BillToDate, int StartPage, int ResultPerPage, int BillType)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var CABMasterData = new BillingInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@ClientCode", CompanyID);
                sqlParams[1] = new SqlParameter("@Source", CompanySource);
                sqlParams[2] = new SqlParameter("@FeeCode", FeeCode);
                sqlParams[3] = new SqlParameter("@BillFromDate", HelperClasses.ConvertDateFormat(BillFromDate));
                sqlParams[4] = new SqlParameter("@BillToDate", HelperClasses.ConvertDateFormat(BillToDate));
                sqlParams[5] = new SqlParameter("@BillType", BillType);
                sqlParams[6] = new SqlParameter("@StartPage", StartPage);
                sqlParams[7] = new SqlParameter("@ResultPerPage", ResultPerPage);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCABMasterReport", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var CabMaster = new CABMaster();
                    CABMaster.FetchCABMasterReport(CabMaster, safe);
                    CABMasterData.CABMasterList.Add(CabMaster);
                    CABMasterData.BillingCount = Convert.ToInt32(reader["BillingCount"]);
                }
                return CABMasterData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return CABMasterData;
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
        static internal BillingInfo GetCABFeeScheduleReportByMasterID(int CABMasterID)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var CABFeeScheduleData = new BillingInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CABMasterID", CABMasterID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCABFeeScheduleReportByMasterID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var CabMaster = new CABFeeSchedule();
                    CABFeeSchedule.FetchCABFeeScheduleReportByMasterID(CabMaster, safe);
                    CABFeeScheduleData.CABFeeScheduleList.Add(CabMaster);
                }
                return CABFeeScheduleData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return CABFeeScheduleData;
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
        static internal BillingInfo GetCABFeeScheduleDetailsReportByCABFeeScheduleID(int CABFeeScheduleID)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var CABFeeScheduleDetailsData = new BillingInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CABFeeScheduleID", CABFeeScheduleID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCABFeeScheduleDetailsReportByCABFeeScheduleID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var GetCABFeeScheduleDetails = new CABFeeScheduleDetails();
                    CABFeeScheduleDetails.FetchCABFeeScheduleDetailsReportByCABFeeScheduleID(GetCABFeeScheduleDetails, safe);
                    CABFeeScheduleDetailsData.CABFeeScheduleDetailsList.Add(GetCABFeeScheduleDetails);
                }
                return CABFeeScheduleDetailsData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return CABFeeScheduleDetailsData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get Download InVoice From CSS2.
        /// Created By   : Pavan
        /// Created Date : 28 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static DataSet GetCABPrepareInvoiceFromCSS2()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCABPrepareInvoiceFromCSS2");
                return ds;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ds;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get CABMaster Invoice Preview By MasterID.
        /// Created By   : Shiva
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        static internal BillingInfo GetCABMasterInvoicePreviewByMasterID(int CABMasterID)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var CABMasterInvoicePreview = new BillingInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CabMasterID", CABMasterID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCABMasterInvoicePreview", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var InvoicePrevie = new CABMasterInvoicePreview();
                    InvoicePrevie.FetchCABMasterInvoicePreviewAddresses(InvoicePrevie, safe);
                    CABMasterInvoicePreview.CABMasterInvoicePreviewList1.Add(InvoicePrevie);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var InvoicePrevie = new CABMasterInvoicePreview();
                    InvoicePrevie.FetchCABMasterInvoicePreview(InvoicePrevie, safe);
                    CABMasterInvoicePreview.CABMasterInvoicePreviewList2.Add(InvoicePrevie);
                }
                //reader.NextResult();
                //while (reader.Read())
                //{
                //    var InvoicePrevie = new CABMasterInvoicePreview();
                //    InvoicePrevie.FetchCABMasterInvoicePreview(InvoicePrevie, safe);
                //    CABMasterInvoicePreview.CABMasterInvoicePreviewList3.Add(InvoicePrevie);
                //}

                return CABMasterInvoicePreview;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return CABMasterInvoicePreview;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Update Sent Status For Invoice From CSS2
        /// Created By   : Pavan
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static void CABUpdateSentStatus(string CABMasterIDs)
        {
            int UpdateStatus = -2;


            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CABMasterIDs", CABMasterIDs);
            UpdateStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCABUpdateSentStatus", sqlParams);


        }

        /// <summary>
        /// Description  : To Update Receive Status For Invoice From ACCPAC
        /// Created By   : Pavan
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int CABUpdateReceiveStatus(DataTable dtINVOICE_FROM_CSS2, DataTable dtINVOICE_DETAILS_FROM_CSS2)
        {
            int UpdateStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@dtINVOICE_FROM_ACCPAC", dtINVOICE_FROM_CSS2);
                sqlParams[0].SqlDbType = SqlDbType.Structured;
                sqlParams[1] = new SqlParameter("@dtINVOICE_DETAILS_FROM_ACCPAC", dtINVOICE_DETAILS_FROM_CSS2);
                sqlParams[1].SqlDbType = SqlDbType.Structured;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCABUpdateReceiveStatus", sqlParams);
                return UpdateStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return UpdateStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To insert Invoice details
        /// Created By   : Pavan
        /// Created Date : 31 October 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int InsertCABInvoiceLog(string Direction, int InvoiceCount, int DetailsCount, string FileName)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@Direction", Direction);
                sqlParams[1] = new SqlParameter("@InvoiceCount", InvoiceCount);
                sqlParams[2] = new SqlParameter("@DetailsCount", DetailsCount);
                sqlParams[3] = new SqlParameter("@FileName", FileName);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertCABInvoiceLog", sqlParams);
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return result;
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
        public static int ToSaveInvoiceFromCss2(string InvoiceSelection, string Date, int Month, string ClientCode, string SourceID)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (InvoiceSelection == string.Empty)
                {
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@ClientCode", ClientCode);
                    sqlParams[1] = new SqlParameter("@SourceID", SourceID);
                    result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCABMasterAdhoc", sqlParams);
                }

                else
                {
                    SqlParameter[] sqlParams = new SqlParameter[5];
                    sqlParams[0] = new SqlParameter("@SCCode", InvoiceSelection);
                    sqlParams[1] = new SqlParameter("@ONDate", HelperClasses.ConvertDateFormat(Date));
                    sqlParams[2] = new SqlParameter("@Month", Month);
                    sqlParams[3] = new SqlParameter("@ClientCode", ClientCode);
                    sqlParams[4] = new SqlParameter("@SourceID", SourceID);


                    result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCABMaster", sqlParams);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return result;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }




        /// <summary>
        /// Description  : To Update Sent Status For Invoice From CSS2
        /// Created By   : Pavan
        /// Created Date : 29 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static int InsertCSS2toCC1Data()
        {

            try
            {
                var UpdateStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCSS1CSS2Integration");
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return 0;
            }

        }

        #region CabInvoice Error

        /// <summary>
        /// Description  : To get All Billing details by Company AND Fee.
        /// Created By   : Shiva
        /// Created Date : 7 Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        static internal List<CABMaster> GetAllCABInvoiceErrors(string ClientId, string SourceID, int startpage, int rowsperpage, string FromDate, string ToDate)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            var CABInvoiceErrorsData = new List<CABMaster>();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@startPage", startpage);
                sqlParams[1] = new SqlParameter("@resultPerPage", rowsperpage);
                sqlParams[2] = new SqlParameter("@ClientId", ClientId);
                sqlParams[3] = new SqlParameter("@SourceID", SourceID);
                sqlParams[4] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[5] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetCABInvoiceErrorList]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var CabMaster = new CABMaster();
                    CABMaster.FetchCABInvoiceErrors(CabMaster, safe);
                    CabMaster.InvoiceErrorsCount = Convert.ToInt32(reader["InvoiceErrorsCount"]);
                    CABInvoiceErrorsData.Add(CabMaster);
                }
                return CABInvoiceErrorsData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return CABInvoiceErrorsData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get All Billing details by Company AND Fee.
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        static internal DataSet GetCABInvoiceErrorByCABMasterID(int CABMasterID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CABMasterID", CABMasterID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCABInvoiceErrorDetaisById", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return ds;
        }

        /// <summary>
        /// Description  : To Delete Invoice Error By CABMasterID
        /// Created By   : Pavan
        /// Created Date : 16 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        static internal int DeleteCABInvoiceErrorByCABMasterID(int CABMasterID)
        {
            int DeleteStatus = 0;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CABMasterID", CABMasterID);
                DeleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCABDeleteInvoice", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return DeleteStatus;
        }

        #endregion








        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class BillingInfo
        {
            public List<Billing> BillingList { get; set; }
            public List<FeeSetting> ClientFeeSettingList { get; set; }
            public List<FeeMapping> ClientFeeMappingList { get; set; }
            public List<FeeSchedule> ClientFeeScheduleList { get; set; }
            public List<CABMaster> CABMasterList { get; set; }
            public List<CABFeeSchedule> CABFeeScheduleList { set; get; }
            public List<CABFeeScheduleDetails> CABFeeScheduleDetailsList { set; get; }
            public List<CABMasterInvoicePreview> CABMasterInvoicePreviewList1 { set; get; }
            public List<CABMasterInvoicePreview> CABMasterInvoicePreviewList2 { set; get; }
            //  public List<CABMasterInvoicePreview> CABMasterInvoicePreviewList3 { set; get; }

            public int BillingCount { get; set; }

            public BillingInfo()
            {
                BillingList = new List<Billing>();
                ClientFeeSettingList = new List<FeeSetting>();
                ClientFeeMappingList = new List<FeeMapping>();
                ClientFeeScheduleList = new List<FeeSchedule>();
                CABMasterList = new List<CABMaster>();
                CABFeeScheduleList = new List<CABFeeSchedule>();
                CABFeeScheduleDetailsList = new List<CABFeeScheduleDetails>();
                CABMasterInvoicePreviewList1 = new List<CABMasterInvoicePreview>();
                CABMasterInvoicePreviewList2 = new List<CABMasterInvoicePreview>();
                // CABMasterInvoicePreviewList3 = new List<CABMasterInvoicePreview>();
            }
        }
        #endregion


    }
}

