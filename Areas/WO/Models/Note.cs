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

    public class Note
    {
        private static ILog log = LogManager.GetLogger(typeof(Note));

        #region Properties

        public string Description { set; get; }
        public string Type { set; get; }
        public int ReferId { set; get; }
        public int CreatedBy { get; set; }
        public int ID { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }
        public int IsLoggedUserForAction { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        #endregion

        #region Fetching Data
        private Note FetchNote(Note Note, SafeDataReader dr)
        {
            Note.ID = dr.GetInt32("ID");
            Note.Type = dr.GetString("Type");
            Note.ReferId = dr.GetInt32("ReferID");
            Note.Description = dr.GetString("Description");
            Note.Name = dr.GetString("Name");
            Note.IsLoggedUserForAction = dr.GetInt32("IsLoggedUserForAction");
            Note.CreatedDate = dr.GetString("CreatedDate");
            Note.UpdatedDate = dr.GetString("UpdatedDate");
            return Note;
        }
        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description   : To Insert Note
        /// Created By    : Pavan
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public int InsertNote()
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@Type", this.Type);
                sqlParams[1] = new SqlParameter("@RefId", this.ReferId);
                sqlParams[2] = new SqlParameter("@Description", this.Description);
                sqlParams[3] = new SqlParameter("@ID", this.ID);
                sqlParams[4] = new SqlParameter("@CreatedBy", this.CreatedBy);
                sqlParams[5] = new SqlParameter("@Action", this.Action);

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpInsertNote]", sqlParams);
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
        /// Description   : To Get Note Details for ReferId and Type
        /// Created By    : Pavan
        /// Created Date  : 4 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static NoteInfo GetNoteData(int ReferId, string Type, int UserID, int startpage, int rowsperpage)
        {
            var data = new NoteInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startpage);
                sqlParams[1] = new SqlParameter("@resultPerPage", rowsperpage);
                sqlParams[2] = new SqlParameter("@RefID", ReferId);
                sqlParams[3] = new SqlParameter("@Type", Type);
                sqlParams[4] = new SqlParameter("@UserID", UserID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetNoteByTypeAndRefID]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Notes = new Note();
                    Notes.FetchNote(Notes, safe);
                    data.NoteList.Add(Notes);
                    data.NoteCount = Convert.ToInt32(reader["NoteCount"]);
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
        /// Description   : To Delete Note
        /// Created By    : Pavan
        /// Created Date  : 5 June 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public int DeleteNote()
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@CreatedBy", this.CreatedBy);
                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpDeleteNoteByID]", sqlParams);
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

        #endregion
    }

    #region NoteInfo Class
    /// <summary>
    /// Description  : To do all events in Note Partial View
    /// Created By   : Pavan
    /// Created Date : 5 June 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class NoteInfo
    {
        public List<Note> NoteList { get; set; }
        public int NoteCount { get; set; }

        public NoteInfo()
        {
            NoteList = new List<Note>();
        }
    }
    #endregion

}