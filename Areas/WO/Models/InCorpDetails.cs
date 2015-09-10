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
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System;
    using log4net;
    #endregion

    public class InCorpDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(InCorpDetails));

        #region Properties

        public int WOID { get; set; }
        public string CompanyName { get; set; }
        public string ClientNo { get; set; }
        public string IncropDate { get; set; }
        public string RegistrationNo { get; set; }
        public string TypeofCompany { get; set; }
        public bool FMGClient { set; get; }
        public int ClientType { set; get; }
        public int IncorporationCountry { set; get; }
        public int CompanyStatus { set; get; }
        public bool FMGasRegisteredAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string FinancialYearEnd { get; set; }
        public string FirstFinancialYearEnd { get; set; }
        public string Currency { get; set; }
        public string ClassofShare { get; set; }
        public decimal PaidupCapital { get; set; }
        public decimal AmountPaidupperShare { get; set; }
        public string ActivityOne { get; set; }
        public string ActivityOneDescription { get; set; }
        public string ActivityTwo { get; set; }
        public string ActivityTwoDescription { get; set; }


        public decimal AmountGuaranteedByEachMember { get; set; }
        public string GuaranteedAmountCurrency { get; set; }
        public int ClientID { set; get; }
        public string ClientSource { set; get; }


        #endregion

        #region Fetch Data

        private InCorpDetails FetchWOInCorp(InCorpDetails WOIncorpData, SafeDataReader dr)
        {
            WOIncorpData.WOID = dr.GetInt32("WOID");
            WOIncorpData.CompanyName = dr.GetString("CompanyName");
            WOIncorpData.ClientNo = dr.GetString("ClientNo");
            WOIncorpData.ClientID = dr.GetInt32("ClientID");
            WOIncorpData.ClientSource = dr.GetString("ClientSource");
            WOIncorpData.IncropDate = dr.GetDateTime("IncropDate").ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dr.GetDateTime("IncropDate").ToString("dd/MM/yyyy");
            WOIncorpData.RegistrationNo = dr.GetString("RegistrationNo");
            WOIncorpData.TypeofCompany = dr.GetString("TypeofCompany");
            WOIncorpData.FMGasRegisteredAddress = dr.GetBoolean("FMGasRegisteredAddress");
            WOIncorpData.AddressLine1 = dr.GetString("AddressLine1");
            WOIncorpData.AddressLine2 = dr.GetString("AddressLine2");
            WOIncorpData.AddressLine3 = dr.GetString("AddressLine3");
            WOIncorpData.Country = dr.GetString("Country");
            WOIncorpData.PostalCode = dr.GetString("PostalCode");
            WOIncorpData.FinancialYearEnd = dr.GetDateTime("FinancialYearEnd").ToString("dd MMMM"); //== "01 January" ? "" : dr.GetDateTime("FinancialYearEnd").ToString("dd MMMM");
            WOIncorpData.FirstFinancialYearEnd = dr.GetDateTime("FirstFinancialYearEnd").ToString("dd MMMM yyyy") == "01 January 0001" ? "" : dr.GetDateTime("FirstFinancialYearEnd").ToString("dd MMMM yyyy");
            WOIncorpData.Currency = dr.GetString("Currency");
            WOIncorpData.ClassofShare = dr.GetString("ClassofShare");
            WOIncorpData.PaidupCapital = dr.GetDecimal("PaidupCapital");
            WOIncorpData.AmountPaidupperShare = dr.GetDecimal("AmountPaidupperShare");
            WOIncorpData.ActivityOne = dr.GetString("ActivityOne");
            WOIncorpData.ActivityOneDescription = dr.GetString("ActivityOneDescription");
            WOIncorpData.ActivityTwo = dr.GetString("ActivityTwo");
            WOIncorpData.ActivityTwoDescription = dr.GetString("ActivityTwoDescription");

            WOIncorpData.AmountGuaranteedByEachMember = dr.GetDecimal("AmountGuaranteedByEachMember");
            WOIncorpData.GuaranteedAmountCurrency = dr.GetString("GuaranteedAmountCurrency");

            WOIncorpData.FMGClient = dr.GetBoolean("FMGClient");
            WOIncorpData.ClientType = dr.GetInt32("ClientType");
            WOIncorpData.IncorporationCountry = dr.GetInt32("IncorporationCountry");
            WOIncorpData.CompanyStatus = dr.GetInt32("CompanyStatus");

            return WOIncorpData;
        }

        #endregion

        #region DataBase Methods
        /// <summary>
        /// Description  : To Insert Work Order INCorpDetails.
        /// Created By   : Sudheer  
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOInCorpDetails(InCorpDetails WOInCorpDetails, int CreatedBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[35];
                sqlParams[0] = new SqlParameter("@WOID", WOInCorpDetails.WOID);
                sqlParams[1] = new SqlParameter("@CompanyName", WOInCorpDetails.CompanyName);
                sqlParams[2] = new SqlParameter("@ClientNo", WOInCorpDetails.ClientNo);
                if (WOInCorpDetails.IncropDate == null)
                    sqlParams[3] = new SqlParameter("@IncropDate", DBNull.Value);
                else
                    sqlParams[3] = new SqlParameter("@IncropDate", HelperClasses.ConvertDateFormat(WOInCorpDetails.IncropDate));
                sqlParams[4] = new SqlParameter("@RegistrationNo", WOInCorpDetails.RegistrationNo);
                sqlParams[5] = new SqlParameter("@TypeofCompany", WOInCorpDetails.TypeofCompany);
                sqlParams[6] = new SqlParameter("@FMGasRegisteredAddress", WOInCorpDetails.FMGasRegisteredAddress);
                sqlParams[7] = new SqlParameter("@AddressLine1", WOInCorpDetails.AddressLine1);
                sqlParams[8] = new SqlParameter("@AddressLine2", WOInCorpDetails.AddressLine2);
                sqlParams[9] = new SqlParameter("@AddressLine3", WOInCorpDetails.AddressLine3);
                sqlParams[10] = new SqlParameter("@Country", WOInCorpDetails.Country);
                sqlParams[11] = new SqlParameter("@PostalCode", WOInCorpDetails.PostalCode);
                if (WOInCorpDetails.FinancialYearEnd == "")
                    sqlParams[12] = new SqlParameter("@FinancialYearEnd", DBNull.Value);
                else
                    sqlParams[12] = new SqlParameter("@FinancialYearEnd", WOInCorpDetails.FinancialYearEnd);

                if (WOInCorpDetails.FirstFinancialYearEnd == "")
                    sqlParams[13] = new SqlParameter("@FirstFinancialYearEnd", DBNull.Value);
                else
                    sqlParams[13] = new SqlParameter("@FirstFinancialYearEnd", WOInCorpDetails.FirstFinancialYearEnd);
                sqlParams[14] = new SqlParameter("@Currency", WOInCorpDetails.Currency);
                sqlParams[15] = new SqlParameter("@ClassofShare", WOInCorpDetails.ClassofShare);
                sqlParams[16] = new SqlParameter("@PaidupCapital", WOInCorpDetails.PaidupCapital);
                sqlParams[17] = new SqlParameter("@AmountPaidupperShare", WOInCorpDetails.AmountPaidupperShare);
                sqlParams[18] = new SqlParameter("@ActivityOne", WOInCorpDetails.ActivityOne);
                sqlParams[19] = new SqlParameter("@ActivityOneDescription", WOInCorpDetails.ActivityOneDescription);
                sqlParams[20] = new SqlParameter("@ActivityTwo", WOInCorpDetails.ActivityTwo);
                sqlParams[21] = new SqlParameter("@ActivityTwoDescription", WOInCorpDetails.ActivityTwoDescription);
                sqlParams[22] = new SqlParameter("@CreatedBy", CreatedBy);

                sqlParams[29] = new SqlParameter("@AmountGuaranteedByEachMember", WOInCorpDetails.AmountGuaranteedByEachMember);
                sqlParams[30] = new SqlParameter("@GuaranteedAmountCurrency", WOInCorpDetails.GuaranteedAmountCurrency);

                sqlParams[31] = new SqlParameter("@FMGClient ", WOInCorpDetails.FMGClient);
                sqlParams[32] = new SqlParameter("@ClientType", WOInCorpDetails.ClientType);
                sqlParams[33] = new SqlParameter("@IncorporationCountry", WOInCorpDetails.IncorporationCountry);
                sqlParams[34] = new SqlParameter("@CompanyStatus", WOInCorpDetails.CompanyStatus);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertOrUpdateWOIncorpDetails]", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOINCORPFirstSubscribers(string WOID, string personid, string sourcecode, string occupation, string NoOfSharesHeld, string TotalAmountPaid, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@Occupation", occupation);
                sqlParams[4] = new SqlParameter("@NoOfSharesHeld", NoOfSharesHeld);
                sqlParams[5] = new SqlParameter("@TotalAmountPaid", TotalAmountPaid);
                sqlParams[6] = new SqlParameter("@CreatedBy", createdBy);
                // sqlParams[7] = new SqlParameter("@Currency", Currency);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertIncorpFirstSubscribersDetails]", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOINCORPAuthorizedPersonFS(string WOID, string personid, string sourcecode, string FSID, int createdBy)
        {
            int output = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                sqlParams[4] = new SqlParameter("@CreatedBy", createdBy);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertIncorpAuthorizedPersonFSDetails]", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOINCORPAuthorizedPersonPrincipalDetails(string WOID, string personid, string sourcecode, string FSID, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                sqlParams[4] = new SqlParameter("@CreatedBy", createdBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertIncorpAuthorizedPersonPrincipalDetails]", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOINCORPNomineeDirectorsDetails(string WOID, string personid, string sourcecode, string FSID, string FeeDueNominee, string SharesHeldNominee, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                sqlParams[4] = new SqlParameter("@FeeDueNominee", FeeDueNominee);
                sqlParams[5] = new SqlParameter("@SharesHeldNominee", SharesHeldNominee);
                sqlParams[6] = new SqlParameter("@CreatedBy", createdBy);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertIncorpNomineeDirectorsDetails]", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertWOINCORPPrincipalDetails(string WOID, string personid, string sourcecode, string NDPersonId, string NDSourceCode, string ContactPerson, int createdBy)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@NDPersonId", NDPersonId);
                sqlParams[4] = new SqlParameter("@NDSourceCode", NDSourceCode);
                sqlParams[5] = new SqlParameter("@ContactPerson", ContactPerson);
                sqlParams[6] = new SqlParameter("@CreatedBy", createdBy);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertIncorpPrincipalDetails]", sqlParams);
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
        /// Description  :To Get Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static DataSet GetWOINCORPFirstSubscribers(string WOID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOIncorpFirstSubscribersDetails", sqlParams);
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
        /// Description  :To Get Work Order INCorp AuthorisedFirstSubscribers Details.
        /// Created By   : Sudheer  
        /// Created Date : 4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static DataSet GetWOINCORPAuthorisedFirstSubscribers(string WOID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetIncorpAuthorisedFirstSubscribersDetails", sqlParams);
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
        /// Description  :To Get Work Order INCorp AuthorisedFirstSubscribers Details.
        /// Created By   : Sudheer  
        /// Created Date : 4th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static DataSet GetWOINCORPAuthorizedPersonPrincipalDetails(string WOID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOINCORPAuthorizedPersonPrincipalDetails", sqlParams);
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
        /// Description  :To Get Work Order INCorp PrincipalDetails Details.
        /// Created By   : Sudheer  
        /// Created Date : 5th sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static DataSet GetWOINCORPPrincipalDetails(string WOID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetINCORPPrincipalDetails", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOINCORPFirstSubscribers(string WOID, string personid, string sourcecode, string FSID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDelteIncorpFirstSubscribersDetails", sqlParams);
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
        /// Description  : To Insert Work Order INCorp FirstSubscriber Details.
        /// Created By   : Sudheer  
        /// Created Date : 3rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOINCORPAuthorizedPersonFSDetails(string WOID, string personid, string sourcecode, string FSID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDelteIncorpAuthorizedPersonFSDetails", sqlParams);
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
        /// Description  : To delete Work Order INCorp principal Details.
        /// Created By   : Sudheer  
        /// Created Date : 4rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOINCORPPrincipalDetails(string WOID, string personid, string sourcecode, string FSID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                sqlParams[3] = new SqlParameter("@FSID", FSID);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDelteIncorpPrincipalDetails", sqlParams);
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
        /// Description  : To delete Work Order INCorp principal Details.
        /// Created By   : Sudheer  
        /// Created Date : 4rd sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteWOINCORPAuthorizedPersonPrincipalDetails(string WOID, string personid, string sourcecode, string FSID)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@Personid", personid);
                sqlParams[2] = new SqlParameter("@Sourcecode", sourcecode);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDelteIncorpAuthorizedPersonPrincipalDetails", sqlParams);
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
        /// Description  : To Get Work Order INCorpDetails.
        /// Created By   : Sudheer  
        /// Created Date : 30 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static InCorpDetails GetWOIncorpDetails(string WOID)
        {
            var WOIncorpData = new InCorpDetails();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOIncorpDetails]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WOIncorpData.FetchWOInCorp(WOIncorpData, safe);
                }

                //safe.NextResult();
                //while (reader.Read())
                //{
                //    WOIncorpData.CompanyName = Convert.ToString(safe["CompanyName"]);
                //    WOIncorpData.RegistrationNo = Convert.ToString(safe["RegistrationNo"]);
                //}

                return WOIncorpData;

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOIncorpData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion
    }
}