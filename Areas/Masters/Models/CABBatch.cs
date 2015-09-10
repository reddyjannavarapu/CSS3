using CSS2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using log4net;

namespace CSS2.Areas.Masters.Models
{
    public partial class CABBatch
    {
        private static ILog log = LogManager.GetLogger(typeof(WOTypes));
        #region Properties

        public int ID { set; get; }
        public string Name { set; get; }
        public int BatchType { set; get; }
        public string BatchID { set; get; }
        public string BatchCode { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string FromDateList { set; get; }
        public string ToDateList { set; get; }
        public int SavedBy { set; get; }
        public int SNO { set; get; }
        public int Duration { set; get; }
        public string CreatedDate { set; get; }

        #endregion

        #region Fetch Methods
        private static CABBatch FetchBATCHType(CABBatch CABBatch, SafeDataReader dr)
        {
            CABBatch.BatchCode = dr.GetString("BatchCode");
            CABBatch.Name = dr.GetString("Name");
            return CABBatch;
        }

        private static CABBatch FetchBatchDetails(CABBatch CABBatch, SafeDataReader dr)
        {
            CABBatch.ID = dr.GetInt32("ID");
            CABBatch.Name = dr.GetString("Name");
            CABBatch.BatchCode = dr.GetString("BatchType");
            CABBatch.BatchID = dr.GetString("BatchID");
            CABBatch.FromDate = dr.GetDateTime("FromDate").ToString("dd/MM/yyyy");
            CABBatch.ToDate = dr.GetDateTime("ToDate").ToString("dd/MM/yyyy");
            CABBatch.FromDateList = CABBatch.FromDate == "" ? "" : dr.GetDateTime("FromDate").ToString("dd MMM yyyy");
            CABBatch.ToDateList = CABBatch.ToDate == "" ? "" : dr.GetDateTime("ToDate").ToString("dd MMM yyyy");
            CABBatch.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt"); ;
            return CABBatch;
        }

        public CABBatch FetchGapInfo(CABBatch GapInfo, SafeDataReader dr)
        {
            GapInfo.SNO = dr.GetInt32("new_id");
            GapInfo.FromDate = dr.GetDateTime("start_date").ToString("dd MMM yyyy");
            GapInfo.ToDate = dr.GetDateTime("end_date").ToString("dd MMM yyyy");
            GapInfo.Duration = dr.GetInt32("NoofDays");

            return GapInfo;
        }
        #endregion

        #region Database Methods


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Get the Batch Type
        /// </summary>        
        public static CABBatchInfo GetMBatchType()
        {
            var data = new CABBatchInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetMBatchType");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var CABBatch = new CABBatch();
                    CABBatch.FetchBATCHType(CABBatch, safe);
                    data.CABBatchList.Add(CABBatch);
                }
                return data;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return data;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Save Cab Batch details.
        /// </summary>    
        public int SaveCabBatchDetails()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@BatchType", this.BatchCode);
                sqlParams[2] = new SqlParameter("@BatchID", this.BatchID);
                sqlParams[3] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(this.FromDate));
                sqlParams[4] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(this.ToDate));
                sqlParams[5] = new SqlParameter("@CreatedBy", this.SavedBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveCabBatchDetails", sqlParams);
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
        /// Description   : To Get Company Data
        /// Created By    : Pavan
        /// Created Date  : 30 September 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static CABBatchInfo GetCabBatchDetails(string BatchType, string OrderBy, int startPage, int resultPerPage)
        {
            var data = new CABBatchInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@BatchType", BatchType);
                sqlParams[3] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCabBatchDetails", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var CABBatch = new CABBatch();
                    CABBatch.FetchBatchDetails(CABBatch, safe);
                    data.CABBatchList.Add(CABBatch);
                    data.CABBatchCount = Convert.ToInt32(reader["BatchCount"]);
                }
                return data;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return data;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 19 Nov 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Delete Cab Batch Details By ID.
        /// </summary>        
        public static int DeleteCabBatchDetailsByID(int ID)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);

                return ID = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPDeleteCabBatchDetailsByID", sqlParams);

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ID;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        #endregion

        #region InfoClass
        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : Shiva
        /// Created Date : 19 Nov 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class CABBatchInfo
        {
            public List<CABBatch> CABBatchList { get; set; }
            public int CABBatchCount { get; set; }

            public CABBatchInfo()
            {
                CABBatchList = new List<CABBatch>();
            }
        }
        #endregion

        /// <summary>
        /// Description  : Gap analysis by BatchType.
        /// Created By   : Shiva
        /// Created Date : 22 Dec 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static CABBatchInfo GetBabBatchGapByBatchType(string BatchType)
        {
            var GetGapInfo = new CABBatchInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@BatchType", BatchType);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCabBatchGapAnalysisByBatchType_Club", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var GapInfo = new CABBatch();
                    GapInfo.FetchGapInfo(GapInfo, safe);
                    GetGapInfo.CABBatchList.Add(GapInfo);
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




    }
}