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
    public class WOTransferDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(WOTransferDetails));

        #region Properties

        public int WOID { get; set; }
        public int IssuedAndPaidUpCapitalCurrency { get; set; }
        public int IssuedAndPaidUpCapitalClassOfShare { get; set; }
        public bool IsPreEmptionRights { get; set; }
        public int LettertoIRAS { get; set; }
        public string LettertoIRASSource { get; set; }
        public string Transferor { get; set; }
        public string TransferorSource { get; set; }
        public string Transferee { get; set; }
        public string TransfereeSource { get; set; }
        public string SharesTransfered { get; set; }
        public string Consideration { get; set; }
        public int CreatedBy { get; set; }
        public int ID { set; get; }
        public string CurrencyName { set; get; }



        #endregion

        #region Fetch Methods

        private WOTransferDetails FetchWOTransferTransactionDetails(WOTransferDetails TransferTransactionDetails, SafeDataReader dr)
        {
            TransferTransactionDetails.ID = dr.GetInt32("ID");
            TransferTransactionDetails.WOID = dr.GetInt32("WOID");
            TransferTransactionDetails.Transferor = dr.GetString("Transferor");
            TransferTransactionDetails.Transferee = dr.GetString("Transferee");
            TransferTransactionDetails.SharesTransfered = dr.GetString("SharesTransfered");
            TransferTransactionDetails.CurrencyName = dr.GetString("CurrencyName");
            TransferTransactionDetails.Consideration = dr.GetString("Consideration");
            return TransferTransactionDetails;
        }
        private WOTransferDetails FetchWOTransferDetails(WOTransferDetails TrasferDetails, SafeDataReader dr)
        {
            // TrasferDetails.IssuedAndPaidUpCapitalCurrency = dr.GetInt32("Currency");
            TrasferDetails.IssuedAndPaidUpCapitalClassOfShare = dr.GetInt32("ClassOfShare");
            TrasferDetails.IsPreEmptionRights = dr.GetBoolean("IsPreEmptionRights");
            TrasferDetails.LettertoIRAS = dr.GetInt32("LettertoIRAS");
            TrasferDetails.LettertoIRASSource = dr.GetString("LettertoIRASSource");
            return TrasferDetails;
        }
        #endregion

        #region DataBase Methods

        /// <summary>
        /// Created By   : Pavan
        /// Created Date : 22 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Insert WOTransferDetails
        /// </summary>
        /// <returns></returns>
        public int InsertWOTransferDetails()
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                //    sqlParams[1] = new SqlParameter("@IssuedAndPaidUpCapitalCurrency", this.IssuedAndPaidUpCapitalCurrency);
                sqlParams[2] = new SqlParameter("@IssuedAndPaidUpCapitalClassOfShare", this.IssuedAndPaidUpCapitalClassOfShare);
                sqlParams[3] = new SqlParameter("@IsPreEmptionRights", this.IsPreEmptionRights);
                sqlParams[4] = new SqlParameter("@LettertoIRAS", this.LettertoIRAS);
                sqlParams[5] = new SqlParameter("@LettertoIRASSource", this.LettertoIRASSource);
                sqlParams[6] = new SqlParameter("@CreatedBy", this.CreatedBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWOTransferDetails]", sqlParams);
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
        /// Description  : Get WOTransferDetails by WOID
        /// </summary>
        /// <returns></returns>
        public static WOTransferDetails GetWOTransferDetailsByWOID(int WOID)
        {
            var Details = new WOTransferDetails();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOTransferDetailsByWOID]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    Details.FetchWOTransferDetails(Details, safe);
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
        /// Created By   : Shiva
        /// Created Date : 5th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Insert WO TransferT ransaction Details
        /// </summary>
        /// <returns></returns>
        public int InsertWOTransferTransactionDetails()
        {
            int output = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[9];
                sqlParams[0] = new SqlParameter("@WOID", this.WOID);
                sqlParams[1] = new SqlParameter("@Transferor", this.Transferor);
                sqlParams[2] = new SqlParameter("@TransferorSource", this.TransferorSource);
                sqlParams[3] = new SqlParameter("@Transferee", this.Transferee);
                sqlParams[4] = new SqlParameter("@TransfereeSource", this.TransfereeSource);
                sqlParams[5] = new SqlParameter("@SharesTransfered", this.SharesTransfered);
                sqlParams[6] = new SqlParameter("@Consideration", this.Consideration);
                sqlParams[7] = new SqlParameter("@Currency", this.IssuedAndPaidUpCapitalCurrency);
                sqlParams[8] = new SqlParameter("@SavededBy", this.CreatedBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPInsertWOTransferTransactionDetailsByWOID", sqlParams);
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
        /// Created By   : Shiva
        /// Created Date : 5 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get WOTransferTransactionDetails by WOID
        /// </summary>
        /// <returns></returns>
        public static WOTransferDetailsInfo GetWOTransferTransactionDetailsByWOID(int WOID)
        {
            var GetWOTransactionData = new WOTransferDetailsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOTransferTransactionDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Details = new WOTransferDetails();
                    Details.FetchWOTransferTransactionDetails(Details, safe);
                    GetWOTransactionData.WOTransferTransactionDetailsList.Add(Details);
                }
                return GetWOTransactionData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GetWOTransactionData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 5th Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Delete WO Transfer Transaction details by ID
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOTransferTransactionDetailsByID(int TransferID)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", TransferID);
                return result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPDeleteWOTransferTransactionDetailsByID", sqlParams);
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


        public class WOTransferDetailsInfo
        {
            public List<WOTransferDetails> WOTransferTransactionDetailsList { set; get; }
            public WOTransferDetailsInfo()
            {
                WOTransferTransactionDetailsList = new List<WOTransferDetails>();
            }
        }
    }
}