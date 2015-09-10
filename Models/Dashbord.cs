# region Document Header
//Created By       : Sudheer 
//Created Date     : 
//Description      : 
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
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    #endregion

    public class Dashbord
    {
        private static ILog log = LogManager.GetLogger(typeof(Dashbord));

        #region Properties
        public string WorkOrderID { set; get; }
        public string WorkOrderTypeText { set; get; }
        public string Name { set; get; }
        public string AssignedTo { set; get; }
        public string AssignedDate { set; get; }
        public string WOCode { set; get; }
        public string GroupCode { set; get; }
        public string CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string DocGeneratedDate { set; get; }
        public string CompletedDate { set; get; }
        public string UpdatedDate { set; get; }

        public int RefID { set; get; }
        public int NoteCount { set; get; }
        public string NoteType { set; get; }

        public string ColorFlag { set; get; }
        public string RowCount { set; get; }

        #endregion

        #region Fetch Methods
        private void FetchingDashbordData(DashbordInfo data, SqlDataReader reader, SafeDataReader safe)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                //UnAssigned               
                while (reader.Read())
                {
                    var ObjGetDashBord = new Dashbord();
                    ObjGetDashBord.FetchUnAssignedWODetails(ObjGetDashBord, safe);
                    data.WOUnAssignedList.Add(ObjGetDashBord);
                }

                //Assigned 
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetDashBord = new Dashbord();
                    ObjGetDashBord.FetchAssignedWODetails(ObjGetDashBord, safe);
                    data.WOAssignedList.Add(ObjGetDashBord);
                }

                //Draft
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetDashBord = new Dashbord();
                    ObjGetDashBord.FetchWODraftDetails(ObjGetDashBord, safe);
                    data.WODraftList.Add(ObjGetDashBord);
                }

                //Document generated
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetDashBord = new Dashbord();
                    ObjGetDashBord.FetchWODocGeneratedDetails(ObjGetDashBord, safe);
                    data.WODocGeneratedList.Add(ObjGetDashBord);
                }

                //Completed 
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetDashBord = new Dashbord();
                    ObjGetDashBord.FetchWOCompletedDetails(ObjGetDashBord, safe);
                    data.WOCompletedList.Add(ObjGetDashBord);
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }

        }

        private Dashbord FetchAssignedWODetails(Dashbord WorkOrderList, SafeDataReader dr)
        {
            WorkOrderList.WorkOrderID = dr.GetString("WOID");
            WorkOrderList.WorkOrderTypeText = dr.GetString("Type");
            WorkOrderList.Name = dr.GetString("ClientName");
            WorkOrderList.AssignedTo = dr.GetString("AssignedTo");
            WorkOrderList.AssignedDate = dr.GetDateTime("AssignedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            WorkOrderList.WOCode = dr.GetString("WO");
            WorkOrderList.GroupCode = dr.GetString("GroupCode");

            WorkOrderList.RefID = dr.GetInt32("WOID");
            WorkOrderList.NoteCount = dr.GetInt32("NoteCount");
            WorkOrderList.NoteType = dr.GetString("NoteType");
            WorkOrderList.ColorFlag = dr.GetString("ColorFlag");
            WorkOrderList.RowCount = dr.GetString("RowCount");
            return WorkOrderList;
        }

        private Dashbord FetchUnAssignedWODetails(Dashbord WorkOrderList, SafeDataReader dr)
        {
            WorkOrderList.WorkOrderID = dr.GetString("WOID");
            WorkOrderList.WorkOrderTypeText = dr.GetString("Type");
            WorkOrderList.Name = dr.GetString("ClientName");
            WorkOrderList.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            WorkOrderList.CreatedBy = dr.GetString("CreatedBy");
            WorkOrderList.WOCode = dr.GetString("WO");
            WorkOrderList.GroupCode = dr.GetString("GroupCode");

            WorkOrderList.RefID = dr.GetInt32("WOID");
            WorkOrderList.NoteCount = dr.GetInt32("NoteCount");
            WorkOrderList.NoteType = dr.GetString("NoteType");
            WorkOrderList.ColorFlag = dr.GetString("ColorFlag");
            WorkOrderList.RowCount = dr.GetString("RowCount");

            return WorkOrderList;
        }

        private Dashbord FetchWODocGeneratedDetails(Dashbord WorkOrderList, SafeDataReader dr)
        {
            WorkOrderList.WorkOrderID = dr.GetString("WOID");
            WorkOrderList.WorkOrderTypeText = dr.GetString("Type");
            WorkOrderList.Name = dr.GetString("ClientName");
            WorkOrderList.CreatedBy = dr.GetString("CreatedBy");
            WorkOrderList.DocGeneratedDate = dr.GetDateTime("DocGeneratedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            WorkOrderList.WOCode = dr.GetString("WO");
            WorkOrderList.GroupCode = dr.GetString("GroupCode");

            WorkOrderList.RefID = dr.GetInt32("WOID");
            WorkOrderList.NoteCount = dr.GetInt32("NoteCount");
            WorkOrderList.NoteType = dr.GetString("NoteType");
            WorkOrderList.RowCount = dr.GetString("RowCount");
            return WorkOrderList;
        }

        private Dashbord FetchWOCompletedDetails(Dashbord WorkOrderList, SafeDataReader dr)
        {
            WorkOrderList.WorkOrderID = dr.GetString("WOID");
            WorkOrderList.WorkOrderTypeText = dr.GetString("Type");
            WorkOrderList.Name = dr.GetString("ClientName");
            WorkOrderList.CreatedBy = dr.GetString("CreatedBy");
            WorkOrderList.CompletedDate = dr.GetDateTime("CompletedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            WorkOrderList.WOCode = dr.GetString("WO");
            WorkOrderList.GroupCode = dr.GetString("GroupCode");

            WorkOrderList.RefID = dr.GetInt32("WOID");
            WorkOrderList.NoteCount = dr.GetInt32("NoteCount");
            WorkOrderList.NoteType = dr.GetString("NoteType");
            WorkOrderList.RowCount = dr.GetString("RowCount");
            return WorkOrderList;
        }

        private Dashbord FetchWODraftDetails(Dashbord WorkOrderList, SafeDataReader dr)
        {
            WorkOrderList.WorkOrderID = dr.GetString("WOID");
            WorkOrderList.WorkOrderTypeText = dr.GetString("Type");
            WorkOrderList.Name = dr.GetString("ClientName");
            WorkOrderList.CreatedBy = dr.GetString("CreatedBy");
            WorkOrderList.UpdatedDate = dr.GetDateTime("UpdatedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            WorkOrderList.WOCode = dr.GetString("WO");
            WorkOrderList.GroupCode = dr.GetString("GroupCode");

            WorkOrderList.RefID = dr.GetInt32("WOID");
            WorkOrderList.NoteCount = dr.GetInt32("NoteCount");
            WorkOrderList.NoteType = dr.GetString("NoteType");
            WorkOrderList.RowCount = dr.GetString("RowCount");
            return WorkOrderList;
        }
        #endregion

        /// <summary>
        /// Description  : To get DashBord Details.
        /// Created By   : Sudheer  
        /// Created Date : 7th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public DashbordInfo GetDashbordDetails(int userID)
        {
            var data = new DashbordInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var getWorkOrder = new List<Dashbord>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@userID", userID);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetDashbordDetails", sqlParams);
                var safe = new SafeDataReader(reader);

                var WorkOrderList = new Dashbord();
                WorkOrderList.FetchingDashbordData(data, reader, safe);
                // getWorkOrder.Add(WorkOrderList);
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

    #region InfoClass

    /// <summary>
    /// Description  : All WO data based on status in same List
    /// Created By   : Sudheer
    /// Created Date : 7th Oct 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class DashbordInfo
    {
        public List<Dashbord> WOAssignedList { get; set; }
        public List<Dashbord> WOUnAssignedList { get; set; }
        public List<Dashbord> WODocGeneratedList { get; set; }
        public List<Dashbord> WOCompletedList { get; set; }
        public List<Dashbord> WODraftList { get; set; }

        public DashbordInfo()
        {
            WOAssignedList = new List<Dashbord>();
            WOUnAssignedList = new List<Dashbord>();
            WODocGeneratedList = new List<Dashbord>();
            WOCompletedList = new List<Dashbord>();
            WODraftList = new List<Dashbord>();
        }
    }
    #endregion
}