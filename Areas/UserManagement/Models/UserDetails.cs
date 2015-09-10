
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

namespace CSS2.Areas.UserManagement.Models
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

    [MetadataType(typeof(UserDetailsMetadata))]

    public partial class UserDetails
    {
        private static ILog log = LogManager.GetLogger(typeof(UserDetails));

        #region Properties
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string LoginID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string GroupNames { get; set; }
        public string GroupID { set; get; }
        public bool IsManager { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }
        public string Roles { set; get; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public bool Status { get; set; }

        public string SessionID { set; get; }

        #endregion

        #region Fetching Data
        private UserDetails FetchUser(UserDetails user, SafeDataReader dr)
        {
            user.UserID = dr.GetInt32("UserID");
            user.LoginID = dr.GetString("LoginID");
            user.UserName = dr.GetString("Name");
            user.GroupNames = dr.GetString("GroupNames");
            user.Email = dr.GetString("Email");
            user.IsAdmin = dr.GetBoolean("IsAdmin");
            user.IsManager = dr.GetBoolean("IsManager");
            user.IsSuperAdmin = dr.GetBoolean("IsSuperAdmin");
            user.Status = dr.GetBoolean("Status");
            return user;
        }

        private UserDetails FetchAccessUser(UserDetails user, SafeDataReader dr)
        {
            user.UserID = dr.GetInt32("UserID");
            user.UserName = dr.GetString("Name");
            user.LoginID = dr.GetString("LoginID");
            user.Password = dr.GetString("Password");
            user.Email = dr.GetString("Email");
            user.GroupID = dr.GetString("GroupIDs");
            user.GroupNames = dr.GetString("GroupNames");
            return user;
        }

        public UserDetails FetchLoginUser(UserDetails user, SafeDataReader dr)
        {
            user.UserID = dr.GetInt32("UserID");
            user.LoginID = dr.GetString("LoginID");
            user.UserName = dr.GetString("Name");
            user.SessionID = dr.GetString("SESSIONID");
            user.Email = dr.GetString("Email");
            user.IsSuperAdmin = dr.GetBoolean("isSuperAdmin");
            user.IsAdmin = dr.GetBoolean("isadmin");
            user.IsManager = dr.GetBoolean("ismanager");
            user.Roles = dr.GetString("Roles").Trim();
            user.Roles = user.Roles.EndsWith(",") ? user.Roles.Substring(0, user.Roles.Length - 1) : user.Roles;
            return user;
        }
        private UserDetails FetchAllNames(UserDetails user, SafeDataReader dr)
        {
            // user.UserID = dr.GetInt32("UserID");
            user.UserName = dr.GetString("Name");

            return user;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : 
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  : Shiva
        /// Modified Date: 13 May 2014
        /// </summary>
        /// <returns>it will give all the UserDetails details available in database</returns>
        public static UserInfo GetAllUserDetails(int startPage, int resultPerPage, string Search, string Role, int Status, string OrderBy)
        {
            var users = new UserInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var User = new List<UserDetails>();
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@Search", Search);
                sqlParams[3] = new SqlParameter("@OrderBy", OrderBy);
                sqlParams[4] = new SqlParameter("@Role", Role);
                sqlParams[5] = new SqlParameter("@Status", Status);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAllUserDetails", sqlParams);
                var safe = new SafeDataReader(reader);

                while (reader.Read())
                {
                    var user = new UserDetails();
                    user.FetchUser(user, safe);
                    User.Add(user);
                    users.UserCount = Convert.ToInt32(reader["UserCount"]);
                }
                users.UserList = User;
                return users;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return users;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Show all the Services in View
        /// Created By   : Shiva
        /// Created Date : 13 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will Insert or Update all the User details in database</returns>
        public static int InsertANDUpdateUserDetails(int UserID, bool IsManager, bool IsAdmin, bool IsSuperAdmin, bool IsActive, int UpdatedBy)
        {
            int returnValue = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@UserID", UserID);
                sqlParams[1] = new SqlParameter("@IsManager", IsManager);
                sqlParams[2] = new SqlParameter("@IsAdmin", IsAdmin);
                sqlParams[3] = new SqlParameter("@IsSuperAdmin", IsSuperAdmin);
                sqlParams[4] = new SqlParameter("@IsActive", IsActive);
                sqlParams[5] = new SqlParameter("@SavedBy", UpdatedBy);
                returnValue = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPCSS1InsertAndUpdateUserDetails", sqlParams);
                return returnValue;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnValue;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : Shiva
        /// Created Date : 13 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class UserInfo
        {
            public IEnumerable<UserDetails> UserList { get; set; }
            public UserDetails UpdateUser { get; set; }
            public int UserCount { get; set; }
        }

        #endregion
    }
}