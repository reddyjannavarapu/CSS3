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

    public class AllotmentDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(AllotmentDetails));

        #region Properties

        public int WOID { get; set; }
        public int ReturnOfAllotmentOfShares { get; set; }
        public int Currency { get; set; }
        public int ClassOfShare { get; set; }
        public string NumberOfNewSharesToBeAllotted { get; set; }
        public string ConsiderationOfEachShare { get; set; }
        public string AmountToBeTreatedAsPaidOnEachShare { get; set; }
        public string TotalConsideration { get; set; }
        public string ResultantTotalNoOfIssuedShares { get; set; }
        public string ResultantIssuedCapital { get; set; }
        public string ResultantPaidUpCapital { get; set; }
        public int MeetingNotice { get; set; }
        public string MeetingNoticeSource { get; set; }
        public int MeetingMinutes { get; set; }
        public string OtherMeetingMinutes { set; get; }
        public string MeetingMinutesSource { get; set; }
        public string Designation { get; set; }
        public int NoticeOfResolution { get; set; }
        public string NoticeOfResolutionSource { get; set; }
        public int F24F25 { get; set; }
        public string F24F25Source { get; set; }
        public int ShareholdingStructure { get; set; }
        public bool IsROPlaceOfMeeting { set; get; }
        public string MAddressLine1 { set; get; }
        public string MAddressLine2 { set; get; }
        public string MAddressLine3 { set; get; }
        public int MAddressCountry { set; get; }
        public string MAddressPostalCode { set; get; }

        #endregion

        #region FetchData

        private AllotmentDetails FetchWOAllot(AllotmentDetails WOAllotData, SafeDataReader dr)
        {
            WOAllotData.WOID = dr.GetInt32("WOID");
            WOAllotData.ReturnOfAllotmentOfShares = dr.GetInt32("ReturnOfAllotmentOfShares");
            WOAllotData.Currency = dr.GetInt32("Currency");
            WOAllotData.ClassOfShare = dr.GetInt32("ClassOfShare");
            WOAllotData.NumberOfNewSharesToBeAllotted = dr.GetString("NumberOfNewSharesToBeAllotted");
            WOAllotData.ConsiderationOfEachShare = dr.GetString("ConsiderationOfEachShare");
            WOAllotData.AmountToBeTreatedAsPaidOnEachShare = dr.GetString("AmountToBeTreatedAsPaidOnEachShare");
            WOAllotData.TotalConsideration = dr.GetString("TotalConsideration");
            WOAllotData.ResultantTotalNoOfIssuedShares = dr.GetString("ResultantTotalNoOfIssuedShares");
            WOAllotData.ResultantIssuedCapital = dr.GetString("ResultantIssuedCapital");
            WOAllotData.ResultantPaidUpCapital = dr.GetString("ResultantPaidUpCapital");
            WOAllotData.MeetingNotice = dr.GetInt32("MeetingNotice");
            WOAllotData.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            WOAllotData.MeetingMinutes = dr.GetInt32("MeetingMinutes");
            WOAllotData.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            WOAllotData.OtherMeetingMinutes = dr.GetString("OtherMeetingMinutes");
            WOAllotData.Designation = dr.GetString("DesignationOfThePersonSigningTheAllotment");
            WOAllotData.NoticeOfResolution = dr.GetInt32("NoticeOfResolution");
            WOAllotData.NoticeOfResolutionSource = dr.GetString("NoticeOfResolutionSource");
            WOAllotData.F24F25 = dr.GetInt32("F24F25");
            WOAllotData.F24F25Source = dr.GetString("F24F25Source");
            WOAllotData.ShareholdingStructure = dr.GetInt32("ShareholdingStructure");
            WOAllotData.IsROPlaceOfMeeting = dr.GetBoolean("IsROPlaceOfMeeting");
            WOAllotData.MAddressLine1 = dr.GetString("MeetingAddressLine1");
            WOAllotData.MAddressLine2 = dr.GetString("MeetingAddressLine2");
            WOAllotData.MAddressLine3 = dr.GetString("MeetingAddressLine3");
            WOAllotData.MAddressCountry = dr.GetInt32("MAddressCountry");
            WOAllotData.MAddressPostalCode = dr.GetString("MPostalCode");
            return WOAllotData;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Insert Work Order WOAllotmentDetails.
        /// Created By   : Sudheer  
        /// Created Date : 4th Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOAllotmentDetails(AllotmentDetails WOAlloatDetails, int CreatedBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[29];

                sqlParams[0] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[1] = new SqlParameter("@WOID", WOAlloatDetails.WOID);
                sqlParams[2] = new SqlParameter("@ReturnOfAllotmentOfShares", WOAlloatDetails.ReturnOfAllotmentOfShares);
                sqlParams[3] = new SqlParameter("@Currency", WOAlloatDetails.Currency);
                sqlParams[4] = new SqlParameter("@ClassOfShare", WOAlloatDetails.ClassOfShare);
                sqlParams[5] = new SqlParameter("@NumberOfNewSharesToBeAllotted", WOAlloatDetails.NumberOfNewSharesToBeAllotted);
                sqlParams[6] = new SqlParameter("@ConsiderationOfEachShare", WOAlloatDetails.ConsiderationOfEachShare);
                sqlParams[7] = new SqlParameter("@AmountToBeTreatedAsPaidOnEachShare", WOAlloatDetails.AmountToBeTreatedAsPaidOnEachShare);
                sqlParams[8] = new SqlParameter("@TotalConsideration", WOAlloatDetails.TotalConsideration);
                sqlParams[9] = new SqlParameter("@ResultantTotalNoOfIssuedShares", WOAlloatDetails.ResultantTotalNoOfIssuedShares);
                sqlParams[10] = new SqlParameter("@ResultantIssuedCapital", WOAlloatDetails.ResultantIssuedCapital);
                sqlParams[11] = new SqlParameter("@ResultantPaidUpCapital", WOAlloatDetails.ResultantPaidUpCapital);
                sqlParams[12] = new SqlParameter("@MeetingNotice", WOAlloatDetails.MeetingNotice);
                sqlParams[13] = new SqlParameter("@MeetingNoticeSource", WOAlloatDetails.MeetingNoticeSource);
                sqlParams[14] = new SqlParameter("@MeetingMinutes", WOAlloatDetails.MeetingMinutes);
                sqlParams[15] = new SqlParameter("@MeetingMinutesSource", WOAlloatDetails.MeetingMinutesSource);
                sqlParams[16] = new SqlParameter("@DesignationOfThePersonSigningTheAGM", WOAlloatDetails.Designation);
                sqlParams[17] = new SqlParameter("@NoticeOfResolution", WOAlloatDetails.NoticeOfResolution);
                sqlParams[18] = new SqlParameter("@NoticeOfResolutionSource", WOAlloatDetails.NoticeOfResolutionSource);
                sqlParams[19] = new SqlParameter("@F24F25", WOAlloatDetails.F24F25);
                sqlParams[20] = new SqlParameter("@F24F25Source", WOAlloatDetails.F24F25Source);
                sqlParams[21] = new SqlParameter("@ShareholdingStructure", WOAlloatDetails.ShareholdingStructure);
                sqlParams[22] = new SqlParameter("@OtherMeetingMinutes", WOAlloatDetails.OtherMeetingMinutes);
                sqlParams[23] = new SqlParameter("@IsROPlaceOfMeeting", WOAlloatDetails.IsROPlaceOfMeeting);
                sqlParams[24] = new SqlParameter("@MeetingAddressLine1", WOAlloatDetails.MAddressLine1);
                sqlParams[25] = new SqlParameter("@MeetingAddressLine2", WOAlloatDetails.MAddressLine2);
                sqlParams[26] = new SqlParameter("@MeetingAddressLine3", WOAlloatDetails.MAddressLine3);
                sqlParams[27] = new SqlParameter("@MAddressCountry", WOAlloatDetails.MAddressCountry);
                sqlParams[28] = new SqlParameter("@MPostalCode", WOAlloatDetails.MAddressPostalCode);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertOrUpdateWOAllotmentDetails", sqlParams);
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
        /// Description  : To Get Work Order INCorpDetails.
        /// Created By   : Sudheer  
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static AllotmentDetails GetWOAllotmentDetails(string WOID)
        {
            var WOAllotData = new AllotmentDetails();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOAllotmentDetails]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOAllotData.FetchWOAllot(WOAllotData, safe);
                }

                return WOAllotData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOAllotData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

    }
}