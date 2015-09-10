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
    public class _InterimDividend
    {
        private static ILog log = LogManager.GetLogger(typeof(_InterimDividend));

        #region Properties

        public int ID { set; get; }
        public int WOID { set; get; }
        public string FinancialPeriod { set; get; }
        public int ClassOfShare { set; get; }
        public string DividendPerShare { set; get; }
        public int Currency { set; get; }
        public string TotalAmount { set; get; }
        public string DateOfDeclaration { set; get; }
        public string DateOfPayment { set; get; }
        public int DividendDirector { set; get; }
        public string DividendSource { set; get; }
        public int SavedBy { set; get; }
        public string TotalShares { set; get; }
        public bool IsDividend { set; get; }
        #endregion

        #region Fetch methods

        internal _InterimDividend FetchInerimDividendByWOID(_InterimDividend objGetDetails, SafeDataReader dr)
        {
            objGetDetails.ID = dr.GetInt32("ID");
            objGetDetails.WOID = dr.GetInt32("WOID");
            objGetDetails.FinancialPeriod = dr.GetString("FinancialPeriod");
            objGetDetails.ClassOfShare = dr.GetInt32("ClassOfShare");
            objGetDetails.DividendPerShare = dr.GetString("DividendPerShare");
            objGetDetails.Currency = dr.GetInt32("Currency");
            objGetDetails.TotalAmount = dr.GetString("TotalAmount");
            objGetDetails.DateOfDeclaration = dr.GetString("DateOfDeclaration");
            objGetDetails.DateOfPayment = dr.GetString("DateOfPayment");
            objGetDetails.DividendDirector = dr.GetInt32("DividendDirector");
            objGetDetails.DividendSource = dr.GetString("DividendSource");
            objGetDetails.IsDividend = dr.GetBoolean("IsDividend");
            objGetDetails.TotalShares = dr.GetString("TotalNoOfShares");
            return objGetDetails;
        }

        #endregion

        #region Database Methods

        /// <summary>
        /// Description  : To Save All WOInterimDividend Details.
        /// Created By   : Shiva  
        /// Created Date : 22 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOInterimDividend Saved status.</returns>
        public int SaveWoInterimDividendDetails()
        {
            int SavedStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var WOAllotData = new AllotmentDetails();

                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@FinancialPeriod", HelperClasses.ConvertDateFormat(this.FinancialPeriod));
                //sqlParams[2] = new SqlParameter("@ClassOfShare", DBNull.Value);
                // sqlParams[3] = new SqlParameter("@DividendPerShare", this.DividendPerShare);
                //  sqlParams[5] = new SqlParameter("@TotalAmount", this.TotalAmount);
                sqlParams[2] = new SqlParameter("@DateOfDeclaration", HelperClasses.ConvertDateFormat(this.DateOfDeclaration));
                sqlParams[3] = new SqlParameter("@DateOfPayment", HelperClasses.ConvertDateFormat(this.DateOfPayment));
                sqlParams[4] = new SqlParameter("@DividendDirector", this.DividendDirector);
                sqlParams[5] = new SqlParameter("@DividendSource", this.DividendSource);
                sqlParams[6] = new SqlParameter("@SavedBy", this.SavedBy);
                // sqlParams[11] = new SqlParameter("@TotalShares", this.TotalShares);
                SavedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpSaveWOInterimDividendDetails", sqlParams);
                return SavedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return SavedStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Get All WOInterimDividend Details by WOID.
        /// Created By   : Shiva  
        /// Created Date : 22 Aug 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOInterimDividend details.</returns>
        public static _InterimDividend GetWOInerimDividendDetailsByWOID(int WOID)
        {
            var WOInterimDividendData = new _InterimDividend();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOInterimDividendDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOInterimDividendData.FetchInerimDividendByWOID(WOInterimDividendData, safe);
                }

                return WOInterimDividendData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOInterimDividendData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        public class InterimDividendInfo
        {
            internal List<_InterimDividend> InterimDividendList { set; get; }
            public InterimDividendInfo()
            {
                InterimDividendList = new List<_InterimDividend>();
            }
        }
        #endregion
    }
}