
#region Document Header
//Created By       : Shiva 
//Created Date     : 2nd Sept 2014
//Description      : WO EGM Acquisition Disposal Logic
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
    public class WOEGMAcquisitionDisposal
    {
        private static ILog log = LogManager.GetLogger(typeof(WOEGMAcquisitionDisposal));

        #region Properties.
        public int WOID { set; get; }
        public string MeetingNotice { set; get; }
        public string MeetingNoticeSource { set; get; }
        public string MeetingMinutes { set; get; }
        public string MeetingMinutesSource { set; get; }
        public string OthersMeetingMinutes { set; get; }
        public string Designation { set; get; }
        public int ShareHoldingStructure { set; get; }
        public int ConsiderationCurrency { set; get; }
        public string ConsiderationAmount { set; get; }
        public string NameVendor { set; get; }
        public bool IsROPlaceOfMeeting { set; get; }
        public string MeetingAddressLine1 { set; get; }
        public string MeetingAddressLine2 { set; get; }
        public string MeetingAddressLine3 { set; get; }
        public int MeetingAddressCountry { set; get; }
        public string MeetingAddressCountryName { set; get; }
        public string MeetingAddressPostalCode { set; get; }
        public string PropertyAddressLine1 { set; get; }
        public string PropertyAddressLine2 { set; get; }
        public string PropertyAddressLine3 { set; get; }
        public int PropertyAddressCountry { set; get; }
        public string PropertyAddressPostalCode { set; get; }
        public int SavedBy { set; get; }

        public int TypeOfTransaction { get; set; }

        #endregion

        #region FetchMethods.
        private WOEGMAcquisitionDisposal FetchCompanyDetailsByWOID(WOEGMAcquisitionDisposal GetDetails, SafeDataReader dr, bool IsFMGAddress)
        {
            if (IsFMGAddress)
            {
                string[] str = dr.GetString("AddressLine1").Split(';');
                GetDetails.MeetingAddressLine1 = str[0];
                GetDetails.MeetingAddressLine2 = str[1];
                GetDetails.MeetingAddressLine3 = str[2];
                GetDetails.MeetingAddressPostalCode = str[3];
            }
            else
            {
                GetDetails.MeetingAddressLine1 = dr.GetString("AddressLine1");
                GetDetails.MeetingAddressLine2 = dr.GetString("AddressLine2");
                GetDetails.MeetingAddressLine3 = dr.GetString("AddressLine3");
                GetDetails.MeetingAddressPostalCode = dr.GetString("AddressPostalCode");
            }
            GetDetails.MeetingAddressCountry = dr.GetInt32("countryid");
            GetDetails.MeetingAddressCountryName = dr.GetString("AddressCountry");

            return GetDetails;

        }

        private WOEGMAcquisitionDisposal FetchEGMByWOID(WOEGMAcquisitionDisposal GetDetails, SafeDataReader dr)
        {
            GetDetails.WOID = dr.GetInt32("WOID");
            GetDetails.MeetingNotice = dr.GetString("MeetingNotice");
            GetDetails.MeetingNoticeSource = dr.GetString("MeetingNoticeSource");
            GetDetails.MeetingMinutes = dr.GetString("MeetingMinutes");
            GetDetails.MeetingMinutesSource = dr.GetString("MeetingMinutesSource");
            GetDetails.OthersMeetingMinutes = dr.GetString("OthersMeetingMinutes");
            GetDetails.Designation = dr.GetString("Designation");
            GetDetails.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            GetDetails.ConsiderationCurrency = dr.GetInt32("ConsiderationCurrency");
            GetDetails.ConsiderationAmount = dr.GetString("ConsiderationAmount");
            GetDetails.NameVendor = dr.GetString("NameVendor");
            GetDetails.IsROPlaceOfMeeting = dr.GetBoolean("ISROPlaceOfMeeting");
            GetDetails.MeetingAddressLine1 = dr.GetString("MeetingAddressLine1");
            GetDetails.MeetingAddressLine2 = dr.GetString("MeetingAddressLine2");
            GetDetails.MeetingAddressLine3 = dr.GetString("MeetingAddressLine3");
            GetDetails.MeetingAddressCountry = dr.GetInt32("MeetingAddressCountry");
            GetDetails.MeetingAddressPostalCode = dr.GetString("MeetingAddressPostalCode");
            GetDetails.PropertyAddressLine1 = dr.GetString("PropertyAddressLine1");
            GetDetails.PropertyAddressLine2 = dr.GetString("PropertyAddressLine2");
            GetDetails.PropertyAddressLine3 = dr.GetString("PropertyAddressLine3");
            GetDetails.PropertyAddressCountry = dr.GetInt32("PropertyAddressCountry");
            GetDetails.PropertyAddressPostalCode = dr.GetString("PropertyAddressPostalCode");
            return GetDetails;
        }

        #endregion

        #region DatabaseMethods.

        /// <summary>
        /// Description  : To Get CompanyDetails by WOID.
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Company Details.</returns>
        public static WOEGMAcquisitionDisposal GetCompanyDetailsByWOID(int WOID, bool IsFMGAddress)
        {
            var WOEGMData = new WOEGMAcquisitionDisposal();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@IsFMGAddress", IsFMGAddress);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCompanyDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOEGMData.FetchCompanyDetailsByWOID(WOEGMData, safe, IsFMGAddress);
                }

                return WOEGMData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOEGMData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : To Save EGM Details.
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Status.</returns>
        public int SaveWOEGMDetails()
        {
            int result = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[24];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@MeetingNotice", this.MeetingNotice);
                sqlParams[2] = new SqlParameter("@MeetingNoticeSource", this.MeetingNoticeSource);
                sqlParams[3] = new SqlParameter("@MeetingMinutes", this.MeetingMinutes);
                sqlParams[4] = new SqlParameter("@MeetingMinutesSource", this.MeetingMinutesSource);
                sqlParams[5] = new SqlParameter("@OtherMeetingMinutes", this.OthersMeetingMinutes);
                sqlParams[6] = new SqlParameter("@Designation", this.Designation);
                sqlParams[7] = new SqlParameter("@ShareHoldingStructure", this.ShareHoldingStructure);
                sqlParams[8] = new SqlParameter("@ConsiderartionCurrency", this.ConsiderationCurrency);
                sqlParams[9] = new SqlParameter("@ConsiderationAmount", this.ConsiderationAmount);
                sqlParams[10] = new SqlParameter("@NameVendor", this.NameVendor);
                sqlParams[11] = new SqlParameter("@IsROPlaceOfMeeting", this.IsROPlaceOfMeeting);
                sqlParams[12] = new SqlParameter("@MAddress1", this.MeetingAddressLine1);
                sqlParams[13] = new SqlParameter("@MAddress2", this.MeetingAddressLine2);
                sqlParams[14] = new SqlParameter("@MAddress3", this.MeetingAddressLine3);
                sqlParams[15] = new SqlParameter("@MCountry", this.MeetingAddressCountry);
                sqlParams[16] = new SqlParameter("@MPostalCode", this.MeetingAddressPostalCode);
                sqlParams[17] = new SqlParameter("@PAddress1", this.PropertyAddressLine1);
                sqlParams[18] = new SqlParameter("@PAddress2", this.PropertyAddressLine2);
                sqlParams[19] = new SqlParameter("@PAddress3", this.PropertyAddressLine3);
                sqlParams[20] = new SqlParameter("@PCountry", this.PropertyAddressCountry);
                sqlParams[21] = new SqlParameter("@PPostalCode", this.PropertyAddressPostalCode);
                sqlParams[22] = new SqlParameter("@SavedBy", this.SavedBy);
                sqlParams[23] = new SqlParameter("@TypeOfTransaction", this.TypeOfTransaction);

                return result = SqlHelper.ExecuteNonQuery(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveEGMDetailsByWOID", sqlParams);
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
        /// Description  : To Get EGM Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 2nd Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>EGM Details.</returns>
        public static WOEGMAcquisitionDisposal GetEGMDetailsByWOID(int WOID)
        {
            var WOEGMData = new WOEGMAcquisitionDisposal();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetEGMDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOEGMData.FetchEGMByWOID(WOEGMData, safe);
                }

                return WOEGMData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOEGMData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        #endregion
    }
}