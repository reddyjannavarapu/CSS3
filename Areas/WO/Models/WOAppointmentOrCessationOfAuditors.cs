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
    public class WOAppointmentOrCessationOfAuditors
    {
        private static ILog log = LogManager.GetLogger(typeof(WOAppointmentOrCessationOfAuditors));

        #region Properties

        public int WOID { get; set; }
        public int ModeofAppointment { get; set; }
        public int Auditor { get; set; }
        public string NameOfAuditor { get; set; }
        public int ShareHoldingStructure { get; set; }
        public int MeetingNotice { get; set; }
        public string MeetingNoticeSource { get; set; }
        public int MeetingMinutes { get; set; }
        public string MeetingMinutesSource { get; set; }
        public string OtherMeetingMinutes { set; get; }
        public string Designation { set; get; }
        public string OutgoingAuditor { set; get; }
        public bool IsROPlaceOfMeeting { set; get; }
        public string MAddressLine1 { set; get; }
        public string MAddressLine2 { set; get; }
        public string MAddressLine3 { set; get; }
        public int MAddressCountry { set; get; }
        public string MAddressPostalCode { set; get; }

        #endregion

        #region Fetch Methods

        private WOAppointmentOrCessationOfAuditors FetchWOAppointOrCessationDetails(WOAppointmentOrCessationOfAuditors AppointmentOrCessationOfAuditors, SafeDataReader dr)
        {
            AppointmentOrCessationOfAuditors.WOID=dr.GetInt32("WOID");
            AppointmentOrCessationOfAuditors.ModeofAppointment = dr.GetInt32("ModeofAppointment");
            AppointmentOrCessationOfAuditors.Auditor = dr.GetInt32("Auditor");
            AppointmentOrCessationOfAuditors.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            AppointmentOrCessationOfAuditors.IsROPlaceOfMeeting = dr.GetBoolean("IsROPlaceOfMeeting");
            AppointmentOrCessationOfAuditors.MAddressLine1 = dr.GetString("MeetingAddressLine1");
            AppointmentOrCessationOfAuditors.MAddressLine2 = dr.GetString("MeetingAddressLine2");
            AppointmentOrCessationOfAuditors.MAddressLine3 = dr.GetString("MeetingAddressLine3");
            AppointmentOrCessationOfAuditors.MAddressCountry = dr.GetInt32("MAddressCountry");
            AppointmentOrCessationOfAuditors.MAddressPostalCode = dr.GetString("MPostalCode");
            AppointmentOrCessationOfAuditors.MeetingNotice = dr.GetInt32("MeetingNotice");
            AppointmentOrCessationOfAuditors.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            AppointmentOrCessationOfAuditors.MeetingMinutes = dr.GetInt32("MeetingMinutes");
            AppointmentOrCessationOfAuditors.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            AppointmentOrCessationOfAuditors.OtherMeetingMinutes = dr.GetString("OtherMeetingMinutes");
            AppointmentOrCessationOfAuditors.Designation = dr.GetString("Designation");
            AppointmentOrCessationOfAuditors.OutgoingAuditor = dr.GetString("OutgoingAuditor");
            AppointmentOrCessationOfAuditors.NameOfAuditor = dr.GetString("NameOfAuditor");
            return AppointmentOrCessationOfAuditors;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Insert Work Order AppointmentOrCessation Of Auditors Details.
        /// Created By   : Pavan
        /// Created Date : 23 August 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOAppointmentOrCessationDetails(WOAppointmentOrCessationOfAuditors AppointmentOrCessationOfAuditors, int CreatedBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[19];
                sqlParams[0] = new SqlParameter("@WOID", AppointmentOrCessationOfAuditors.WOID);
                sqlParams[1] = new SqlParameter("@ModeofAppointment", AppointmentOrCessationOfAuditors.ModeofAppointment);
                sqlParams[2] = new SqlParameter("@Auditor", AppointmentOrCessationOfAuditors.Auditor);
                sqlParams[3] = new SqlParameter("@NameOfAuditor", AppointmentOrCessationOfAuditors.NameOfAuditor);
                sqlParams[4] = new SqlParameter("@ShareHoldingStructure", AppointmentOrCessationOfAuditors.ShareHoldingStructure);
                sqlParams[5] = new SqlParameter("@MeetingNotice", AppointmentOrCessationOfAuditors.MeetingNotice);
                sqlParams[6] = new SqlParameter("@MeetingNoticeSource", AppointmentOrCessationOfAuditors.MeetingNoticeSource);
                sqlParams[7] = new SqlParameter("@MeetingMinutes", AppointmentOrCessationOfAuditors.MeetingMinutes);
                sqlParams[8] = new SqlParameter("@MeetingMinutesSource", AppointmentOrCessationOfAuditors.MeetingMinutesSource);
                sqlParams[9] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[10] = new SqlParameter("@OtherMeetingMinutes", AppointmentOrCessationOfAuditors.OtherMeetingMinutes);
                sqlParams[11] = new SqlParameter("@Designation", AppointmentOrCessationOfAuditors.Designation);
                sqlParams[12] = new SqlParameter("@OutgoingAuditor", AppointmentOrCessationOfAuditors.OutgoingAuditor);
                sqlParams[13] = new SqlParameter("@IsROPlaceOfMeeting", AppointmentOrCessationOfAuditors.IsROPlaceOfMeeting);
                sqlParams[14] = new SqlParameter("@MeetingAddressLine1", AppointmentOrCessationOfAuditors.MAddressLine1);
                sqlParams[15] = new SqlParameter("@MeetingAddressLine2", AppointmentOrCessationOfAuditors.MAddressLine2);
                sqlParams[16] = new SqlParameter("@MeetingAddressLine3", AppointmentOrCessationOfAuditors.MAddressLine3);
                sqlParams[17] = new SqlParameter("@MAddressCountry", AppointmentOrCessationOfAuditors.MAddressCountry);
                sqlParams[18] = new SqlParameter("@MPostalCode", AppointmentOrCessationOfAuditors.MAddressPostalCode);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWOAppointmentOrCessationDetails]", sqlParams);
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
        /// Created Date : 22 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WO AppointmentOrCessation Of AuditorsDetails by WOID
        /// </summary>
        /// <returns></returns>
        public static WOAppointmentOrCessationOfAuditors GetWOAppointmentOrCessationDetailsByWOID(int WOID)
        {
            var Details = new WOAppointmentOrCessationOfAuditors();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOAppointmentOrCessationDetailsByWOID]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    Details.FetchWOAppointOrCessationDetails(Details, safe);
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