using CSS2.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSS2.Areas.WO.Models
{
    public class WOBonusIssue
    {
        private static ILog log = LogManager.GetLogger(typeof(WOBonusIssue));

        #region Properties
        public int ID { set; get; }
        public int WOID { set; get; }
        public string BonusIssuePerShare { set; get; }
        public string RegisterOfMembersOn { set; get; }
        public string AmountPaidOnEachShare { set; get; }
        public string TotalNoOfIssuedShares { set; get; }
        public string ResultantIssuedCapital { set; get; }
        public int ClassOfShare { set; get; }
        public string ResultantPaidUpCapital { set; get; }
        public bool IsRegisteredAddressAsPlaceOfMeeting { set; get; }
        public string MeetingAddressLine1 { set; get; }
        public string MeetingAddressLine2 { set; get; }
        public string MeetingAddressLine3 { set; get; }
        public int MeetingAddressCountry { set; get; }
        public string MeetingAddressPostalCode { set; get; }
        public int MeetingNotice { set; get; }
        public string MeetingNoticeSource { set; get; }
        public int MeetingMinutes { set; get; }
        public string MeetingMinutesSource { set; get; }
        public string OthersMeetingMinutes { set; get; }
        public string Designation { set; get; }
        public int NoticeOfResolution { set; get; }
        public string NoticeOfResolutionSource { set; get; }
        public int LetterOfAllotment { set; get; }
        public string LetterOfAllotmentSource { set; get; }
        public int ReturnOfAllotment { set; get; }
        public string ReturnOfAllotmentSource { set; get; }
        public int ShareHoldingStructure { set; get; }
        public string ConsiderationOfEachShare { set; get; }
        public string TotalNoOfNewSharesToBeAllotted { set; get; }
        public string TotalConsideration { set; get; }
        public int Currency { set; get; }
        public int SavedBy { set; get; }



        #endregion

        #region FetchMethods

        internal static WOBonusIssue FetchWOBonusByWOID(WOBonusIssue bonusData, SafeDataReader dr)
        {
            bonusData.ID = dr.GetInt32("ID");
            bonusData.WOID = dr.GetInt32("WOID");
            bonusData.BonusIssuePerShare = dr.GetString("BonusIssuePerShare");
            bonusData.RegisterOfMembersOn = dr.GetString("RegisterOfMembersOn");
            bonusData.AmountPaidOnEachShare = dr.GetString("AmountPaidOnEachShare");
            bonusData.TotalNoOfIssuedShares = dr.GetString("TotalNoOfIssuedShares");
            bonusData.ResultantIssuedCapital = dr.GetString("ResultantIssuedCapital");
            bonusData.ClassOfShare = dr.GetInt32("ClassOfShare");
            bonusData.ResultantPaidUpCapital = dr.GetString("ResultantPaidUpCapital");
            bonusData.IsRegisteredAddressAsPlaceOfMeeting = dr.GetBoolean("IsRegisteredAddressAsPlaceOfMeeting");
            bonusData.MeetingAddressLine1 = dr.GetString("MeetingAddressLine1");
            bonusData.MeetingAddressLine2 = dr.GetString("MeetingAddressLine2");
            bonusData.MeetingAddressLine3 = dr.GetString("MeetingAddressLine3");
            bonusData.MeetingAddressCountry = dr.GetInt32("MeetingAddressCountry");
            bonusData.MeetingAddressPostalCode = dr.GetString("MeetingAddressPostalCode");
            bonusData.MeetingNotice = dr.GetInt32("MeetingNotice");
            bonusData.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            bonusData.MeetingMinutes = dr.GetInt32("MeetingMinutes");
            bonusData.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            bonusData.OthersMeetingMinutes = dr.GetString("OthersMeetingMinutes");
            bonusData.Designation = dr.GetString("Designation");
            bonusData.NoticeOfResolution = dr.GetInt32("NoticeOfResolution");
            bonusData.NoticeOfResolutionSource = dr.GetString("NoticeOfResolutionSource");
            bonusData.LetterOfAllotment = dr.GetInt32("LetterOfAllotment");
            bonusData.LetterOfAllotmentSource = dr.GetString("LetterOfAllotmentSource");
            bonusData.ReturnOfAllotment = dr.GetInt32("ReturnOfAllotment");
            bonusData.ReturnOfAllotmentSource = dr.GetString("ReturnOfAllotmentSource");
            bonusData.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            bonusData.ConsiderationOfEachShare = dr.GetString("ConsiderationOfEachShare");
            bonusData.TotalNoOfNewSharesToBeAllotted = dr.GetString("TotalNoOfNewSharesToBeAllotted");
            bonusData.TotalConsideration = dr.GetString("TotalConsideration");
            bonusData.Currency = dr.GetInt32("Currency");
            return bonusData;
        }



        #endregion

        #region DBMethods

        /// <summary>
        /// Description  : To Save WO bonus Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 24th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WO bonus status.</returns>
        public int SaveWOBonusIssueDetailsByWOID()
        {
            int result = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[27];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@BonusIssuePerShare", this.BonusIssuePerShare);
                sqlParams[2] = new SqlParameter("@RegisterOfMembersOn", HelperClasses.ConvertDateFormat(this.RegisterOfMembersOn));
                sqlParams[3] = new SqlParameter("@AmountPaidOnEachShare", this.AmountPaidOnEachShare);
                sqlParams[4] = new SqlParameter("@TotalNoOfIssuedShares", this.TotalNoOfIssuedShares);
                sqlParams[5] = new SqlParameter("@ResultantIssuedCapital", this.ResultantIssuedCapital);
                //sqlParams[6] = new SqlParameter("@ClassOfShare", this.ClassOfShare);
                sqlParams[6] = new SqlParameter("@ResultantPaidUpCapital", this.ResultantPaidUpCapital);
                sqlParams[7] = new SqlParameter("@IsRegisteredAddressAsPlaceOfMeeting", this.IsRegisteredAddressAsPlaceOfMeeting);
                sqlParams[8] = new SqlParameter("@MeetingAddressLine1", this.MeetingAddressLine1);
                sqlParams[9] = new SqlParameter("@MeetingAddressLine2", this.MeetingAddressLine2);
                sqlParams[10] = new SqlParameter("@MeetingAddressLine3", this.MeetingAddressLine3);
                sqlParams[11] = new SqlParameter("@MeetingAddressCountry", this.MeetingAddressCountry);
                sqlParams[12] = new SqlParameter("@MeetingAddressPostalCode", this.MeetingAddressPostalCode);
                sqlParams[13] = new SqlParameter("@MeetingNotice", this.MeetingNotice);
                sqlParams[14] = new SqlParameter("@MeetingNoticeSource", this.MeetingNoticeSource);
                sqlParams[15] = new SqlParameter("@MeetingMinutes", this.MeetingMinutes);
                sqlParams[16] = new SqlParameter("@MeetingMinutesSource", this.MeetingMinutesSource);
                sqlParams[17] = new SqlParameter("@OthersMeetingMinutes", this.OthersMeetingMinutes);
                sqlParams[18] = new SqlParameter("@Designation", this.Designation);
                sqlParams[19] = new SqlParameter("@NoticeOfResolution", this.NoticeOfResolution);
                sqlParams[20] = new SqlParameter("@NoticeOfResolutionSource", this.NoticeOfResolutionSource);
                sqlParams[21] = new SqlParameter("@LetterOfAllotment", this.LetterOfAllotment);
                sqlParams[22] = new SqlParameter("@LetterOfAllotmentSource", this.LetterOfAllotmentSource);
                sqlParams[23] = new SqlParameter("@ReturnOfAllotment", this.ReturnOfAllotment);
                sqlParams[24] = new SqlParameter("@ReturnOfAllotmentSource", this.ReturnOfAllotmentSource);
                sqlParams[25] = new SqlParameter("@ShareHoldingStructure", this.ShareHoldingStructure);
                sqlParams[26] = new SqlParameter("@SavedBy", this.SavedBy);

                return result = SqlHelper.ExecuteNonQuery(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveWOBonusIssueDetailsByWOID", sqlParams);
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
        /// Description  : To Get WO bonus Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 24th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WO bonus details.</returns>
        public static WOBonusIssue GetWOBonusIssueDetailsByWOID(int WOID)
        {
            var BonusData = new WOBonusIssue();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOBonusDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOBonusIssue.FetchWOBonusByWOID(BonusData, safe);
                }

                return BonusData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return BonusData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        #endregion

        #region Events
        public class WoBonusIssueInfo
        {
            internal List<WOBonusIssue> WOBonusList { set; get; }
            public WoBonusIssueInfo()
            {
                WOBonusList = new List<WOBonusIssue>();
            }
        }

        #endregion
    }
}