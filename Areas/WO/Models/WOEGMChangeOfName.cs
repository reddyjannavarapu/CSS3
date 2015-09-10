
#region Document Header
//Created By       : Shiva 
//Created Date     : 4th Sept 2014
//Description      : WO EGM Change Of Name Logic
//------------------------------------------------------------------------------------------------------------------------------------------------
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
    public class WOEGMChangeOfName
    {
        private static ILog log = LogManager.GetLogger(typeof(WOEGMChangeOfName));

        #region Properties

        public int WOID { set; get; }
        public string NewName { set; get; }
        public bool IsROPlaceOfMeeting { set; get; }
        public string MAddressLine1 { set; get; }
        public string MAddressLine2 { set; get; }
        public string MAddressLine3 { set; get; }
        public int MAddressCountry { set; get; }
        public string MAddressPostalCode { set; get; }
        public string MeetingNoticeSource { set; get; }
        public int MeetingNotice { set; get; }
        public string MeetingMinutesSource { set; get; }
        public int MeetingMinutes { set; get; }
        public string OthersMeetingMinutes { set; get; }
        public string Designation { set; get; }
        public string NoticeResolutionSource { set; get; }
        public int NoticeResolution { set; get; }
        public int ShareHoldingStructure { set; get; }
        public int SavedBy { set; get; }
        #endregion

        #region Fetch Methods
        private WOEGMChangeOfName FetchEGMChangeOfNameDetailsByWOID(WOEGMChangeOfName WOEGMChangeOfName, SafeDataReader dr)
        {
            WOEGMChangeOfName.WOID = dr.GetInt32("WOID");
            WOEGMChangeOfName.NewName = dr.GetString("NewName");
            WOEGMChangeOfName.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            WOEGMChangeOfName.IsROPlaceOfMeeting = dr.GetBoolean("IsROPlaceOfMeeting");
            WOEGMChangeOfName.MAddressLine1 = dr.GetString("MeetingAddressLine1");
            WOEGMChangeOfName.MAddressLine2 = dr.GetString("MeetingAddressLine2");
            WOEGMChangeOfName.MAddressLine3 = dr.GetString("MeetingAddressLine3");
            WOEGMChangeOfName.MAddressCountry = dr.GetInt32("MAddressCountry");
            WOEGMChangeOfName.MAddressPostalCode = dr.GetString("MPostalCode");
            WOEGMChangeOfName.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            WOEGMChangeOfName.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            WOEGMChangeOfName.MeetingNotice = dr.GetInt32("MeetingNotice");
            WOEGMChangeOfName.MeetingMinutes = dr.GetInt32("MeetingMinutes");
            WOEGMChangeOfName.OthersMeetingMinutes = dr.GetString("OthersMeetingMinutes");
            WOEGMChangeOfName.Designation = dr.GetString("Designation");
            WOEGMChangeOfName.NoticeResolutionSource = dr.GetString("NoticeResolutionSource");
            WOEGMChangeOfName.NoticeResolution = dr.GetInt32("NoticeResolution");
            return WOEGMChangeOfName;
        }

        #endregion

        #region Database Methods
        /// <summary>
        /// Description  : To Save EGM Change Of Name Details.
        /// Created By   : Shiva  
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Change Of Name Status.</returns>
        public int SaveWOEGMChangeOfNameDetailsByWOID()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[18];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@ShareHoldingStructure", this.ShareHoldingStructure);
                sqlParams[2] = new SqlParameter("@IsROPlaceOfMeeting", this.IsROPlaceOfMeeting);
                sqlParams[3] = new SqlParameter("@MeetingAddressLine1", this.MAddressLine1);
                sqlParams[4] = new SqlParameter("@MeetingAddressLine2", this.MAddressLine2);
                sqlParams[5] = new SqlParameter("@MeetingAddressLine3", this.MAddressLine3);
                sqlParams[6] = new SqlParameter("@MAddressCountry", this.MAddressCountry);
                sqlParams[7] = new SqlParameter("@MPostalCode", this.MAddressPostalCode);
                sqlParams[8] = new SqlParameter("@MeetingMinutesSource", this.MeetingMinutesSource);
                sqlParams[9] = new SqlParameter("@MeetingNoticeSource", this.MeetingNoticeSource);
                sqlParams[10] = new SqlParameter("@MeetingNotice", this.MeetingNotice);
                sqlParams[11] = new SqlParameter("@MeetingMinutes", this.MeetingMinutes);
                sqlParams[12] = new SqlParameter("@OthersMeetingMinutes", this.OthersMeetingMinutes);
                sqlParams[13] = new SqlParameter("@Designation", this.Designation);
                sqlParams[14] = new SqlParameter("@NoticeResolutionSource", this.NoticeResolutionSource);
                sqlParams[15] = new SqlParameter("@NoticeResolution", this.NoticeResolution);
                sqlParams[16] = new SqlParameter("@SavedBy", this.SavedBy);
                sqlParams[17] = new SqlParameter("@NewName", this.NewName);


                return result = SqlHelper.ExecuteNonQuery(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveWOEGMChangeOfNameDetailsByWOID", sqlParams);
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
        #endregion


        /// <summary>
        /// Description  : To Get WO EGM Change Of Name Details.
        /// Created By   : Shiva  
        /// Created Date : 4th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Change Of Name Deatils.</returns>
        public static object GetWOEGMChangeOfNameDetailsByWOID(int WOID)
        {
            var GetWOEGMChangeOfName = new WOEGMChangeOfName();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOEGMChangeOfNameDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    GetWOEGMChangeOfName.FetchEGMChangeOfNameDetailsByWOID(GetWOEGMChangeOfName, safe);
                }
                return GetWOEGMChangeOfName;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetWOEGMChangeOfName;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


    }
}