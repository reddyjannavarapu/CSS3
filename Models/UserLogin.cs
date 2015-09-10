# region Document Header
//Created By       : Anji 
//Created Date     : 15 May 2014
//Description      : to check user login details
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Models
{
    #region Usings
    using CSS2.Areas.UserManagement.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    public class UserLogin
    {
        private static ILog log = LogManager.GetLogger(typeof(UserLogin));

        [Display(Name = "Login ID :")]
        [Required(ErrorMessage = "*")]
        public string LoginID { set; get; }

        [Display(Name = "Password  :")]
        [Required(ErrorMessage = "*")]
        public string Password { set; get; }

        public class ActiveUsers
        {
            #region Properties
            public string DisplayName { set; get; }
            public string UserName { set; get; }
            public string Roles { set; get; }
            public string Login { set; get; }
            public string Duration { set; get; }
            public string Logout { set; get; }
            #endregion

            #region FetchActiveUsers
            internal void FetchActiveUsers(ActiveUsers user, SafeDataReader dr)
            {
                user.DisplayName = dr.GetString("DisplayName");
                user.UserName = dr.GetString("UserName");
                user.Roles = dr.GetString("Roles");
                user.Login = dr.GetDateTime("Login").ToString("dd MMM yyyy hh:mm:ss tt");
                user.Duration = dr.GetString("Duration");
                user.Logout = (dr.GetDateTime("LogoutOn") == DateTime.MinValue) ? string.Empty : dr.GetDateTime("LogoutOn").ToString("dd MMM yyyy hh:mm:ss tt");
            }

            #endregion


        }

        public class ErrorLogs
        {
            #region Properties

            public string UserName { get; set; }
            public string PageName { get; set; }
            public string ErrorTime { get; set; }
            public string Browser { get; set; }
            public string IPAddress { get; set; }
            public string ErrorMessage { get; set; }

            #endregion

            #region FetchMethods

            internal void FetchErrorLogs(ErrorLogs ErrorLog, SafeDataReader dr)
            {
                ErrorLog.UserName = dr.GetString("UserName");
                ErrorLog.PageName = dr.GetString("PageName");
                ErrorLog.ErrorTime = dr.GetDateTime("ErrorTime").ToString("dd MMM yyyy hh:mm:ss tt");
                ErrorLog.Browser = dr.GetString("Browser");
                ErrorLog.IPAddress = dr.GetString("IPAddress");
                ErrorLog.ErrorMessage = dr.GetString("ErrorMessage");
            }

            #endregion
        }

        public class CABErrors
        {
            #region Properties

            public int ID { get; set; }
            public string ERROR_NUMBER { get; set; }
            public string ERROR_SEVERITY { get; set; }
            public string ERROR_STATE { get; set; }
            public string ERROR_PROCEDURE { get; set; }
            public string ERROR_LINE { get; set; }
            public string ERROR_MESSAGE { get; set; }
            public string SCCode { get; set; }
            public string ONDate { get; set; }
            public string Client { get; set; }
            public string FeeCode { get; set; }
            public string CREATEDATE { get; set; }

            #endregion

            #region FetchMethods

            internal void FetchCABErrors(CABErrors ErrorLog, SafeDataReader dr)
            {
                ErrorLog.ID = dr.GetInt32("ID");
                ErrorLog.ERROR_NUMBER = dr.GetString("ERROR_NUMBER");
                ErrorLog.ERROR_SEVERITY = dr.GetString("ERROR_SEVERITY");
                ErrorLog.ERROR_STATE = dr.GetString("ERROR_STATE");
                ErrorLog.ERROR_PROCEDURE = dr.GetString("ERROR_PROCEDURE");
                ErrorLog.ERROR_LINE = dr.GetString("ERROR_LINE");
                ErrorLog.ERROR_MESSAGE = dr.GetString("ERROR_MESSAGE");
                ErrorLog.SCCode = dr.GetString("SCCode");
                ErrorLog.ONDate = dr.GetString("ONDate");
                ErrorLog.Client = dr.GetString("Client");
                ErrorLog.FeeCode = dr.GetString("FeeCode");
                ErrorLog.CREATEDATE = dr.GetDateTime("CREATEDATE").ToString("dd MMM yyyy hh:mm:ss tt");
            }

            #endregion
        }

        public class Css1IntegrationErrors
        {

            #region Properties

            public int ID { get; set; }
            public string ErrorNumber { get; set; }
            public string ErrorMessage { get; set; }
            public string ErrorState { get; set; }
            public string ErrorProcedure { get; set; }
            public string ErrorLine { get; set; }
            public string Type { get; set; }
            public string CreatedOn { get; set; }

            #endregion

            #region Fetch Methods

            internal void FetchCss1IntegrationErrors(Css1IntegrationErrors Css1IntegrationErrors, SafeDataReader dr)
            {
                Css1IntegrationErrors.ErrorNumber = dr.GetString("ErrorNumber");
                Css1IntegrationErrors.ErrorMessage = dr.GetString("ErrorMessage");
                Css1IntegrationErrors.ErrorState = dr.GetString("ErrorState");
                Css1IntegrationErrors.ErrorProcedure = dr.GetString("ErrorProcedure");
                Css1IntegrationErrors.ErrorLine = dr.GetString("ErrorLine");
                Css1IntegrationErrors.Type = dr.GetString("Type");
                Css1IntegrationErrors.CreatedOn = dr.GetDateTime("CreatedOn").ToString("dd MMM yyyy hh:mm:ss tt");
            }

            #endregion

        }

        public class ScheduleHistory
        {
            #region Properties

            public int Id { get; set; }
            public string ScheduleName { get; set; }
            public string Parameter { get; set; }
            public string ScheduleDescription { get; set; }
            public string RunbySystem    { get; set; }
            public string LatRunOn { get; set; }

            #endregion

            #region Fetch Methods

            internal void FetchScheduleHistory(ScheduleHistory history, SafeDataReader dr)
            {
                history.Id = dr.GetInt32("Id");
                history.ScheduleName = dr.GetString("ScheduleName");
                history.Parameter = dr.GetString("Parameter");
                history.ScheduleDescription = dr.GetString("ScheduleDescription");
                history.RunbySystem = dr.GetString("RunbySystem");
                history.LatRunOn = dr.GetDateTime("LatRunOn").ToString("dd MMM yyyy hh:mm:ss tt");
            }

            #endregion

        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> check User Login credentials valid UserDetails</returns>
        public static List<UserDetails> CheckUsersLogin(string Userid, string Pwd)
        {
            var users = new List<UserDetails>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            string encryptedPwd = UserLogin.Encrypt(Pwd);
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@LoginID", Userid);
            sqlParams[1] = new SqlParameter("@Pwd", encryptedPwd);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpCheckUserLogin", sqlParams);
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var user = new UserDetails();
                user.FetchLoginUser(user, safe);
                users.Add(user);
            }

            log.Debug("End: " + methodBase.Name);

            return users;
        }


        #region Authenticate Request and Validatesession
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   : Shiva 
        /// Modified Date : 16 Sep 2014
        /// AuthenticateRequest 
        /// </summary>
        /// <param name="Token">SessionToken</param>
        /// <returns>Active Session:0 InActive Session: UserID</returns>
        public static int AuthenticateRequest()
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            int userId = 0; ;
            try
            {

                string SessionToken = Convert.ToString(HttpContext.Current.Session["CSS2SessionID"]);
                if (!string.IsNullOrEmpty(SessionToken) ? true : false)
                {
                    // User session is Active Session it returns users id else returns 0
                    userId = UserLogin.CheckUserSessionToken(SessionToken);


                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return userId;
        }

        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> check User Logout To Update logout time</returns>
        public static void UpdateSessionToken(string sessionId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@sessionToken", sessionId);
            SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpLogOutSessionToken", sqlParams);
        }
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> check User Logout To Update logout time</returns>
        public static int CheckUserSessionToken(string SessionToken)
        {
            int userId;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@sessionToken", SessionToken);
                sqlParams[1] = new SqlParameter("@return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                userId = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCheck_SessionToken", sqlParams);
                userId = Convert.ToInt32(sqlParams[1].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userId;
        }
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> TO check User ValidateSession</returns>
        public static bool ValidateSession()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["CSS2SessionID"])))
            {
                return true;
            }
            return false;
        }
        #endregion

        //If Session is invalid then return true else false
        public static bool ValidateUserRequest()
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var session = UserLogin.ValidateSession();
                var AuthenticateRequest = UserLogin.AuthenticateRequest();
                if (session == false || AuthenticateRequest == 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return false;
        }


        /// <summary>
        /// Created By    : Shiva
        /// Created Date  : 29 Dec 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Active Users List</returns>

        public static UserInfo GetActiveUsersByDays(int NoOfDays)
        {
            var usersInfo = new UserInfo();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@NoOfdays", NoOfDays);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetUserLoginDetailsByDays", sqlParams);
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var user = new ActiveUsers();
                user.FetchActiveUsers(user, safe);
                usersInfo.ActiveUsersList.Add(user);
            }

            log.Debug("End: " + methodBase.Name);
            return usersInfo;
        }


        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 30 June 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Error Log List</returns>
        public static List<ErrorLogs> GetErrorLogs()
        {
            List<ErrorLogs> ErrLogs = new List<ErrorLogs>();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetErrorLogDetails");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var ErrorLog = new ErrorLogs();
                ErrorLog.FetchErrorLogs(ErrorLog, safe);
                ErrLogs.Add(ErrorLog);
            }

            log.Debug("End: " + methodBase.Name);
            return ErrLogs;
        }



        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 1 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Error Log List</returns>
        public static List<CABErrors> GetCABErrors()
        {
            List<CABErrors> ErrLogs = new List<CABErrors>();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCABErrorDetails");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var ErrorLog = new CABErrors();
                ErrorLog.FetchCABErrors(ErrorLog, safe);
                ErrLogs.Add(ErrorLog);
            }

            log.Debug("End: " + methodBase.Name);
            return ErrLogs;
        }

        public static List<ScheduleHistory> GetScheduleHistory()
        {
            List<ScheduleHistory> history = new List<ScheduleHistory>();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetScheduleHistory");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var detail = new ScheduleHistory();
                detail.FetchScheduleHistory(detail, safe);
                history.Add(detail);
            }

            log.Debug("End: " + methodBase.Name);
            return history;
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 23 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns>Css1 Integration Error Log List</returns>
        public static List<Css1IntegrationErrors> GetCss1IntegrationErrors()
        {
            List<Css1IntegrationErrors> Errs = new List<Css1IntegrationErrors>();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetCss1IntegrationErrorDetails");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var Error = new Css1IntegrationErrors();
                Error.FetchCss1IntegrationErrors(Error, safe);
                Errs.Add(Error);
            }
            log.Debug("End: " + methodBase.Name);
            return Errs;
        }

        internal static void FetchAllTableNames(SelectListItem Table, SafeDataReader dr)
        {
            Table.Text = dr.GetString("Name");
        }


        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 1 July 2015
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetAllTableNames()
        {
            List<SelectListItem> Tables = new List<SelectListItem>();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAllTableNames");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var Table = new SelectListItem();
                FetchAllTableNames(Table, safe);
                Tables.Add(Table);
            }

            log.Debug("End: " + methodBase.Name);
            return Tables;
        }

        /// <summary>
        /// Description  : To get TableData By TableName
        /// Created By   : Pavan
        /// Created Date : 11 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static DataSet GetTableDataByTableName(string TableName)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TableName", TableName);
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetTableDataByTableName]", sqlParams);
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


        #region Password Encryption and Decryption
        private static byte[] KEY_64 = { 42, 16, 93, 156, 78, 4, 218, 32 };
        private static byte[] IV_64 = { 55, 103, 246, 79, 36, 99, 167, 3 };
        /// <summary>
        /// Created By    : hussain
        /// Created Date  : 15 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// </summary>
        /// <returns> User Login credentials Encryption and Decryption</returns>
        public static string Encrypt(string value)
        {
            string strReturn = string.Empty;
            try
            {
                if (value != "")
                {

                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                    StreamWriter sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    ms.Flush();
                    //convert back to a string 
                    strReturn = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return strReturn;
        }

        public static string Decrypt(string value)
        {
            string strReturn = string.Empty;
            try
            {
                if (value != "")
                {
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    //convert from string to byte array 
                    byte[] buffer = Convert.FromBase64String(value);
                    MemoryStream ms = new MemoryStream(buffer);
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cs);
                    strReturn = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return strReturn;
        }

        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events.
        /// Created By   : Shiva
        /// Created Date : 29 Dec 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class UserInfo
        {
            public List<ActiveUsers> ActiveUsersList { get; set; }
            public int ActiveCount { get; set; }
            public UserInfo()
            {
                ActiveUsersList = new List<ActiveUsers>();
            }
        }
        #endregion

    }
}