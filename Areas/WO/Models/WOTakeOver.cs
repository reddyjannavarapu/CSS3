#region Document Header
//Created By       : Sudheer     
//Created Date     : 17th Oct 2014
//Description      : WO TakeOver logic--------------------------------------------------------------------------------------------------
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
    public class WOTakeOver
    {
        private static ILog log = LogManager.GetLogger(typeof(WOTakeOver));

        #region Properties

        public int WOID { set; get; }
        public string CompanyName { set; get; }
        public string RegistrationNo { set; get; }
        public string DateAccBeLaid { set; get; }
        public int ClientID { set; get; }
        public string ClientSource { set; get; }      
        public int Currency { set; get; }
        public int ClassOfShare { set; get; }
        public string NewAllottedShares { set; get; }
        public string EachShare { set; get; }
        public string AmountPaidToEachShare { set; get; }
        public string TotalConsideration { set; get; }
        public string NoOfIssuedShares { set; get; }
        public string IssuedCapital { set; get; }
        public string ResultantPaidupCapital { set; get; }
        public bool IsFMRegisteredAddress { set; get; }
        public string AddressLine1 { set; get; }
        public string AddressLine2 { set; get; }
        public string AddressLine3 { set; get; }
        public int AddressCountry { set; get; }
        public string AddressPostalCode { set; get; }
        public string OutGoingAuditor { set; get; }
        public string Auditor { set; get; }
        public string MeetingNotice { set; get; }

        public string MeetingMinutes { set; get; }
       
        public string Designation { set; get; }
        
        public string NoticeResolution { set; get; }
       
        public string F24F25ID { set; get; }

        public int ShareHoldingStructure { set; get; }
       
        public int SavedBy { set; get; }
        #endregion

        #region FetchMethods

        private WOTakeOver FetchTakeOverDetails(WOTakeOver WOTAKOData, SafeDataReader dr)
        {
            WOTAKOData.CompanyName =  dr.GetString("CompanyName");
            WOTAKOData.RegistrationNo =  dr.GetString("RegistrationNo");

            WOTAKOData.ClientID = dr.GetInt32("ClientID");
            WOTAKOData.ClientSource = dr.GetString("ClientSource");

            WOTAKOData.DateAccBeLaid = dr.GetString("DateAccBeLaid");
            WOTAKOData.Currency = dr.GetInt32("Currency");
            WOTAKOData.ClassOfShare = dr.GetInt32("ClassOfShare");
            WOTAKOData.NewAllottedShares = dr.GetString("NewAllottedShares");
            WOTAKOData.EachShare = dr.GetString("ConsiderationOfEachShare");
            WOTAKOData.AmountPaidToEachShare = dr.GetString("AmountPaidToEachShare");
            WOTAKOData.TotalConsideration = dr.GetString("TotalConsideration");
            WOTAKOData.NoOfIssuedShares = dr.GetString("ResultantNoOfIssuedShares");
            WOTAKOData.IssuedCapital = dr.GetString("IssuedCapital");
            WOTAKOData.IsFMRegisteredAddress = dr.GetBoolean("IsFMRegisteredAddress");
            WOTAKOData.AddressLine1 = dr.GetString("AddressLine1");
            WOTAKOData.AddressLine2 = dr.GetString("AddressLine2");
            WOTAKOData.AddressLine3 = dr.GetString("AddressLine3");
            WOTAKOData.AddressCountry = dr.GetInt32("AddressCountry");
            WOTAKOData.AddressPostalCode = dr.GetString("AddressPostalCode");
            WOTAKOData.OutGoingAuditor = dr.GetString("OutGoingAuditor");
            WOTAKOData.Auditor = dr.GetString("Auditor");
            WOTAKOData.MeetingNotice = dr.GetString("MeetingNotice");
            WOTAKOData.MeetingMinutes = dr.GetString("MeetingMinutes");
            WOTAKOData.Designation = dr.GetString("Designation");
            WOTAKOData.NoticeResolution = dr.GetString("NoticeResolution");
            WOTAKOData.F24F25ID = dr.GetString("F24F25ID");
            WOTAKOData.ShareHoldingStructure = dr.GetInt32("ShareHoldingStructure");
            WOTAKOData.ResultantPaidupCapital = dr.GetString("ResultantPaidupCapital");           
            return WOTAKOData;
        }
        #endregion

        #region DatabaseMethods
        /// <summary>
        /// Description  : To Save ECE Details.
        /// Created By   : Sudheer  
        /// Created Date : 20th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>TakeOver Status.</returns>
        public int SaveWOTakeOverDetails()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[31];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@CompanyName", this.CompanyName);
                sqlParams[2] = new SqlParameter("@RegistrationNo", this.RegistrationNo);
                if (this.DateAccBeLaid == "" || this.DateAccBeLaid == null)
                    sqlParams[3] = new SqlParameter("@DateAccBeLaid", DBNull.Value);
                else
                    sqlParams[3] = new SqlParameter("@DateAccBeLaid", HelperClasses.ConvertDateFormat(this.DateAccBeLaid));

                sqlParams[4] = new SqlParameter("@Currency", this.Currency);
                sqlParams[5] = new SqlParameter("@ClassOfShare", this.ClassOfShare);
                sqlParams[6] = new SqlParameter("@NewAllottedShares", this.NewAllottedShares);
                sqlParams[7] = new SqlParameter("@EachShare", this.EachShare);
                sqlParams[8] = new SqlParameter("@AmountPaidToEachShare", this.AmountPaidToEachShare);
                sqlParams[9] = new SqlParameter("@TotalConsideration", this.TotalConsideration);
                sqlParams[10] = new SqlParameter("@NoOfIssuedShares", this.NoOfIssuedShares);
                sqlParams[11] = new SqlParameter("@IssuedCapital", this.IssuedCapital);
                sqlParams[12] = new SqlParameter("@IsFMRegisteredAddress", this.IsFMRegisteredAddress);
                sqlParams[13] = new SqlParameter("@AddressLine1", this.AddressLine1);
                sqlParams[14] = new SqlParameter("@AddressLine2", this.AddressLine2);
                sqlParams[15] = new SqlParameter("@AddressLine3", this.AddressLine3);
                sqlParams[16] = new SqlParameter("@AddressCountry", this.AddressCountry);
                sqlParams[17] = new SqlParameter("@AddressPostalCode", this.AddressPostalCode);
                sqlParams[18] = new SqlParameter("@MeetingNotice", this.MeetingNotice);
                sqlParams[19] = new SqlParameter("@MeetingMinutes", this.MeetingMinutes);
                sqlParams[20] = new SqlParameter("@Designation", this.Designation);
                sqlParams[21] = new SqlParameter("@NoticeResolution", this.NoticeResolution);
                sqlParams[22] = new SqlParameter("@F24F25ID", this.F24F25ID);
                sqlParams[23] = new SqlParameter("@ShareHoldingStructure", this.ShareHoldingStructure);
                sqlParams[24] = new SqlParameter("@SavedBy", this.SavedBy);
                sqlParams[25] = new SqlParameter("@ResultantPaidupCapital", this.ResultantPaidupCapital);
                sqlParams[26] = new SqlParameter("@OutGoingAuditor", this.OutGoingAuditor);                
                sqlParams[30] = new SqlParameter("@Auditor", this.Auditor);

                return result = SqlHelper.ExecuteNonQuery(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveTakeoverDetailsByWOID", sqlParams);
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
        /// Description  : To Get TakeOver Details by WOID.
        /// Created By   : Sudheer  
        /// Created Date : 20th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>ECE Details.</returns>
        public static WOTakeOver GetWOTakeOverDetailsByWOID(int WOID)
        {
            var WOTAKOData = new WOTakeOver();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(CSS2.Models.ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOTakeOverDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOTAKOData.FetchTakeOverDetails(WOTAKOData, safe);
                }               

                return WOTAKOData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOTAKOData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        } 
                  

        /// <summary>
        /// Description  : To Insert TakeOver Director Details
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOTakeOverDirectorDetails(string WOID, string DirectorName, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@DirectorName", DirectorName);               
                sqlParams[2] = new SqlParameter("@CreatedBy", createdBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertTakeOverDirectorDetails", sqlParams);
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
        /// Description  : To Insert TakeOver Director Details
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOTakeOverDirectorDetails(string WOID, string PersonID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@PersonID", PersonID);             
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteTakeOverDirectorDetails", sqlParams);
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
        /// Description  : To Get TakeOver Shareholder Details by WOID.
        /// Created By   : Sudheer  
        /// Created Date : 29th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Take Over Shareholder Details.</returns>
        public static DataSet GetWOTakeOverShareholderDetails(string WOID)
        {
            DataSet ds = new DataSet();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOTakeOverShareholderDetails", sqlParams);
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
        /// Description  : To Insert TakeOver Shareholder Details
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertTakeOverShareholderDetails(string WOID, string ShareholderName, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@ShareholderName", ShareholderName);
                sqlParams[2] = new SqlParameter("@CreatedBy", createdBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertTakeOverShareholderDetails", sqlParams);
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
        /// Description  : To Insert TakeOver Shareholder Details
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOTakeOverShareholderDetails(string WOID, string PersonID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@PersonID", PersonID);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteTakeOvershareholderDetails]", sqlParams);
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
        #endregion
    }
}