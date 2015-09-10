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

namespace CSS2.Areas.WO.Models
{
    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    public class AGMDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(AGMDetails));

        #region Properties
        public int ID { get; set; }
        public string WOIDForWoAGM { get; set; }
        public string DateOfAGM { get; set; }
        public string TimeOfAGM { get; set; }
        public string FinancialYearEnd { get; set; }
        public string DateOfFinancialStatement { get; set; }
        public bool IsAuditor { get; set; }
        public int Auditors { get; set; }
        public int ShareHoldingStructure { get; set; }
        public bool IsDirectorsFeeAmount { get; set; }
        public string DirectorFeeAmount { get; set; }
        public int DirectorCurrency { get; set; }
        public bool IsRemunerationAmount { get; set; }
        public string RemunerationAmount { get; set; }
        public int RemunerationCurrency { get; set; }
        public string MeetingNotice { get; set; }
        public string MeetingNoticeSource { get; set; }
        public string MeetingMinutes { get; set; }
        public string OtherMeetingMinutes { set; get; }
        public string MeetingMinutesSource { get; set; }
        public string DesignationofPersonSigningAGM { get; set; }
        public string S197Certificate { get; set; }
        public string S197CertificateSource { get; set; }
        public bool IsS161toIssueShares { get; set; }
        public string S161NoticeofResolution { get; set; }
        public string S161NoticeofResolutionSource { get; set; }
        public string DividentVoucher { get; set; }
        public string DividentVoucherSource { get; set; }
        public bool IsDirectorsdueforRetirement { get; set; }
        public bool IsDividend { get; set; }
        public string Dividendpershare { get; set; }
        public int DividendCurrency { get; set; }
        public string TotalNetAmountofDividend { get; set; }
        public string TotalNoOfShares { get; set; }
        public bool ROPlaceOfAGM { set; get; }
        public bool ApprovalFS { set; get; }

        public string MeetingAddressLine1 { get; set; }
        public string MeetingAddressLine2 { get; set; }
        public string MeetingAddressLine3 { get; set; }
        public int MeetingAddressCountry { get; set; }
        public string MeetingAddressPostalCode { get; set; }

        #endregion

        #region Fetch Data

        private AGMDetails FetchWOAGMDetails(AGMDetails AGMDetails, SafeDataReader dr)
        {
            AGMDetails.ID = dr.GetInt32("ID");
            AGMDetails.WOIDForWoAGM = dr.GetString("WOID");
            AGMDetails.ROPlaceOfAGM = dr.GetBoolean("ROPlaceofAGM");
            AGMDetails.DateOfAGM = dr.GetDateTime("DateofAGM").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("DateofAGM").ToString("dd/MM/yyyy");
            AGMDetails.TimeOfAGM = dr.GetString("TimeofAGM");
            AGMDetails.FinancialYearEnd = dr.GetDateTime("FinancialYearEnd").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("FinancialYearEnd").ToString("dd/MM/yyyy");
            AGMDetails.DateOfFinancialStatement = dr.GetDateTime("DateofFinancialStatement").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("DateofFinancialStatement").ToString("dd/MM/yyyy");
            AGMDetails.IsAuditor = dr.GetBoolean("IsAuditor");
            AGMDetails.Auditors = dr.GetInt32("Auditors");
            AGMDetails.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            AGMDetails.IsDirectorsFeeAmount = dr.GetBoolean("IsDirectorsFeeAmount");
            AGMDetails.DirectorFeeAmount = dr.GetString("DirectorFeeAmount");
            AGMDetails.DirectorCurrency = dr.GetInt32("DirectorCurrency");
            AGMDetails.IsRemunerationAmount = dr.GetBoolean("IsRemunerationAmount");
            AGMDetails.RemunerationAmount = dr.GetString("RemunerationAmount");
            AGMDetails.RemunerationCurrency = dr.GetInt32("RemunerationCurrency");
            AGMDetails.MeetingNotice = dr.GetString("MeetingNotice");
            AGMDetails.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            AGMDetails.MeetingMinutes = dr.GetString("MeetingMinutes");
            AGMDetails.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            AGMDetails.OtherMeetingMinutes = dr.GetString("OtherMeetingMinutes");
            AGMDetails.DesignationofPersonSigningAGM = dr.GetString("DesignationofPersonSigningAGM");
            AGMDetails.S197Certificate = dr.GetString("S197Certificate");
            AGMDetails.S197CertificateSource = dr.GetString("S197CertificateSource");
            AGMDetails.IsS161toIssueShares = dr.GetBoolean("IsS161toIssueShares");
            AGMDetails.S161NoticeofResolution = dr.GetString("S161NoticeofResolution");
            AGMDetails.S161NoticeofResolutionSource = dr.GetString("S161NoticeofResolutionSource");
            AGMDetails.DividentVoucher = dr.GetString("DividentVoucher");
            AGMDetails.DividentVoucherSource = dr.GetString("DividentVoucherSource");
            AGMDetails.IsDirectorsdueforRetirement = dr.GetBoolean("IsDirectorsdueforRetirement");
            AGMDetails.IsDividend = dr.GetBoolean("IsDividend");
            AGMDetails.Dividendpershare = dr.GetString("Dividendpershare");
            AGMDetails.DividendCurrency = dr.GetInt32("DividendCurrency");
            AGMDetails.TotalNetAmountofDividend = dr.GetString("TotalNetAmountofDividend");
            AGMDetails.TotalNoOfShares = dr.GetString("TotalNoOfShares");
            AGMDetails.ApprovalFS = dr.GetBoolean("IsApprovalFS");
            AGMDetails.MeetingAddressLine1 = dr.GetString("MeetingAddressLine1");
            AGMDetails.MeetingAddressLine2 = dr.GetString("MeetingAddressLine2");
            AGMDetails.MeetingAddressLine3 = dr.GetString("MeetingAddressLine3");
            AGMDetails.MeetingAddressCountry = dr.GetInt32("MeetingAddressCountry");
            AGMDetails.MeetingAddressPostalCode = dr.GetString("MeetingAddressPostalCode");

            return AGMDetails;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Insert Work Order AGM Details.
        /// Created By   : Pavan
        /// Created Date : 25 July 2014
        /// Modified By  : Shiva
        /// Modified Date: 26 Aug 2014
        /// </summary>
        /// <returns></returns>
        public static int InsertWOAGMDetails(AGMDetails AGMDetails, int CreatedBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[37];
                sqlParams[0] = new SqlParameter("@WOID", AGMDetails.WOIDForWoAGM);
                sqlParams[1] = new SqlParameter("@DateofAGM", HelperClasses.ConvertDateFormat(AGMDetails.DateOfAGM));
                sqlParams[2] = new SqlParameter("@TimeofAGM", AGMDetails.TimeOfAGM);
                sqlParams[3] = new SqlParameter("@FinancialYearEnd", HelperClasses.ConvertDateFormat(AGMDetails.FinancialYearEnd));
                sqlParams[4] = new SqlParameter("@DateofFinancialStatement", HelperClasses.ConvertDateFormat(AGMDetails.DateOfFinancialStatement));
                sqlParams[5] = new SqlParameter("@IsAuditor", AGMDetails.IsAuditor);
                sqlParams[6] = new SqlParameter("@Auditors", AGMDetails.Auditors);
                sqlParams[7] = new SqlParameter("@ShareHoldingStructure", AGMDetails.ShareHoldingStructure);
                sqlParams[8] = new SqlParameter("@IsDirectorsFeeAmount", AGMDetails.IsDirectorsFeeAmount);
                sqlParams[9] = new SqlParameter("@DirectorFeeAmount", AGMDetails.DirectorFeeAmount);
                sqlParams[10] = new SqlParameter("@DirectorCurrency", AGMDetails.DirectorCurrency);
                sqlParams[11] = new SqlParameter("@IsRemunerationAmount", AGMDetails.IsRemunerationAmount);
                sqlParams[12] = new SqlParameter("@RemunerationAmount", AGMDetails.RemunerationAmount);
                sqlParams[13] = new SqlParameter("@RemunerationCurrency", AGMDetails.RemunerationCurrency);
                sqlParams[14] = new SqlParameter("@MeetingNotice", Convert.ToInt32(AGMDetails.MeetingNotice));
                sqlParams[15] = new SqlParameter("@MeetingNoticeSource", AGMDetails.MeetingNoticeSource);
                sqlParams[16] = new SqlParameter("@MeetingMinutes", Convert.ToInt32(AGMDetails.MeetingMinutes));
                sqlParams[17] = new SqlParameter("@MeetingMinutesSource", AGMDetails.MeetingMinutesSource);
                sqlParams[18] = new SqlParameter("@DesignationofPersonSigningAGM", AGMDetails.DesignationofPersonSigningAGM);
                sqlParams[19] = new SqlParameter("@S197Certificate", Convert.ToInt32(AGMDetails.S197Certificate));
                sqlParams[20] = new SqlParameter("@S197CertificateSource", AGMDetails.S197CertificateSource);
                sqlParams[21] = new SqlParameter("@IsS161toIssueShares", AGMDetails.IsS161toIssueShares);
                sqlParams[22] = new SqlParameter("@S161NoticeofResolution", Convert.ToInt32(AGMDetails.S161NoticeofResolution));
                sqlParams[23] = new SqlParameter("@S161NoticeofResolutionSource", AGMDetails.S161NoticeofResolutionSource);
                sqlParams[24] = new SqlParameter("@DividentVoucher", Convert.ToInt32(AGMDetails.DividentVoucher));
                sqlParams[25] = new SqlParameter("@DividentVoucherSource", AGMDetails.DividentVoucherSource);
                sqlParams[26] = new SqlParameter("@IsDirectorsdueforRetirement", AGMDetails.IsDirectorsdueforRetirement);
                sqlParams[27] = new SqlParameter("@IsDividend", AGMDetails.IsDividend);
                sqlParams[28] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[29] = new SqlParameter("@OtherMeetingMinutes", AGMDetails.OtherMeetingMinutes);
                sqlParams[30] = new SqlParameter("@ROPlaceofAGM", AGMDetails.ROPlaceOfAGM);
                sqlParams[31] = new SqlParameter("@IsApprovalFS", AGMDetails.ApprovalFS);
                sqlParams[32] = new SqlParameter("@MeetingAddressLine1", AGMDetails.MeetingAddressLine1);
                sqlParams[33] = new SqlParameter("@MeetingAddressLine2", AGMDetails.MeetingAddressLine2);
                sqlParams[34] = new SqlParameter("@MeetingAddressLine3", AGMDetails.MeetingAddressLine3);
                sqlParams[35] = new SqlParameter("@MeetingAddressCountry", AGMDetails.MeetingAddressCountry);
                sqlParams[36] = new SqlParameter("@MeetingAddressPostalCode", AGMDetails.MeetingAddressPostalCode);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWOAGMDetails]", sqlParams);
                return output;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return output;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOAGMDetails by WOID
        /// </summary>
        /// <returns></returns>
        public static AGMDetails GetWOAGMDetailsByWOID(int WOID)
        {
            var Details = new AGMDetails();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOAGMDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    Details.FetchWOAGMDetails(Details, safe);
                }
                return Details;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return Details;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion
    }
}