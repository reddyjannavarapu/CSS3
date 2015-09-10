using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSS2.Areas.Masters.Models
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using CSS2.Models;
    using CSS2.Areas.UserManagement.Models;
    using log4net;
    #endregion

    public partial class WOTypes
    {
        private static ILog log = LogManager.GetLogger(typeof(WOTypes));

        #region Properties
        public int WOTypeId { get; set; }
        public string WOName { get; set; }
        public string WOCategoryCode { get; set; }
        public string WOCode { get; set; }
        public string WODescription { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        #endregion

        #region Fetching Data
        private WOTypes FetchService(WOTypes wotypes, SafeDataReader dr)
        {
            wotypes.WOTypeId = dr.GetInt32("ID");
            wotypes.WOName = dr.GetString("Name");
            wotypes.WOCategoryCode = dr.GetString("CategoryCode");
            wotypes.WOCode = dr.GetString("Code");
            wotypes.WODescription = dr.GetString("Description");
            wotypes.Status = dr.GetBoolean("Status");
            return wotypes;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the WOType A
        /// </summary>    
        public static WOTypeInfo GetAllWOTypes(int startPage, int resultPerPage, string Name, int status, string OrderBy)
        {
            var data = new WOTypeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@Name", Name);
                sqlParams[3] = new SqlParameter("@status", status);
                sqlParams[4] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMWOType]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var wotype = new WOTypes();
                    wotype.FetchService(wotype, safe);

                    data.WOTypeList.Add(wotype);
                    data.wotypeCount = Convert.ToInt32(reader["wotypecount"]);
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
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the WOType A
        /// </summary>    
        public int InsertOrUpdateWOType()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@ID", this.WOTypeId);
                sqlParams[1] = new SqlParameter("@Code", this.WOCode);
                sqlParams[2] = new SqlParameter("@CategoryCode", this.WOCategoryCode);
                sqlParams[3] = new SqlParameter("@Name", this.WOName);
                sqlParams[4] = new SqlParameter("@Description", this.WODescription);
                sqlParams[5] = new SqlParameter("@Status", this.Status);
                sqlParams[6] = new SqlParameter("@CreatedBy", this.CreatedBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateMWOType]", sqlParams);
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
        /// Created By    : Sudheer
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the WOType A
        /// </summary>    
        public static WOTypes GetWOTypeById(int? id)
        {
            var wotype = new WOTypes();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@id", id);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOTypeById]", sqlParams);

                while (reader.Read())
                {
                    wotype.FetchService(wotype, new SafeDataReader(reader));
                }
                return wotype;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return wotype;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Created By    : Sudheer
        /// Created Date  : 15 Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// Description   : To Insert or Update the WOType A
        /// </summary>    
        public static int DeleteWOTypeById(int id)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@id", id);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteMWOTypeById]", sqlParams);
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

        #endregion

    }

    #region InfoClass
    /// <summary>
    /// Description  : To do all events in same view
    /// Created By   : Anji
    /// Created Date : 05 May 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class WOTypeInfo
    {
        public List<WOTypes> WOTypeList { get; set; }
        public int wotypeCount { get; set; }

        public WOTypeInfo()
        {
            WOTypeList = new List<WOTypes>();
        }
    }
    #endregion
}