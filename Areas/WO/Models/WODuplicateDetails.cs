# region Document Header
//Created By       :    Pavan 
//Created Date     :    3 September 2014
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

#region Usings
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

    public class WODuplicateDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(WODuplicateDetails));

        #region Properties

        public string WOID { set; get; }
        public int ClassOfShare { set; get; }
        public int CreatedBy { set; get; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string CertNo { get; set; }
        public string NoofShares { get; set; }
        public string DateOfIssue { get; set; }
        public int NoOfNewCertToBeIssued { get; set; }

        #endregion

        #region Fetch Methods

        private WODuplicateDetails FetchWODuplicateDetails(WODuplicateDetails WODuplicateDetails, SafeDataReader dr)
        {
            WODuplicateDetails.WOID = dr.GetString("WOID");
            WODuplicateDetails.ClassOfShare = dr.GetInt32("ClassOfShare");
            return WODuplicateDetails;
        }

        private WODuplicateDetails FetchWODuplicateShareHoldersDetails(WODuplicateDetails WODuplicateDetails, SafeDataReader dr)
        {
            WODuplicateDetails.ID = dr.GetInt32("ID");
            WODuplicateDetails.Name = dr.GetString("Name");
            WODuplicateDetails.CertNo = dr.GetString("CertNo");
            WODuplicateDetails.NoofShares = dr.GetString("NoofShares");
            //if NULL Displaying as "" in UI
            WODuplicateDetails.DateOfIssue = dr.GetDateTime("DateOfIssue") == default(DateTime) ? string.Empty : dr.GetDateTime("DateOfIssue").ToString("dd/MM/yy");
            WODuplicateDetails.NoOfNewCertToBeIssued = dr.GetInt32("NoOfNewCertToBeIssued");
            return WODuplicateDetails;
        }

        #endregion

        #region DataBaseMethods

        /// <summary>
        /// Description  : To Insert Work Order Duplicate Details.
        /// Created By   : Pavan
        /// Created Date : 3 Sepember 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <returns></returns>
        public static int InsertWODuplicateDetails(WODuplicateDetails WODuplicateDetails, int CreatedBy)
        {
            int output = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WODuplicateDetails.WOID);
                sqlParams[1] = new SqlParameter("@ClassOfShare", WODuplicateDetails.ClassOfShare);
                sqlParams[2] = new SqlParameter("@CreatedBy", CreatedBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWODuplicateDetails]", sqlParams);
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
        /// Description  : To delete Work Order Duplicate Detail.
        /// Created By   : Pavan
        /// Created Date : 3 Sepember 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <returns></returns>
        public static int DeleteWODuplicateShareholderDetailsByID(int ID)
        {
            int output = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteWODuplicateShareHolderDetail]", sqlParams);
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
        /// Description  : To Insert Work Order Duplicate Share Holder Details.
        /// Created By   : Pavan
        /// Created Date : 4 November 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <returns></returns>
        public static int InsertWODuplicateShareHolderDetails(int WOID, int personId, string sourcecode, string CertNo, string NoOfShares, int NoOfNewCertToBeIssued, string DateOfIssue, int CreatedBy)
        {
            int output = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@PersonID", personId);
                sqlParams[2] = new SqlParameter("@PersonSource", sourcecode);
                sqlParams[3] = new SqlParameter("@CertNo", CertNo);
                sqlParams[4] = new SqlParameter("@NoofShares", NoOfShares);
                sqlParams[5] = new SqlParameter("@NoOfNewCertToBeIssued", NoOfNewCertToBeIssued);
                sqlParams[6] = new SqlParameter("@DateOfIssue", HelperClasses.ConvertDateFormat(DateOfIssue));
                sqlParams[7] = new SqlParameter("@CreatedBy", CreatedBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertWODuplicateShareHolderDetails]", sqlParams);
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
        /// Created Date : 3 September 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WODupliceteDetails by WOID
        /// </summary>
        /// <returns></returns>
        public static WODuplicateDetails GetWODuplicateDetailsByWOID(int WOID)
        {
            var Details = new WODuplicateDetails();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWODuplicateDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    Details.FetchWODuplicateDetails(Details, safe);
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

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 4 November 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WODuplicate ShareHolders details by WOID
        /// </summary>
        /// <returns></returns>
        public static WODupluicateDetailsInfo GetWODuplicateShareHoldersDetailsByWOID(int WOID)
        {
            var Details = new WODupluicateDetailsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWODuplicateShareholdersDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var data = new WODuplicateDetails();
                    data.FetchWODuplicateShareHoldersDetails(data, safe);
                    Details.WODuplicateList.Add(data);
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


        #region Events

        public class WODupluicateDetailsInfo
        {
            public List<WODuplicateDetails> WODuplicateList { set; get; }

            public WODupluicateDetailsInfo()
            {
                WODuplicateList = new List<WODuplicateDetails>();
            }
        }

        #endregion
    }


}