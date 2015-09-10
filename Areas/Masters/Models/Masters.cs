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

namespace CSS2.Areas.Masters.Models
{

    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    public class Masters
    {
        private static ILog log = LogManager.GetLogger(typeof(Masters));

        #region Country

        public class Country
        {

            #region Properties

            public int CountryID { get; set; }
            public string CountryName { get; set; }
            public string Nationality { get; set; }
            #endregion

            #region Fetch Data

            private Country FetchPartialContent(Country country, SafeDataReader dr)
            {
                country.CountryID = dr.GetInt32("CountryID");
                country.CountryName = dr.GetString("CountryName");
                country.Nationality = dr.GetString("Nationality");
                return country;
            }

            #endregion


            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Country Details from database.
            /// Created By   : Pavan
            /// Created Date : 22 July 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<Country> GetCountryDetails()
            {
                var data = new List<Country>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetCSS1CountryDetails]");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var country = new Country();
                        country.FetchPartialContent(country, safe);
                        data.Add(country);
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

            #endregion




        }

        #endregion

        #region Currency

        public class Currency
        {

            #region Properties

            public int CurrencyID { get; set; }
            public string CurrencyName { get; set; }

            #endregion

            #region Fetch Data

            private Currency FetchPartialCurrencyContent(Currency currency, SafeDataReader dr)
            {
                currency.CurrencyID = dr.GetInt32("CurrencyID");
                currency.CurrencyName = dr.GetString("CurrencyName");
                return currency;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Country Details from database.
            /// Created By   : Sudheer
            /// Created Date : 30 July 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<Currency> GetCurrencyDetails()
            {
                var data = new List<Currency>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCSS1CurrencyDetails");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var currency = new Currency();
                        currency.FetchPartialCurrencyContent(currency, safe);
                        data.Add(currency);
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

            #endregion

        }

        #endregion

        #region Company Type

        public class CompanyType
        {

            #region Properties

            public int TypeID { get; set; }
            public string TypeName { get; set; }

            #endregion

            #region Fetch Data

            private CompanyType FetchPartialCompanyTypeContent(CompanyType companyType, SafeDataReader dr)
            {
                companyType.TypeID = dr.GetInt32("TypeID");
                companyType.TypeName = dr.GetString("TypeName");
                return companyType;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Company Type Details from database.
            /// Created By   : Sudheer
            /// Created Date : 30 July 2014
            /// Modified By  : Pavan
            /// Modified Date: added IsIncorp Parameter For IncorpCompanyType
            /// </summary>
            /// <returns></returns>
            public static List<CompanyType> GetCompanyTypeDetails(int IsIncorp)
            {
                var data = new List<CompanyType>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@IsIncorp", IsIncorp);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetCSS1CompanyTypeDetails]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var companyType = new CompanyType();
                        companyType.FetchPartialCompanyTypeContent(companyType, safe);
                        data.Add(companyType);
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

            #endregion

        }

        #endregion



        #region ClientType

        public class ClientType
        {

            #region Properties

            public int ClientTypeID { get; set; }
            public string ClientTypeName { get; set; }

            #endregion

            #region Fetch Data

            private ClientType FetchClientType(ClientType Client, SafeDataReader dr)
            {
                Client.ClientTypeID = dr.GetInt32("ClientTypeID");
                Client.ClientTypeName = dr.GetString("ClientTypeName");
                return Client;
            }
            #endregion

            /// <summary>
            /// Description  : Get Company Type from database.
            /// Created By   : Shiva
            /// Created Date : 24 Nov 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<ClientType> GetClientType()
            {
                var data = new List<ClientType>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCSS1ClientType");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var ClientType = new ClientType();
                        ClientType.FetchClientType(ClientType, safe);
                        data.Add(ClientType);
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



        }

        #endregion


        #region CompanyStatus

        public class CompanyStatus
        {

            #region Properties

            public int StatusID { get; set; }
            public string Status { get; set; }

            #endregion

            #region Fetch Data

            private CompanyStatus FetchCompanyStatus(CompanyStatus CompanyStatus, SafeDataReader dr)
            {
                CompanyStatus.StatusID = dr.GetInt32("StatusID");
                CompanyStatus.Status = dr.GetString("Status");
                return CompanyStatus;
            }

            #endregion
            /// <summary>
            /// Description  : Get Company Type from database.
            /// Created By   : Shiva
            /// Created Date : 24 Nov 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<CompanyStatus> GetCompanyStatus()
            {
                var data = new List<CompanyStatus>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCSS1CompanyStatus");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var CompanyStatus = new CompanyStatus();
                        CompanyStatus.FetchCompanyStatus(CompanyStatus, safe);
                        data.Add(CompanyStatus);
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



        }

        #endregion


        #region Class of Share

        public class ShareClass
        {

            #region Properties

            public int ShareClassID { get; set; }
            public string ClassName { get; set; }

            #endregion

            #region Fetch Data

            private ShareClass FetchPartialShareClassContent(ShareClass shareClass, SafeDataReader dr)
            {
                shareClass.ShareClassID = dr.GetInt32("ShareClassID");
                shareClass.ClassName = dr.GetString("ClassName");
                return shareClass;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Share Class Details from database.
            /// Created By   : Sudheer
            /// Created Date : 30 July 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<ShareClass> GetShareClassDetails(int WOID)
            {
                var data = new List<ShareClass>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetCSS1ShareClassDetails]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var shareClass = new ShareClass();
                        shareClass.FetchPartialShareClassContent(shareClass, safe);
                        data.Add(shareClass);
                    }
                    if (data.Count == 0)
                    {
                        data.Add(new ShareClass { ShareClassID = -1, ClassName = "Select" });
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

            #endregion

        }

        #endregion

        #region Directors

        public class Directors
        {

            #region Properties

            public int ID { get; set; }
            public string Name { get; set; }
            public string SourceCode { get; set; }
            public int RowId { get; set; }

            #endregion

            #region Fetch Data

            private Directors FetchDirectorsDetails(Directors Directors, SafeDataReader dr)
            {
                Directors.ID = dr.GetInt32("ID");
                Directors.Name = dr.GetString("Name");
                Directors.SourceCode = dr.GetString("SourceCode");
                Directors.RowId = dr.GetInt32("RowId");
                return Directors;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Directors Details from database.
            /// Created By   : Pavan
            /// Created Date : 31 July 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static DirectorsInfo GetDirectorsDetails(string SearchType, string SearchKeyWord, string SearchRole, int WOID, string NatureAppoint, int ClassOfShare)
            {
                var data = new DirectorsInfo();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[6];
                    sqlParams[0] = new SqlParameter("@SearchType", SearchType);
                    sqlParams[1] = new SqlParameter("@WOID", WOID);
                    sqlParams[2] = new SqlParameter("@SearchRole", SearchRole);
                    sqlParams[3] = new SqlParameter("@SearchKeyWord", SearchKeyWord);
                    sqlParams[4] = new SqlParameter("@NatureAppoint", NatureAppoint);
                    sqlParams[5] = new SqlParameter("@ClassOfShare", ClassOfShare);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllDirectorDetails]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var directors = new Directors();
                        directors.FetchDirectorsDetails(directors, safe);
                        data.DirectorsList.Add(directors);
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



            #endregion

        }

        #region DirectorsInfo class

        /// <summary>
        /// Description  : To do all events
        /// Created By   : Pavan
        /// Created Date : 31 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class DirectorsInfo
        {
            public List<Directors> DirectorsList { get; set; }
            public DirectorsInfo()
            {
                DirectorsList = new List<Directors>();
            }
        }

        #endregion

        #endregion




        #region WOInformationDetails

        public class WOInformationDetails
        {
            #region DataBaseMethods

            /// <summary>
            /// Description  : WO Information Details from database.
            /// Created By   : Sudheer  
            /// Created Date : 31 July 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static DataSet GetWOInformationDetails(string WOID, string GridCode)
            {
                DataSet ds = new DataSet();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    sqlParams[1] = new SqlParameter("@InfoCode", GridCode);

                    ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetWOInformationDetails]", sqlParams);
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
            /// Description  : To Delete WOInformationdetails By WOID, InfoCode, PersonID, PersonSource
            /// Created By   : Sudheer
            /// Created Date : 8th Aug 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <param name="serviceId">pass the WOID, InfoCode, PersonID, PersonSource</param>
            /// <returns></returns>
            public static int DeleteWOInformationdetails(int WOID, string InfoCode, int PersonID, string PersonSource)
            {
                int result = -2;
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@InfoCode", InfoCode);
                sqlParams[2] = new SqlParameter("@PersonID", PersonID);
                sqlParams[3] = new SqlParameter("@PersonSource", PersonSource);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteWOInformationdetails", sqlParams);

                return result;
            }

            #endregion
        }

        #endregion

        #region Auditors Status

        public class AuditorsStatus
        {

            #region Properties

            public int ID { get; set; }
            public string Name { get; set; }

            #endregion

            #region Fetch Data

            private AuditorsStatus FetchAuditorsStatus(AuditorsStatus objAuditorsStatus, SafeDataReader dr)
            {
                objAuditorsStatus.ID = dr.GetInt32("ID");
                objAuditorsStatus.Name = dr.GetString("Name");
                return objAuditorsStatus;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get AuditorsStatus Details from database.
            /// Created By   : Pavan
            /// Created Date : 11 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<AuditorsStatus> GetAuditorsStatusDetails()
            {
                var data = new List<AuditorsStatus>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMAuditorsStatus]");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var AuditorsStatus = new AuditorsStatus();
                        AuditorsStatus.FetchAuditorsStatus(AuditorsStatus, safe);
                        data.Add(AuditorsStatus);
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

            #endregion

        }

        #endregion

        #region Share Holding Structure

        public class ShareHoldingStructure
        {

            #region Properties

            public int ID { get; set; }
            public string Name { get; set; }
            public int ShareHolderID { get; set; }
            public int CmpyID { get; set; }
            public string ShareHolderName { get; set; }
            public string PersonSource { get; set; }
            public int WOID { get; set; }
            public decimal SharesHeld { get; set; }
            public string NetCashDividend { get; set; }
            public string NoOfBonusShareToIssue { set; get; }
            public int ShareClassID { get; set; }
            public string ClassName { get; set; }

            #endregion

            #region Fetch Data

            private ShareHoldingStructure FetchShareHoldingStructure(ShareHoldingStructure objShareHoldingStructure, SafeDataReader dr)
            {
                objShareHoldingStructure.ID = dr.GetInt32("ID");
                objShareHoldingStructure.Name = dr.GetString("Name");
                return objShareHoldingStructure;
            }

            private ShareHoldingStructure FetchShareHolersList(ShareHoldingStructure objShareHoldingStructure, SafeDataReader dr)
            {
                objShareHoldingStructure.ID = dr.GetInt32("ID");
                objShareHoldingStructure.CmpyID = dr.GetInt32("CmpyID");
                objShareHoldingStructure.ShareHolderID = dr.GetInt32("ShareHolderID");
                objShareHoldingStructure.ShareHolderName = dr.GetString("ShareHolderName");
                objShareHoldingStructure.PersonSource = dr.GetString("PersonSource");
                objShareHoldingStructure.WOID = dr.GetInt32("WOID");
                objShareHoldingStructure.SharesHeld = dr.GetDecimal("SharesHeld");
                objShareHoldingStructure.NetCashDividend = dr.GetString("NetCashDividend");
                objShareHoldingStructure.NoOfBonusShareToIssue = dr.GetString("NoOfBonusShareToIssue");
                objShareHoldingStructure.ShareClassID = dr.GetInt32("ShareClassID");
                objShareHoldingStructure.ClassName = dr.GetString("ClassName");

                return objShareHoldingStructure;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get ShareHoldingStructure Details from database.
            /// Created By   : Pavan
            /// Created Date : 11 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<ShareHoldingStructure> GetShareHoldingStructureDetails()
            {
                var data = new List<ShareHoldingStructure>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMShareHoldingStructure]");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var ShareHoldingStructure = new ShareHoldingStructure();
                        ShareHoldingStructure.FetchShareHoldingStructure(ShareHoldingStructure, safe);
                        data.Add(ShareHoldingStructure);
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

            public static ShareHoldingStructureinfo GetShareholdersByWOID(int WOID, int ShareClassID)
            {
                var data = new ShareHoldingStructureinfo();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {

                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    sqlParams[1] = new SqlParameter("@ShareClassID", ShareClassID);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetShareHoldersListByWOID", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var ShareHoldingStructure = new ShareHoldingStructure();
                        ShareHoldingStructure.FetchShareHolersList(ShareHoldingStructure, safe);
                        data.shereHoldersList.Add(ShareHoldingStructure);
                    }
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

                return data;
            }


            #endregion

            public class ShareHoldingStructureinfo
            {
                public List<ShareHoldingStructure> shereHoldersList { get; set; }
                public ShareHoldingStructureinfo()
                {
                    shereHoldersList = new List<ShareHoldingStructure>();
                }
            }

            public static int InsertShareholdersDetailsWOID(List<ShareHolderDevidentDetails> ShareDividendFields, bool IsDividend, string DividendPerShare, string DividentShareCurrency, string TotalNetAmount, string TotalNoShare, int WOID, int CreatedBy, string WOTypeName, int ClassofShare)
            {
                int output = -2;

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    DataTable dtShareHolders = HelperClasses.ListToDataTable<ShareHolderDevidentDetails>(ShareDividendFields);

                    //@TBLShareHolder AS UDT
                    //@IsDivident bit,
                    //@DividendPerShare deci
                    //@DividentShareCurrency
                    //@TotalNetAmount decima
                    //@WOID int,
                    //@CreatedBy int
                    int length = 0;
                    if (WOTypeName == "AGM")
                    {
                        length = 9;
                    }
                    else if (WOTypeName == "InterimDividend")
                    {
                        length = 10;
                    }
                    SqlParameter[] sqlParams = new SqlParameter[length];
                    sqlParams[0] = new SqlParameter("@TBLShareHolder", SqlDbType.Structured)
                    {
                        Value = dtShareHolders
                    };
                    sqlParams[1] = new SqlParameter("@IsDivident", IsDividend);
                    sqlParams[2] = new SqlParameter("@DividendPerShare", DividendPerShare);
                    sqlParams[3] = new SqlParameter("@DividentShareCurrency", DividentShareCurrency);
                    sqlParams[4] = new SqlParameter("@TotalNetAmount", TotalNetAmount);
                    sqlParams[5] = new SqlParameter("@TotalNoShare", TotalNoShare);
                    sqlParams[6] = new SqlParameter("@WOID", WOID);
                    sqlParams[7] = new SqlParameter("@CreatedBy", CreatedBy);
                    sqlParams[8] = new SqlParameter("@Output", 0);
                    sqlParams[8].Direction = ParameterDirection.Output;
                    if (WOTypeName == "AGM")
                    {
                        var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertWOShareHoldersDivident", sqlParams);
                    }
                    else if (WOTypeName == "InterimDividend")
                    {
                        sqlParams[9] = new SqlParameter("@ClassofShare", ClassofShare);
                        var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertWOShareHoldersDividendAndInterimDividendDetails", sqlParams);
                    }
                    output = Convert.ToInt32(sqlParams[8].Value);
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


            public static int InsertFromWoBonusIssueAndShareholdersDetailsWOID(List<ShareHolderDevidentDetails> ShareDividendFields, string ConsiderationOfEachShare, string TotalNoOfNewSharesToBeAllotted, string Currency, string TotalNetAmount, int WOID, int CreatedBy, int ClassofShare)
            {
                int output = -2;

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    if (TotalNoOfNewSharesToBeAllotted == "")
                    {
                        TotalNoOfNewSharesToBeAllotted = "0";
                    }

                    if (TotalNetAmount == "")
                    {
                        TotalNetAmount = "0";
                    }

                    DataTable dtShareHolders = HelperClasses.ListToDataTable<ShareHolderDevidentDetails>(ShareDividendFields);

                    SqlParameter[] sqlParams = new SqlParameter[9];
                    sqlParams[0] = new SqlParameter("@TBLShareHolder", SqlDbType.Structured)
                    {
                        Value = dtShareHolders
                    };
                    sqlParams[1] = new SqlParameter("@ConsiderationOfEachShare", ConsiderationOfEachShare);
                    sqlParams[2] = new SqlParameter("@TotalNoOfNewSharesToBeAllotted", TotalNoOfNewSharesToBeAllotted);
                    sqlParams[3] = new SqlParameter("@Currency", Currency);
                    sqlParams[4] = new SqlParameter("@TotalNetAmount", TotalNetAmount);
                    sqlParams[5] = new SqlParameter("@WOID", WOID);
                    sqlParams[6] = new SqlParameter("@CreatedBy", CreatedBy);
                    sqlParams[7] = new SqlParameter("@Output", 0);
                    sqlParams[7].Direction = ParameterDirection.Output;
                    sqlParams[8] = new SqlParameter("@ClassofShare", ClassofShare);

                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertWOShareHoldersDividendAndBonusIssueDetails", sqlParams);
                    output = Convert.ToInt32(sqlParams[7].Value);
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

            public static int DeleteShareholdersDetails(string WOShareHoldersDividentID)
            {
                int output = -2;

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[3];
                    sqlParams[0] = new SqlParameter("@WOShareHoldersDividentID", WOShareHoldersDividentID);
                    sqlParams[1] = new SqlParameter("@Output", 0);
                    sqlParams[1].Direction = ParameterDirection.Output;

                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteWOShareHoldersDivident", sqlParams);
                    output = Convert.ToInt32(sqlParams[1].Value);
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
        }

        public class ShareHolderDevidentDetails
        {
            public int PersonId { get; set; }
            public string PersonSource { get; set; }
            public int WOID { get; set; }
            public decimal SharesHeld { get; set; }
            public string NetCashDividend { get; set; }
            public string NoOfBonusShareToIssue { set; get; }
            public int ClassofShare { get; set; }
        }
        #endregion

        #region Css1InfoFor WO Types

        public class Css1InfoDetail
        {
            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Css1Info For WO Types from database.
            /// Created By   : Sudheer  
            /// Created Date : 21st Aug 2014
            /// Modified By  : Pavan
            /// Modified Date: 4 September 2014 (Added Sp Names)
            /// </summary>
            /// <returns></returns>
            public static DataSet GetCss1InfoDetails(string WOID, string woCode)
            {
                DataSet ds = new DataSet();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    string spName = string.Empty;

                    if (woCode == Enum.GetName(typeof(WOType), (int)WOType.AGM))
                    {
                        spName = "SpGetCSS1InfoForAGMDetails";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INCO))
                    {
                        spName = "";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ALLT))
                    {
                        spName = "SpGetCSS1InfoForAllotmentDetails";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPA))
                    {
                        spName = "SpGetCSS1InfoForAppOrCessOfAuditorsDetails";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPO))
                    {
                        spName = "SpGetCSS1InfoForApptOfOfficerDetails";
                    }

                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.CESO))
                    {
                        spName = "SpGetCSS1InfoForCessationOfOfficerDetails";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INTD))
                    {
                        spName = "SpGetCSS1InfoForInterimDividend";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TRAN))
                    {
                        spName = "SpGetCSS1InfoForTransfer";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.DUPL))
                    {
                        spName = "SpGetCSS1InfoForDuplicateDetails";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ECEN) || woCode == Enum.GetName(typeof(WOType), (int)WOType.TAKE))
                    {
                        spName = "SpGetCSS1InfoForExistingClientEngaging";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMA))
                    {
                        spName = "SpGetCSS1InfoForEGMAcqDisOfProperty";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMC))
                    {
                        spName = "SpGetCSS1InfoForEGMChangeOfName";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.BOIS))
                    {
                        spName = "SpGetCSS1InfoForBonusIssue";
                    }

                    else
                    {
                        spName = "";
                    }
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);

                    ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, spName, sqlParams);
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

            #endregion
        }

        #endregion

        #region Css1 Review For WO Types

        public class Css1ReviewDetails
        {
            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Css1Review For WO Types from database.
            /// Created By   : Pavan  
            /// Created Date : 18th May 2015
            /// Modified By  : 
            /// Modified Date: 
            /// </summary>
            /// <returns></returns>
            public static DataSet GetCss1ReviewDetails(string WOID, string woCode)
            {
                DataSet ds = new DataSet();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    string spName = string.Empty;

                    if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ALLT))
                    {
                        spName = "SPReviewCSS1InfoForAllotment";
                    }

                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPO))
                    {
                        spName = "SPReviewCSS1InfoForAppointmentOfOfficer";
                    }

                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INCO))
                    {
                        spName = "SPReviewCSS1InfoForIncorp";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TAKE))
                    {
                        spName = "SPReviewCSS1InfoForTakeover";
                    }
                    else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TRAN))
                    {
                        spName = "SPReviewCSS1InfoForTransfer";
                    }

                    else
                    {
                        spName = "";
                    }
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);

                    ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, spName, sqlParams);
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

            #endregion
        }

        #endregion

        #region AuditorsDetails

        public class Auditors
        {

            #region Properties

            public int AuditorID { get; set; }
            public string AuditorName { get; set; }

            #endregion

            #region Fetch Data

            private Auditors FetchAuditors(Auditors Auditors, SafeDataReader dr)
            {
                Auditors.AuditorID = dr.GetInt32("AuditorID");
                Auditors.AuditorName = dr.GetString("AuditorName");
                return Auditors;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Auditors Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<Auditors> GetAuditorDetailsByWOID(int WOID)
            {
                var data = new List<Auditors>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAuditorDetailsByWOID]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var Auditors = new Auditors();
                        Auditors.FetchAuditors(Auditors, safe);
                        data.Add(Auditors);
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

            #endregion

        }

        #endregion

        #region Mode Of Appointment

        public class ModeOfAppointment
        {

            #region Properties

            public int ID { get; set; }
            public string Name { get; set; }

            #endregion

            #region Fetch Data

            private ModeOfAppointment FetchModeOfAppointment(ModeOfAppointment ModeOfAppointment, SafeDataReader dr)
            {
                ModeOfAppointment.ID = dr.GetInt32("ID");
                ModeOfAppointment.Name = dr.GetString("Name");
                return ModeOfAppointment;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get ModeOfAppointment Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<ModeOfAppointment> GetModeOfAppointmentDetails()
            {
                var data = new List<ModeOfAppointment>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetMModeOfAppointment]");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var ModeOfAppointment = new ModeOfAppointment();
                        ModeOfAppointment.FetchModeOfAppointment(ModeOfAppointment, safe);
                        data.Add(ModeOfAppointment);
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

            #endregion

        }

        #endregion

        #region designations list
        public class ModeOfDesignations
        {

            #region Properties

            public int ID { get; set; }
            public string Name { get; set; }
            #endregion

            #region Fetch Data

            private ModeOfDesignations FetchModeOfDesignations(ModeOfDesignations designations, SafeDataReader dr)
            {
                designations.ID = dr.GetInt32("ID");
                designations.Name = dr.GetString("Name");
                return designations;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get ModeOfAppointment Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<ModeOfDesignations> GetModeOfDesignations()
            {
                var data = new List<ModeOfDesignations>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetModeOfDesignations]");
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var designations = new ModeOfDesignations();
                        designations.FetchModeOfDesignations(designations, safe);
                        data.Add(designations);
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

            #endregion

        }
        #endregion

        #region FirstSubscriber

        public class FirstSubscriber
        {

            #region Properties

            public int FirstSubscriberID { get; set; }
            public string FirstSubscriberName { get; set; }

            #endregion

            #region Fetch Data

            private FirstSubscriber FetchFirstSubscriber(FirstSubscriber Subscriber, SafeDataReader dr)
            {
                Subscriber.FirstSubscriberID = dr.GetInt32("PersonID");
                Subscriber.FirstSubscriberName = dr.GetString("PersonName");
                return Subscriber;
            }

            #endregion

            #region DataBaseMethods

            /// <summary>
            /// Description  : Get Auditors Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<FirstSubscriber> GetddlFirstSubscriber(int WOID)
            {
                var data = new List<FirstSubscriber>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetFirstSubscriberByWOID]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var FirstSubscriber = new FirstSubscriber();
                        FirstSubscriber.FetchFirstSubscriber(FirstSubscriber, safe);
                        data.Add(FirstSubscriber);
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
            /// Description  : Get Auditors Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<FirstSubscriber> GetddlPrincipalDetails(int WOID)
            {
                var data = new List<FirstSubscriber>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetPrincipalDetailsByWOID]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var FirstSubscriber = new FirstSubscriber();
                        FirstSubscriber.FetchFirstSubscriber(FirstSubscriber, safe);
                        data.Add(FirstSubscriber);
                    }
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
                return data;
            }

            #endregion

        }

        #endregion

        #region Get Nominee Directors

        public class NomineeDirectors
        {

            #region Properties

            public int PersonID { get; set; }
            public string PersonSource { get; set; }
            public string Name { get; set; }

            #endregion

            #region Fetch Data

            private NomineeDirectors FetchNomineeDirectors(NomineeDirectors NomineeDirector, SafeDataReader dr)
            {
                NomineeDirector.PersonID = dr.GetInt32("PersonID");
                NomineeDirector.PersonSource = dr.GetString("PersonSource");
                NomineeDirector.Name = dr.GetString("Name");
                return NomineeDirector;
            }

            #endregion

            #region DataBase Methods

            /// <summary>
            /// Description  : Get Auditors Details from database.
            /// Created By   : Pavan
            /// Created Date : 23 August 2014
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <returns></returns>
            public static List<NomineeDirectors> GetNomineeDirectorsDetails(int WOID, string InfoCode)
            {
                var data = new List<NomineeDirectors>();

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
                log.Debug("Start: " + methodBase.Name);
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@WOID", WOID);
                    sqlParams[1] = new SqlParameter("@InfoCode", InfoCode);
                    var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetNomineeDirectorsByWOIDAndInfoCode]", sqlParams);
                    var safe = new SafeDataReader(reader);
                    while (reader.Read())
                    {
                        var NomineeDirectors = new NomineeDirectors();
                        NomineeDirectors.FetchNomineeDirectors(NomineeDirectors, safe);
                        data.Add(NomineeDirectors);
                    }
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
                return data;
            }

            #endregion

        }

        #endregion

    }
}