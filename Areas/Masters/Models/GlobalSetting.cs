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

    [MetadataType(typeof(GlobalSettingsMetadata))]
    public partial class GlobalSetting
    {
        private static ILog log = LogManager.GetLogger(typeof(GlobalSetting));

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string RangeMin { get; set; }
        public string RangeMax { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        #endregion

        #region Fetching Data
        private GlobalSetting FetchGlobalSettings(GlobalSetting setting, SafeDataReader dr)
        {
            setting.Id = dr.GetInt32("ID");
            setting.Code = dr.GetString("Code");
            setting.Value = dr.GetString("Value");
            setting.RangeMin = dr.GetString("RangeMin");
            setting.RangeMax = dr.GetString("RangeMax");
            setting.Description = dr.GetString("Description");
            setting.Status = dr.GetBoolean("Status");

            return setting;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : 
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  : pavan
        /// Modified Date: 13 May 2014
        /// </summary>
        /// <returns>it will give all the GlobalSettings details available in database</returns>
        public static GlobalSettingInfo GetAllGlobalSettings(int startPage, int resultPerPage, string code, int status, string OrderBy)
        {
            var data = new GlobalSettingInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@code", code);
                sqlParams[3] = new SqlParameter("@status", status);
                sqlParams[4] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMGlobalSettings]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Setting = new GlobalSetting();
                    Setting.FetchGlobalSettings(Setting, safe);
                    data.GlobalSettingList.Add(Setting);
                    data.GlobalSettingsCount = Convert.ToInt32(reader["GlobalSettingCount"]);
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
        /// Description  :  
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="e">pass the GlobalSettings object to insert details</param>
        /// <returns></returns>
        public int InsertGlobalSettings()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@ID", this.Id);
                sqlParams[1] = new SqlParameter("@Code", this.Code);
                sqlParams[2] = new SqlParameter("@Description", this.Description);
                sqlParams[3] = new SqlParameter("@Value", this.Value);
                sqlParams[4] = new SqlParameter("@RangeMin", this.RangeMin);
                sqlParams[5] = new SqlParameter("@RangeMax", this.RangeMax);
                sqlParams[6] = new SqlParameter("@Status", this.Status);
                sqlParams[7] = new SqlParameter("@CreatedBy", this.CreatedBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertMGlobalSettings]", sqlParams);
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
        /// Description  :  
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="GlobalSettingsId">pass the GlobalSettings id which GlobalSettings details you wont </param>
        /// <returns></returns>
        public static GlobalSetting GetGlobalSettingsById(int id)
        {
            var GlobalSettings = new GlobalSetting();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", id);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetGlobalSettingsById]", sqlParams);

                while (reader.Read())
                {
                    GlobalSettings.FetchGlobalSettings(GlobalSettings, new SafeDataReader(reader));
                }

                return GlobalSettings;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return GlobalSettings;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  :  
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="GlobalSettingsId">pass the GlobalSettings id which you wont to delete</param>
        /// <returns></returns>
        public static int DeleteGlobalSettingsById(int id)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", id);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteMGlobalSettingById]", sqlParams);

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
    public class GlobalSettingInfo
    {
        public List<GlobalSetting> GlobalSettingList { get; set; }

        public int GlobalSettingsCount { get; set; }

        public GlobalSettingInfo()
        {
            GlobalSettingList = new List<GlobalSetting>();
        }
    }
    #endregion
}