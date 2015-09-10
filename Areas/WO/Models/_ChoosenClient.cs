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

    public class _ChoosenClient
    {
        private static ILog log = LogManager.GetLogger(typeof(_ChoosenClient));

        #region Properties
        public string ClientName { set; get; }
        public string ClientCode { set; get; }
        public string SourceID { set; get; }
        public int UniqueID { set; get; }

        public int BillingPartyID  { set; get; }
        public string BillingPartySource { set; get; }

        #endregion

        private _ChoosenClient FetchClientInfo(_ChoosenClient getClientInfo, SafeDataReader dr)
        {
            getClientInfo.ClientCode = dr.GetString("ID");
            getClientInfo.ClientName = dr.GetString("Name");
            getClientInfo.SourceID = dr.GetString("SourceCode");
            getClientInfo.UniqueID = dr.GetInt32("UniqueID");

            return getClientInfo;
        }

        private _ChoosenClient FetchBillingPartyInfo(_ChoosenClient getClientInfo, SafeDataReader dr)
        {
            getClientInfo.ClientCode = dr.GetString("ID");
            getClientInfo.ClientName = dr.GetString("Name");
            getClientInfo.SourceID = dr.GetString("SourceCode");
            getClientInfo.UniqueID = dr.GetInt32("UniqueID");
          
            return getClientInfo;
        }      

        #region Billing Database methods

        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Shiva
        /// Created Date : 2nd July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static _ChoosenClientInfo GetClientInformation(string Type, string ClientName, string WOID, string ClientSource, string groupcode)
        {
            var GetClientInfo = new _ChoosenClientInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ClientType", Type);
                sqlParams[1] = new SqlParameter("@ClientName", ClientName);
                sqlParams[2] = new SqlParameter("@WOID", WOID);
                sqlParams[3] = new SqlParameter("@ClientSource", ClientSource);
                sqlParams[4] = new SqlParameter("@GroupCode", groupcode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetAllCompanyDetails", sqlParams);

                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var ClientInfo = new _ChoosenClient();
                    ClientInfo.FetchClientInfo(ClientInfo, safe);
                    GetClientInfo._ChoosenClientList.Add(ClientInfo);
                }

                return GetClientInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetClientInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
       
        /// <summary>
        /// Description  : Get the Client information from CSS1
        /// Created By   : Sudheer  
        /// Created Date : 14th Oct 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>   
        public static _ChoosenBillingPartyInfo GetBillingPartyInformation(string ClientName, string WOID)
        {
            var GetClientInfo = new _ChoosenBillingPartyInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@ClientName", ClientName);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "GetAllBillingPartyDetails", sqlParams);
                var safe = new SafeDataReader(reader);
               
                while (reader.Read())
                {
                    var ClientInfo = new _ChoosenClient();
                    ClientInfo.FetchBillingPartyInfo(ClientInfo, safe);
                    GetClientInfo._ChoosenBillingPartyList.Add(ClientInfo);
                }



                return GetClientInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetClientInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        ///// <summary>
        ///// Description  : Get the Client information from CSS1
        ///// Created By   : Shiva
        ///// Created Date : 5th Aug 2014
        ///// Modified By  :
        ///// Modified Date:
        ///// </summary>
        //public static _ChoosenClientInfo GetCSS1ClientDetailsBySearch(string ClientName, string ClientType)
        //{
        //    var GetClientInfo = new _ChoosenClientInfo();
        //    SqlParameter[] sqlParams = new SqlParameter[2];
        //    sqlParams[0] = new SqlParameter("@ClientName", ClientName);
        //    sqlParams[1] = new SqlParameter("@ClientType", ClientType); ;

        //    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCSS1ClientDetailsBySearch", sqlParams);
        //    var safe = new SafeDataReader(reader);
        //    while (reader.Read())
        //    {
        //        var ClientInfo = new _ChoosenClient();
        //        FetchClientInfo(ClientInfo, safe);
        //        GetClientInfo._ChoosenClientList.Add(ClientInfo);
        //    }

        //    return GetClientInfo;
        //}

        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : Shiva
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class _ChoosenClientInfo
        {
            public List<_ChoosenClient> _ChoosenClientList { get; set; }



            public _ChoosenClientInfo()
            {
                _ChoosenClientList = new List<_ChoosenClient>();
            }
        }


        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : sudheer
        /// Created Date : 15th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class _ChoosenBillingPartyInfo
        {
            public List<_ChoosenClient> _ChoosenBillingPartyList { get; set; }



            public _ChoosenBillingPartyInfo()
            {
                _ChoosenBillingPartyList = new List<_ChoosenClient>();
            }
        }
        #endregion
    }
}