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

    public class WOAddress
    {
        private static ILog log = LogManager.GetLogger(typeof(WOAddress));

        #region Properties

        public int PersonID { get; set; }
        public string PersonSource { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        #endregion

        #region Fetch Data

        private WOAddress FetchwoaddressDetails(WOAddress woaddress, SafeDataReader dr)
        {
            woaddress.Email = dr.GetString("Email");
            woaddress.Phone = dr.GetString("Phone");
            woaddress.Fax = dr.GetString("Fax");
            return woaddress;
        }

        #endregion

        #region Database Methods

        /// <summary>
        /// Description  : Get WOAddress Details from database.
        /// Created By   : Pavan
        /// Created Date : 12 August 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static List<WOAddress> GetWOAddressDetails(int PersonID, string PersonSource)
        {
            var data = new List<WOAddress>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@PersonID", PersonID);
                sqlParams[1] = new SqlParameter("@PersonSource", PersonSource);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOAddressDetails]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var woaddress = new WOAddress();
                    woaddress.FetchwoaddressDetails(woaddress, safe);
                    data.Add(woaddress);
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
        /// Description  : Insert or Update WOAddress Details
        /// Created By   : Pavan
        /// Created Date : 12 August 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public int InsertOrUpdateDirectorAddress(int createdBy)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@PersonID", PersonID);
                sqlParams[1] = new SqlParameter("@PersonSource", PersonSource);
                sqlParams[2] = new SqlParameter("@Email", Email);
                sqlParams[3] = new SqlParameter("@Phone", Phone);
                sqlParams[4] = new SqlParameter("@Fax", Fax);
                sqlParams[5] = new SqlParameter("@CreatedBy", createdBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWOAddressDetails]", sqlParams);
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
}