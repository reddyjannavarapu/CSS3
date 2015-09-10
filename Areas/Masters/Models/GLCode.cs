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

    [MetadataType(typeof(GLCodeMetadata))]
    public partial class GLCode
    {
        private static ILog log = LogManager.GetLogger(typeof(GLCode));

        #region Properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        #endregion

        #region Fetching Data
        private GLCode FetchGLCode(GLCode glcode, SafeDataReader dr)
        {
            glcode.Id = dr.GetInt32("ID");
            glcode.Code = dr.GetString("Code");
            glcode.Description = dr.GetString("Description");
            glcode.Status = dr.GetBoolean("Status");

            return glcode;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : 
        /// Created By   : Anji
        /// Created Date : 03 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the GLCode details available in database</returns>
        public static GLCodeInfo GetAllGLCodes(int startPage, int resultPerPage, string code, int status, string OrderBy)
        {
            var data = new GLCodeInfo();

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

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllMGLCodes]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var glcode = new GLCode();
                    glcode.FetchGLCode(glcode, safe);
                    data.GLCodeList.Add(glcode);
                    data.GLCodeCount = Convert.ToInt32(reader["GLCodeCount"]);
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
        /// <param name="e">pass the GLCode object to insert details</param>
        /// <returns></returns>
        public int InsertGLCode()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@ID", this.Id);
                sqlParams[1] = new SqlParameter("@Code", this.Code);
                sqlParams[2] = new SqlParameter("@Description", this.Description);
                sqlParams[3] = new SqlParameter("@Status", this.Status);
                sqlParams[4] = new SqlParameter("@CreatedBy", this.CreatedBy);

                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertMGLCode]", sqlParams);
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
        /// <param name="GLCodeId">pass the GLCode id which GLCode details you wont </param>
        /// <returns></returns>
        public static GLCode GetGLCodeById(int id)
        {
            var glcode = new GLCode();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", id);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetGLCodeById]", sqlParams);
                while (reader.Read())
                {
                    glcode.FetchGLCode(glcode, new SafeDataReader(reader));
                }
                return glcode;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return glcode;
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
        /// <param name="GLCodeId">pass the GLCode id which you wont to delete</param>
        /// <returns></returns>
        public static int DeleteGLCodeById(int id)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@id", id);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteGLCodeById]", sqlParams);
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
    public class GLCodeInfo
    {
        public List<GLCode> GLCodeList { get; set; }
        public int GLCodeCount { get; set; }

        public GLCodeInfo()
        {
            GLCodeList = new List<GLCode>();
        }
    }
    #endregion
}