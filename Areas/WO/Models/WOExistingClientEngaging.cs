
#region Document Header
//Created By       : Shiva 
//Created Date     : 3rd Sept 2014
//Description      : WO Existing Client Engaging logic--------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
#endregion

#region usings
using CSS2.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
#endregion

namespace CSS2.Areas.WO.Models
{
    public class WOExistingClientEngaging
    {
        private static ILog log = LogManager.GetLogger(typeof(WOExistingClientEngaging));

        #region Properties

        public int WOID { set; get; }
        public int Currency { set; get; }
        public int ClassOfShare { set; get; }
        public string NewAllottedShares { set; get; }
        public string EachShare { set; get; }
        public string AmountPaidToEachShare { set; get; }
        public string TotalConsideration { set; get; }
        public string NoOfIssuedShares { set; get; }
        public string IssuedCapital { set; get; }
        public string ResultantPaidupCapital { set; get; }
        public string MeetingNoticeSource { set; get; }
        public int MeetingNotice { set; get; }
        public string MeetingMinutesSource { set; get; }
        public int MeetingMinutes { set; get; }
        public string OthersMeetingMinutes { set; get; }
        public string Designation { set; get; }
        public string NoticeResolutionSource { set; get; }
        public int NoticeResolution { set; get; }
        public string F24F25Source { set; get; }
        public int F24F25ID { set; get; }
        public int ShareHoldingStructure { set; get; }

        public bool IsROPlaceOfMeeting { set; get; }
        public string MAddressLine1 { set; get; }
        public string MAddressLine2 { set; get; }
        public string MAddressLine3 { set; get; }
        public int MAddressCountry { set; get; }
        public string MAddressPostalCode { set; get; }

        public int SavedBy { set; get; }
        #endregion

        #region FetchMethods

        private WOExistingClientEngaging FetchECEDetails(WOExistingClientEngaging ECEData, SafeDataReader dr)
        {

            ECEData.Currency = dr.GetInt32("Currency");
            ECEData.ClassOfShare = dr.GetInt32("ClassOfShare");
            ECEData.NewAllottedShares = dr.GetString("NewAllottedShares");
            ECEData.EachShare = dr.GetString("ConsiderationOfEachShare");
            ECEData.AmountPaidToEachShare = dr.GetString("AmountPaidToEachShare");
            ECEData.TotalConsideration = dr.GetString("TotalConsideration");
            ECEData.NoOfIssuedShares = dr.GetString("ResultantNoOfIssuedShares");
            ECEData.IssuedCapital = dr.GetString("IssuedCapital");
            ECEData.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            ECEData.MeetingNotice = dr.GetInt32("MeetingNotice");
            ECEData.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            ECEData.MeetingMinutes = dr.GetInt32("MeetingMinutes");
            ECEData.OthersMeetingMinutes = dr.GetString("OthersMeetingMinutes");
            ECEData.Designation = dr.GetString("Designation");
            ECEData.NoticeResolutionSource = dr.GetString("NoticeResolutionSource");
            ECEData.NoticeResolution = dr.GetInt32("NoticeResolution");
            ECEData.F24F25Source = dr.GetString("F24F25Source");
            ECEData.F24F25ID = dr.GetInt32("F24F25ID");
            ECEData.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            ECEData.ResultantPaidupCapital = dr.GetString("ResultantPaidupCapital");

            ECEData.IsROPlaceOfMeeting = dr.GetBoolean("IsROPlaceOfMeeting");
            ECEData.MAddressLine1 = dr.GetString("MeetingAddressLine1");
            ECEData.MAddressLine2 = dr.GetString("MeetingAddressLine2");
            ECEData.MAddressLine3 = dr.GetString("MeetingAddressLine3");
            ECEData.MAddressCountry = dr.GetInt32("MAddressCountry");
            ECEData.MAddressPostalCode = dr.GetString("MPostalCode");

            return ECEData;
        }
        #endregion

        #region DatabaseMethods
        /// <summary>
        /// Description  : To Save ECE Details.
        /// Created By   : Shiva  
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>ECE Status.</returns>
        public int SaveWOECEDetails()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[28];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@Currency", this.Currency);
                sqlParams[2] = new SqlParameter("@ClassOfShare", this.ClassOfShare);
                sqlParams[3] = new SqlParameter("@NewAllottedShares", this.NewAllottedShares);
                sqlParams[4] = new SqlParameter("@EachShare", this.EachShare);
                sqlParams[5] = new SqlParameter("@AmountPaidToEachShare", this.AmountPaidToEachShare);
                sqlParams[6] = new SqlParameter("@TotalConsideration", this.TotalConsideration);
                sqlParams[7] = new SqlParameter("@NoOfIssuedShares", this.NoOfIssuedShares);
                sqlParams[8] = new SqlParameter("@IssuedCapital", this.IssuedCapital);
                sqlParams[9] = new SqlParameter("@MeetingNoticeSource", this.MeetingNoticeSource);
                sqlParams[10] = new SqlParameter("@MeetingNotice", this.MeetingNotice);
                sqlParams[11] = new SqlParameter("@MeetingMinutesSource", this.MeetingMinutesSource);
                sqlParams[12] = new SqlParameter("@MeetingMinutes", this.MeetingMinutes);
                sqlParams[13] = new SqlParameter("@OthersMeetingMinutes", this.OthersMeetingMinutes);
                sqlParams[14] = new SqlParameter("@Designation", this.Designation);
                sqlParams[15] = new SqlParameter("@NoticeResolutionSource", this.NoticeResolutionSource);
                sqlParams[16] = new SqlParameter("@NoticeResolution", this.NoticeResolution);
                sqlParams[17] = new SqlParameter("@F24F25Source", this.F24F25Source);
                sqlParams[18] = new SqlParameter("@F24F25ID", this.F24F25ID);
                sqlParams[19] = new SqlParameter("@ShareHoldingStructure", this.ShareHoldingStructure);
                sqlParams[20] = new SqlParameter("@SavedBy", this.SavedBy);
                sqlParams[21] = new SqlParameter("@ResultantPaidupCapital", this.ResultantPaidupCapital);
                sqlParams[22] = new SqlParameter("@IsROPlaceOfMeeting", IsROPlaceOfMeeting);
                sqlParams[23] = new SqlParameter("@MeetingAddressLine1", MAddressLine1);
                sqlParams[24] = new SqlParameter("@MeetingAddressLine2", MAddressLine2);
                sqlParams[25] = new SqlParameter("@MeetingAddressLine3", MAddressLine3);
                sqlParams[26] = new SqlParameter("@MAddressCountry", MAddressCountry);
                sqlParams[27] = new SqlParameter("@MPostalCode", MAddressPostalCode);
                return result = SqlHelper.ExecuteNonQuery(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveECEDetailsByWOID", sqlParams);
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
        /// Description  : To Get ECE Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 3rd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>ECE Details.</returns>
        public static WOExistingClientEngaging GetWOECEDetailsByWOID(int WOID)
        {
            var WOECEData = new WOExistingClientEngaging();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOECEDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOECEData.FetchECEDetails(WOECEData, safe);
                }

                return WOECEData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOECEData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion
    }
}