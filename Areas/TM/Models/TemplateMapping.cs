
# region Document Header
//Created By       : Anji 
//Created Date     : 12 August 2014
//Description      : Document Generate Logic
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
    using System.Linq;
    #endregion

    public class TemplateMapping
    {
        private static ILog log = LogManager.GetLogger(typeof(TemplateMapping));

        #region Properties

        public int WOID { get; set; }
        public int SetID { get; set; }
        public int FileID { get; set; }
        public bool TFileStatus { get; set; }
        public string SetName { get; set; }
        public string FileName { get; set; }
        public string FileFullName { set; get; }
        public string FilePath { set; get; }

        public string EntityName { set; get; }
        public string DisplayName { set; get; }

        public string Description { set; get; }
        public string IsDefault { set; get; }
        public string IsMultiple { set; get; }
        public string MultipleEntity { set; get; }
        public string WOCode { get; set; }

        public string SetDescription { get; set; }
        public bool SetStatus { get; set; }

        #endregion

        #region Fetching Data
        private TemplateMapping FetchTemplate(TemplateMapping TMapping, SafeDataReader dr)
        {
            TMapping.WOID = dr.GetInt32("WOID");
            TMapping.SetID = dr.GetInt32("SetID");
            TMapping.SetName = dr.GetString("SetName");
            TMapping.FileName = dr.GetString("FileName");
            TMapping.FileID = dr.GetInt32("FileID");
            TMapping.FileFullName = dr.GetString("FileFullName");
            TMapping.FilePath = dr.GetString("FilePath");
            TMapping.TFileStatus = dr.GetBoolean("TFileStatus");

            return TMapping;
        }

        private TemplateMapping FetchTemplateSetDetails(TemplateMapping TMapping, SafeDataReader dr)
        {
            TMapping.SetID = dr.GetInt32("SetID");
            TMapping.SetName = dr.GetString("SetName");
            TMapping.FilePath = dr.GetString("FilePath");
            return TMapping;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description   : To WO-Templates Data
        /// Created By    : Sudheer 
        /// Created Date  : 17tg July 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static List<tempinfo> GetTemplateData(int WoID, string WoTypeID)
        {
            var data = new TemplateInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                tempinfo objtempinfo = new tempinfo();
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@WOID", WoID);
                sqlParams[1] = new SqlParameter("@WOTYPEID", WoTypeID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetTemplateByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Template = new TemplateMapping();
                    Template.FetchTemplate(Template, safe);
                    data.TemplateList.Add(Template);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            List<tempinfo> MaintempLst = new List<tempinfo>();
            FetchSection(data, MaintempLst);

            return MaintempLst;
        }

        /// <summary>
        /// Description   : To WO-Templates Data
        /// Created By    : Sudheer 
        /// Created Date  : 25th Aug 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static TemplateInfo GetTemplateSetData(string WoTypeID)
        {
            var data = new TemplateInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                tempinfo objtempinfo = new tempinfo();
                SqlParameter[] sqlParams = new SqlParameter[1];

                sqlParams[0] = new SqlParameter("@WOTYPEID", WoTypeID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPGetTemplateSetByWOType]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Template = new TemplateMapping();
                    Template.FetchTemplateSetDetails(Template, safe);
                    data.TemplateList.Add(Template);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return data;
        }

        /// <summary>
        /// Description   : Method is used to Arrenge template Data In Lists
        /// Created By    : Sudheer 
        /// Created Date  : 17tg July 2014
        /// Modified By   : Shiva  
        /// Modified Date : 13 July 2014
        /// <returns></returns>
        /// </summary>
        private static List<tempinfo> FetchSection(TemplateInfo data, List<tempinfo> MaintempLst)
        {
            var SetIDs = (from s in data.TemplateList select s.SetID).Distinct().ToList();

            foreach (var SetID in SetIDs)
            {
                tempinfo objtempinfo = new tempinfo();
                List<SectionInfo> objlistsec = new List<SectionInfo>();
                var Sections = (from s in data.TemplateList where s.SetID == SetID select s.FileID).ToList();

                string templateName = (from s in data.TemplateList where s.SetID == SetID select s.SetName).ToList().FirstOrDefault();
                int templateID = (from s in data.TemplateList where s.SetID == SetID select s.SetID).ToList().FirstOrDefault();
                int woid = (from s in data.TemplateList where s.SetID == SetID select s.WOID).ToList().FirstOrDefault();
                bool TStatus = false;
                foreach (var sitems in Sections)
                {

                    foreach (var itemdata in data.TemplateList)
                    {
                        if (sitems == itemdata.FileID && sitems != 0 && itemdata.FileID != 0)
                        {
                            SectionInfo objsecinfo = new SectionInfo();
                            objsecinfo.FileName = (from s in data.TemplateList where s.FileID == sitems select s.FileName).ToList().FirstOrDefault();
                            objsecinfo.FileID = (from s in data.TemplateList where s.FileID == sitems select s.FileID).ToList().FirstOrDefault();
                            objsecinfo.TFileStatus = (from s in data.TemplateList where s.FileID == sitems select s.TFileStatus).ToList().FirstOrDefault();

                            if (objsecinfo.TFileStatus && !TStatus)
                                TStatus = true;

                            objsecinfo.WOID = (from s in data.TemplateList where s.FileID == sitems select s.WOID).ToList().FirstOrDefault();
                            objsecinfo.SetID = (from s in data.TemplateList where s.FileID == sitems select s.SetID).ToList().FirstOrDefault();
                            objsecinfo.FileFullName = (from s in data.TemplateList where s.FileID == sitems select s.FileFullName).ToList().FirstOrDefault();
                            objsecinfo.FilePath = (from s in data.TemplateList where s.FileID == sitems select s.FilePath).ToList().FirstOrDefault();
                            //objlistsec.Add(objsecinfo);
                            objtempinfo.SectionList.Add(objsecinfo);
                        }
                    }
                }

                objtempinfo.SetID = (from s in data.TemplateList where s.SetID == SetID select s.SetID).ToList().FirstOrDefault();
                objtempinfo.SetName = (from s in data.TemplateList where s.SetID == SetID select s.SetName).ToList().FirstOrDefault();
                objtempinfo.WOID = (from s in data.TemplateList where s.SetID == SetID select s.WOID).ToList().FirstOrDefault();
                objtempinfo.TFileStatus = TStatus;
                MaintempLst.Add(objtempinfo);

            }
            return MaintempLst;
        }

        /// <summary>
        /// Description   : Method is used to Insert Generated template history.
        /// Created By    : Sudheer 
        /// Created Date  : 17tg July 2014
        /// Modified By   : Sudheer  
        /// Modified Date :  23rd July 2014
        /// <returns></returns>
        /// </summary>
        public static object InsertWOTemplateDetails(DataTable dtTemplateDetails, string UserIDSession, string WOID)
        {
            int result = 0;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@dtTemplateDetails", dtTemplateDetails);
                sqlParams[1] = new SqlParameter("@Createdby", UserIDSession);
                sqlParams[2] = new SqlParameter("@WOID", Convert.ToInt32(WOID));
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertWOTemplateDetails", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return result;
        }

        public static object InsertSetDetails(TemplateMapping data)
        {
            int output = 0;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@Wocode", data.WOCode);
                sqlParams[1] = new SqlParameter("@SetId", data.SetID);
                sqlParams[2] = new SqlParameter("@name", data.DisplayName);
                sqlParams[3] = new SqlParameter("@Description", data.Description);
                sqlParams[4] = new SqlParameter("@Status", data.TFileStatus);

                sqlParams[5] = new SqlParameter("@Output", 0);
                sqlParams[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertTemplatesSet", sqlParams);

                output = Convert.ToInt32(sqlParams[5].Value);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return output;

            // return result;
        }


        /// <summary>
        /// Description   : Method is used to Insert new template details.
        /// Created By    : Sudheer 
        /// Created Date  : 26th Aug 2014
        /// Modified By   : Sudheer          
        /// <returns></returns>
        /// </summary>
        public static int InsertTemplateFileDetails(WOTemplateFileDetails arrFilesList, string flag, string fieldID, string CreatedBy)
        {
            int result = 0;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[12];
                sqlParams[0] = new SqlParameter("@SetID", Convert.ToInt32(arrFilesList.SetID));
                sqlParams[1] = new SqlParameter("@DisplayName", arrFilesList.DisplayName);
                sqlParams[2] = new SqlParameter("@FileName", arrFilesList.FileName);
                sqlParams[3] = new SqlParameter("@FilePath", arrFilesList.FilePath);
                sqlParams[4] = new SqlParameter("@Description", arrFilesList.Description);
                sqlParams[5] = new SqlParameter("@Status", Convert.ToBoolean(arrFilesList.Status));
                sqlParams[6] = new SqlParameter("@IsDefault", Convert.ToBoolean(arrFilesList.IsDefault));
                sqlParams[7] = new SqlParameter("@IsMultiple", Convert.ToBoolean(arrFilesList.IsMultiple));
                sqlParams[8] = new SqlParameter("@MultipleEntity", arrFilesList.MultipleEntity);
                sqlParams[9] = new SqlParameter("@Flag", flag);
                sqlParams[10] = new SqlParameter("@FieldID", fieldID);
                sqlParams[11] = new SqlParameter("@CreatedBy", CreatedBy);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertTemplateFileDetails", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return result;
        }

        public static List<TemplateMapping> GetMDocMultipleEntity(string WOCode)
        {
            var data = new TemplateInfo();
            List<TemplateMapping> MaintempLst = new List<TemplateMapping>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOCode", WOCode);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMDoC", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    TemplateMapping Template = new TemplateMapping();
                    Template.FetchTemplateMDocMultipleEntity(Template, safe);
                    MaintempLst.Add(Template);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return MaintempLst;
        }

        private TemplateMapping FetchTemplateMDocMultipleEntity(TemplateMapping TMapping, SafeDataReader dr)
        {
            TMapping.EntityName = dr.GetString("EntityName");
            TMapping.DisplayName = dr.GetString("DisplayName");
            return TMapping;
        }

        #endregion

        #region Template Management(Add/Edit)

        /// <summary>
        /// Description   : To WO-Templates Data
        /// Created By    : Sudheer 
        /// Created Date  : 25th Aug 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static int DeleteTemplateDoc(string FileId)
        {
            int deleteStatus = 0;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@FileId", FileId);
                deleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteTemplateFileByID", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return deleteStatus;
        }

        /// <summary>
        /// Description   : To Delete Template Set
        /// Created By    : Sudheer 
        /// Created Date  : 2nd Sep 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static int DeleteTemplateSet(string SetID)
        {
            int deleteStatus = 0;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@SetID", SetID);
                deleteStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteTemplateSetByID", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);
            return deleteStatus;
        }
        /// <summary>
        /// Description   : To WO-Templates Data
        /// Created By    : Sudheer 
        /// Created Date  : 25th Aug 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns></returns>
        /// </summary>
        public static List<tempinfo> GetTemplateData(string WoTypeID)
        {
            var data = new TemplateInfo();
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                tempinfo objtempinfo = new tempinfo();
                SqlParameter[] sqlParams = new SqlParameter[1];

                sqlParams[0] = new SqlParameter("@WOTYPEID", WoTypeID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetTemplateByWOType", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Template = new TemplateMapping();
                    Template.FetchTemplateFileDetails(Template, safe);
                    data.TemplateList.Add(Template);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            List<tempinfo> MaintempLst = new List<tempinfo>();
            FetchFileSection(data, MaintempLst);
            return MaintempLst;
        }

        private TemplateMapping FetchTemplateFileDetails(TemplateMapping TMapping, SafeDataReader dr)
        {
            TMapping.SetID = dr.GetInt32("SetID");
            TMapping.SetName = dr.GetString("SetName");
            TMapping.FileName = dr.GetString("FileName");
            TMapping.FileID = dr.GetInt32("FileID");
            TMapping.FileFullName = dr.GetString("FileFullName");
            TMapping.FilePath = dr.GetString("FilePath");
            TMapping.TFileStatus = dr.GetBoolean("TFileStatus");
            TMapping.Description = dr.GetString("Description");
            TMapping.IsDefault = dr.GetString("IsDefault");
            TMapping.IsMultiple = dr.GetString("IsMultiple");
            TMapping.MultipleEntity = dr.GetString("MultipleEntity");
            TMapping.SetDescription = dr.GetString("SetDescription");
            TMapping.SetStatus = dr.GetBoolean("SetStatus");

            return TMapping;
        }

        /// <summary>
        /// Description   : Method is used to Arrenge template Data In Lists
        /// Created By    : Sudheer 
        /// Created Date  : 27th July 2014       
        /// <returns></returns>
        /// </summary>
        private static List<tempinfo> FetchFileSection(TemplateInfo data, List<tempinfo> MaintempLst)
        {
            var SetIDs = (from s in data.TemplateList select s.SetID).Distinct().ToList();

            foreach (var SetID in SetIDs)
            {
                tempinfo objtempinfo = new tempinfo();
                List<SectionInfo> objlistsec = new List<SectionInfo>();
                var Sections = (from s in data.TemplateList where s.SetID == SetID select s.FileID).ToList();

                string templateName = (from s in data.TemplateList where s.SetID == SetID select s.SetName).ToList().FirstOrDefault();
                int templateID = (from s in data.TemplateList where s.SetID == SetID select s.SetID).ToList().FirstOrDefault();
                int woid = (from s in data.TemplateList where s.SetID == SetID select s.WOID).ToList().FirstOrDefault();
                bool TStatus = false;
                foreach (var sitems in Sections)
                {

                    foreach (var itemdata in data.TemplateList)
                    {
                        if (sitems == itemdata.FileID && sitems != 0 && itemdata.FileID != 0)
                        {
                            SectionInfo objsecinfo = new SectionInfo();
                            objsecinfo.FileName = (from s in data.TemplateList where s.FileID == sitems select s.FileName).ToList().FirstOrDefault();
                            objsecinfo.FileID = (from s in data.TemplateList where s.FileID == sitems select s.FileID).ToList().FirstOrDefault();
                            objsecinfo.TFileStatus = (from s in data.TemplateList where s.FileID == sitems select s.TFileStatus).ToList().FirstOrDefault();

                            if (objsecinfo.TFileStatus && !TStatus)
                                TStatus = true;

                            objsecinfo.WOID = (from s in data.TemplateList where s.FileID == sitems select s.WOID).ToList().FirstOrDefault();
                            objsecinfo.SetID = (from s in data.TemplateList where s.FileID == sitems select s.SetID).ToList().FirstOrDefault();
                            objsecinfo.FileFullName = (from s in data.TemplateList where s.FileID == sitems select s.FileFullName).ToList().FirstOrDefault();
                            objsecinfo.FilePath = (from s in data.TemplateList where s.FileID == sitems select s.FilePath).ToList().FirstOrDefault();

                            objsecinfo.Description = (from s in data.TemplateList where s.FileID == sitems select s.Description).ToList().FirstOrDefault();
                            objsecinfo.IsDefault = (from s in data.TemplateList where s.FileID == sitems select s.IsDefault).ToList().FirstOrDefault();
                            objsecinfo.IsMultiple = (from s in data.TemplateList where s.FileID == sitems select s.IsMultiple).ToList().FirstOrDefault();
                            objsecinfo.MultipleEntity = (from s in data.TemplateList where s.FileID == sitems select s.MultipleEntity).ToList().FirstOrDefault();

                            //objlistsec.Add(objsecinfo);
                            objtempinfo.SectionList.Add(objsecinfo);
                        }
                    }
                }

                objtempinfo.SetID = (from s in data.TemplateList where s.SetID == SetID select s.SetID).ToList().FirstOrDefault();
                objtempinfo.SetName = (from s in data.TemplateList where s.SetID == SetID select s.SetName).ToList().FirstOrDefault();
                objtempinfo.SetName = (from s in data.TemplateList where s.SetID == SetID select s.SetName).ToList().FirstOrDefault();
                objtempinfo.WOID = (from s in data.TemplateList where s.SetID == SetID select s.WOID).ToList().FirstOrDefault();
                objtempinfo.TFileStatus = TStatus;
                objtempinfo.SetStatus = (from s in data.TemplateList where s.SetID == SetID select s.SetStatus).ToList().FirstOrDefault();
                objtempinfo.SetDescription = (from s in data.TemplateList where s.SetID == SetID select s.SetDescription).ToList().FirstOrDefault();
                MaintempLst.Add(objtempinfo);

            }
            return MaintempLst;
        }

        #endregion

    }

    #region TemplateInfo Class
    /// <summary>
    /// Description  : To do all events in Note Partial View
    /// Created By    : Sudheer 
    /// Created Date  : 17tg July 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class TemplateInfo
    {
        public List<TemplateMapping> TemplateList { get; set; }
        public int TemplateCount { get; set; }

        public TemplateInfo()
        {
            TemplateList = new List<TemplateMapping>();
        }
    }
    #endregion

    #region TemplateSectionInfo
    /// <summary>
    /// Description  : To bind Template Section information
    /// Created By    : Sudheer 
    /// Created Date  : 17tg July 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>

    public class SectionInfo
    {
        public int WOID { get; set; }
        public string FileName { get; set; }
        public int FileID { get; set; }
        public bool TFileStatus { get; set; }
        public int SetID { get; set; }
        public string FileFullName { set; get; }
        public string FilePath { set; get; }

        public string Description { set; get; }
        public string IsDefault { set; get; }
        public string IsMultiple { set; get; }
        public string MultipleEntity { set; get; }
    }

    public class tempinfo
    {
        public int WOID { get; set; }
        public int SetID { get; set; }
        //public int SectionID { get; set; }
        public bool TFileStatus { get; set; }
        public string SetName { get; set; }
        public string SetDescription { get; set; }
        public bool SetStatus { get; set; }

        public string FileFullName { set; get; }
        public string FilePath { set; get; }
        // public string SECTIONName { get; set; }

        public List<SectionInfo> SectionList { get; set; }
        public tempinfo()
        {
            SectionList = new List<SectionInfo>();
        }
    }
    #endregion

    #region WOTemplateMappingClass
    public class WOTemplateMapping
    {
        public int WOID { get; set; }
        public int SetID { get; set; }
        public int FileID { get; set; }
        public string FileFullName { get; set; }
        public string FilePath { get; set; }
        public string DocType { get; set; }
    }
    #endregion

    #region Template File
    public class WOTemplateFileDetails
    {
        public string SetID { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsDefault { get; set; }
        public bool IsMultiple { get; set; }
        public string MultipleEntity { get; set; }

        public static WOTemplateFileDetails FetchTemplate(WOTemplateFileDetails TMapping, SafeDataReader dr)
        {
            TMapping.FileName = dr.GetString("FileName");
            TMapping.FilePath = dr.GetString("FilePath");
            TMapping.IsDefault = dr.GetBoolean("IsDefault");
            TMapping.IsMultiple = dr.GetBoolean("IsMultiple");
            TMapping.MultipleEntity = dr.GetString("MultipleEntity");

            return TMapping;
        }

        public static List<WOTemplateFileDetails> GetDocumentFiles(string @FileIds)
        {
            var data = new List<WOTemplateFileDetails>();
            tempinfo objtempinfo = new tempinfo();
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@FileIds", @FileIds);

            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetDocumentFiles]", sqlParams);
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var Template = new WOTemplateFileDetails();
                FetchTemplate(Template, safe);
                data.Add(Template);
            }


            return data;
        }

    }
    #endregion
}