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
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    #endregion

    public class WorkOrders
    {
        private static ILog log = LogManager.GetLogger(typeof(WorkOrders));

        #region Properties
        public int ID { set; get; }
        public string WorkOrderID { set; get; }
        public string Description { set; get; }
        public string WorkOrderType { set; get; }
        public string WorkOrderTypeText { set; get; }
        public string ClientId { set; get; }
        public bool Status { set; get; }
        public bool EditStatus { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public string NoteType { set; get; }
        public int NoteCount { set; get; }
        public int RefID { set; get; }
        public string WOCode { set; get; }
        public string ClientName { set; get; }
        public string GroupCode { set; get; }
        public bool IsBillable { set; get; }
        public bool IsAdhoc { set; get; }
        public bool IsPostedToCss1 { set; get; }
        public string CategoryCode { set; get; }
        public string StatusCode { set; get; }
        public string AssignedTo { set; get; }
        public string AssignedToGroup { set; get; }
        public string SourceID { set; get; }
        public string BillingPartyName { set; get; }
        public string CreatedDate { set; get; }

        public int BillingPartyID { get; set; }
        public string BillingPartySourceID { get; set; }

        public List<DisbursementItem> DIList { get; set; }

        public WorkOrders()
        {
            DIList = new List<DisbursementItem>();
        }

        #endregion

        #region Fetch Methods
        private WorkOrders FetchingSerchWODataByWO(WorkOrders SerchWO, SafeDataReader dr)
        {
            SerchWO.ID = dr.GetInt32("ID");
            SerchWO.ClientId = dr.GetString("ClientCode");
            SerchWO.WOCode = dr.GetString("WOCode");
            SerchWO.GroupCode = dr.GetString("GroupCode");
            SerchWO.IsBillable = dr.GetBoolean("IsBillable");
            SerchWO.IsAdhoc = dr.GetBoolean("IsAdhoc");
            SerchWO.IsPostedToCss1 = dr.GetBoolean("IsPostedToCss1");
            SerchWO.CategoryCode = dr.GetString("CategoryCode");
            SerchWO.SourceID = dr.GetString("SourceID");
            SerchWO.WorkOrderType = dr.GetString("Type");
            SerchWO.ClientName = dr.GetString("ClientName");
            SerchWO.StatusCode = dr.GetString("StatusCode");
            // SerchWO.Status = dr.GetBoolean("Status");
            SerchWO.WorkOrderTypeText = dr.GetString("TypeName");
            SerchWO.BillingPartyName = dr.GetString("BillingParty");
            SerchWO.BillingPartyID = dr.GetInt32("BillingPartyID");
            SerchWO.BillingPartySourceID = dr.GetString("BillingPartySourceID");
            SerchWO.Description = dr.GetString("Description");
            return SerchWO;
        }
        private WorkOrders FetchingSerchWOData(WorkOrders SerchWO, SafeDataReader dr)
        {
            SerchWO.ID = dr.GetInt32("ID");
            SerchWO.RefID = SerchWO.ID;
            SerchWO.WorkOrderType = dr.GetString("Type");
            // SerchWO.Description = dr.GetString("Description");
            SerchWO.Status = dr.GetBoolean("Status");
            SerchWO.EditStatus = dr.GetBoolean("EditStatus");
            SerchWO.WorkOrderTypeText = dr.GetString("wotypetext");
            SerchWO.ClientId = dr.GetString("ClientName");
            SerchWO.WorkOrderID = dr.GetString("WOCode");
            SerchWO.NoteCount = dr.GetInt32("NoteCount");
            SerchWO.NoteType = dr.GetString("NoteType");
            SerchWO.StatusCode = dr.GetString("Name");
            SerchWO.BillingPartyName = dr.GetString("BillingPartyName");
            SerchWO.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            SerchWO.IsAdhoc = dr.GetBoolean("IsAdhoc");
            SerchWO.IsPostedToCss1 = dr.GetBoolean("IsPostedToCss1");
            return SerchWO;
        }
        private WorkOrders FetchWOTypeOrCategory(WorkOrders WOTypeOrCategory, SafeDataReader dr)
        {
            WOTypeOrCategory.Name = dr.GetString("Name");
            WOTypeOrCategory.Code = dr.GetString("Code");
            return WOTypeOrCategory;
        }
        private WorkOrders FetchWorkOrderType(WorkOrders SerchWO, SafeDataReader dr)
        {
            SerchWO.Code = dr.GetString("Code");
            SerchWO.Name = dr.GetString("Name");

            return SerchWO;
        }
        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Show all the Workorders SearchWO List 
        /// Created By   : hussain
        /// Created Date : 27 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the Workorders details available in database</returns>
        public static WorkOrderInfo GetSerchWOData(string ClientId, string SourceID, string WorkOrderID, string statusCode, int startpage, int rowsperpage, string Type, string OrderBy, string FromDate, string ToDate, int CreatedBy, string IsAdhoc, string AssignedTo)
        {
            var data = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var getWorkOrder = new List<WorkOrders>();
                SqlParameter[] sqlParams = new SqlParameter[13];
                sqlParams[0] = new SqlParameter("@startPage", startpage);
                sqlParams[1] = new SqlParameter("@resultPerPage", rowsperpage);
                sqlParams[2] = new SqlParameter("@ClientId", ClientId);
                sqlParams[3] = new SqlParameter("@SourceID", SourceID);
                sqlParams[4] = new SqlParameter("@WorkOrderID", WorkOrderID);
                sqlParams[5] = new SqlParameter("@status", statusCode);
                sqlParams[6] = new SqlParameter("@Type", Type);
                sqlParams[7] = new SqlParameter("@OrderBy", OrderBy);
                sqlParams[8] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[9] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));
                sqlParams[10] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[11] = new SqlParameter("@IsAdhoc", IsAdhoc);
                sqlParams[12] = new SqlParameter("@AssignedTo", AssignedTo);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllWorkOrder]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var WorkOrderList = new WorkOrders();
                    WorkOrderList.FetchingSerchWOData(WorkOrderList, safe);
                    getWorkOrder.Add(WorkOrderList);
                    data.workorderCount = Convert.ToInt32(reader["Workordercount"]);
                }
                data.WorkOrdersList = getWorkOrder;
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
        /// Description  : To delete WorkOrder 
        /// Created By   : Pavan
        /// Created Date : 30 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public int DeleteWorkOrderById(int ID, string StatusCode)
        {
            int result = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@StatusCode", StatusCode);

                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteWorkorderById", sqlParams);
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
        /// Description  : To Show all the Workorders SearchWO List 
        /// Created By   : hussain
        /// Created Date : 27 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the Workorders details available in database</returns>
        public static WorkOrderInfo GetWorkOrderDetailsById(int ID)
        {
            var data = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var getWorkOrder = new List<WorkOrders>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", ID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWorkorderById", sqlParams);
                var WorkOrderList = new WorkOrders();
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    WorkOrderList.FetchingSerchWODataByWO(WorkOrderList, safe);
                    getWorkOrder.Add(WorkOrderList);
                }
                data.WorkOrdersList = getWorkOrder;

                reader.NextResult();
                while (reader.Read())
                {
                    var Assignment = new WOAssignment();
                    Assignment.FetchWOAssignmentDetails(Assignment, safe);
                    data.WOAssignment.Add(Assignment);
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
        /// Description  : Get ID and Name from MWorkOrderType from database for Type dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date : 20 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WorkOrderInfo GetWorkOrderType()
        {
            var data = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWorkOrder = new List<WorkOrders>();

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetMWorkOrderType");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getIDFromDIType = new WorkOrders();
                    getIDFromDIType.FetchWorkOrderType(getIDFromDIType, safe);
                    lstWorkOrder.Add(getIDFromDIType);
                }
                data.WorkOrdersList = lstWorkOrder;
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
        /// Description  : Get Code and Name from MWOCategory from database for Category dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date :15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WorkOrderInfo GetMWOCategory()
        {
            var categoryData = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWorkOrder = new List<WorkOrders>();
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMWOCategory");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getCategory = new WorkOrders();
                    getCategory.FetchWOTypeOrCategory(getCategory, safe);
                    lstWorkOrder.Add(getCategory);
                }
                categoryData.WorkOrdersList = lstWorkOrder;
                return categoryData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return categoryData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get MWOType by Category Code.
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WorkOrderInfo GetMWOTypeByCategoryCode(string CategoryCode)
        {
            var WOTypeByCategoryCodeData = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWOType = new List<WorkOrders>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@CategoryCode", CategoryCode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMWOTypeByCategoryCode", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var GetMWOType = new WorkOrders();
                    GetMWOType.FetchWOTypeOrCategory(GetMWOType, safe);
                    lstWOType.Add(GetMWOType);
                }
                WOTypeByCategoryCodeData.WorkOrdersList = lstWOType;
                return WOTypeByCategoryCodeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOTypeByCategoryCodeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
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
        public static object GetDashbordDetails()
        {
            var data = new WorkOrderInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var getWorkOrder = new List<WorkOrders>();
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetDashbordDetails");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var WorkOrderList = new WorkOrders();
                    WorkOrderList.FetchingSerchWOData(WorkOrderList, safe);
                    getWorkOrder.Add(WorkOrderList);
                    data.workorderCount = Convert.ToInt32(reader["Workordercount"]);
                }
                data.WorkOrdersList = getWorkOrder;
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
        /// Description  : To get DashBord Details.
        /// Created By   : Sudheer  
        /// Created Date : 7th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static object ValidateWorkOrederById(int WOID, int UserID)
        {
            int output = 1;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];

                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@UserID", UserID);
                sqlParams[2] = new SqlParameter("@Output", 1);
                sqlParams[2].Direction = ParameterDirection.Output;
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpValidateWorkOrederById]", sqlParams);
                output = Convert.ToInt32(sqlParams[2].Value);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
            return output;
        }



    }

    #region InfoClass

    /// <summary>
    /// Description  : To do all events in same List
    /// Created By   : hussain
    /// Created Date : 27 May 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class WorkOrderInfo
    {
        public List<WorkOrders> WorkOrdersList { get; set; }
        public List<WOAssignment> WOAssignment { set; get; }
        public int workorderCount { get; set; }
        public WorkOrderInfo()
        {
            WOAssignment = new List<WOAssignment>();
        }
    }

    public class WOAssignment
    {
        public string AssignedTo { set; get; }
        public string GroupName { set; get; }
        public WOAssignment FetchWOAssignmentDetails(WOAssignment objAssignment, SafeDataReader dr)
        {
            objAssignment.AssignedTo = dr.GetString("AssignedTo");
            objAssignment.GroupName = dr.GetString("AssignedToGroup");
            return objAssignment;
        }

    }
    #endregion

    #region WOStatusAndAssignment

    public class WOStatusAndAssignment
    {
        private static ILog log = LogManager.GetLogger(typeof(WOStatusAndAssignment));

        public string StatusCode { set; get; }
        public string StatusName { set; get; }
        public int UserID { set; get; }
        public string UserName { set; get; }
        public int WOID { set; get; }
        public string Comment { set; get; }
        public string StatusDate { set; get; }
        public string ClientName { set; get; }
        public string GroupCode { set; get; }
        public string GroupName { set; get; }
        public string AssignedDate { set; get; }
        public string AssignedTo { set; get; }
        public string AssignedBy { set; get; }
        public string WOCode { set; get; }
        public DateTime DateForHistory { set; get; }


        public List<WOStatusAndAssignment> WOStatusAndAssignmentList { set; get; }
        public List<WOStatusAndAssignment> WOAssignmentList { set; get; }
        // public List<WOStatusAndAssignment> WOStatusList { set; get; }

        public WOStatusAndAssignment()
        {
            WOStatusAndAssignmentList = new List<WOStatusAndAssignment>();
            WOAssignmentList = new List<WOStatusAndAssignment>();
            // WOStatusList = new List<WOStatusAndAssignment>();
        }

        private WOStatusAndAssignment FetchAssignmentHistory(WOStatusAndAssignment objAssignment, SafeDataReader dr)
        {
            objAssignment.WOCode = dr.GetString("WOCode");
            objAssignment.AssignedTo = dr.GetString("AssignedTo");
            //objAssignment.GroupCode = dr.GetString("GroupCode");
            objAssignment.AssignedBy = dr.GetString("AssignedBy");
            objAssignment.DateForHistory = dr.GetDateTime("AssignedDate");
            objAssignment.AssignedDate = objAssignment.DateForHistory.ToString("dd MMM yyyy hh:mm:ss tt");
            return objAssignment;
        }

        private WOStatusAndAssignment FetchGroupHistory(WOStatusAndAssignment objAssignment, SafeDataReader dr)
        {
            objAssignment.WOCode = dr.GetString("WOCode");
            objAssignment.GroupCode = dr.GetString("GroupCode");
            objAssignment.AssignedBy = dr.GetString("CreatedBy");
            objAssignment.DateForHistory = dr.GetDateTime("CreatedDate");
            objAssignment.AssignedDate = objAssignment.DateForHistory.ToString("dd MMM yyyy hh:mm:ss tt");
            return objAssignment;
        }
        private WOStatusAndAssignment FetchStatusHistory(WOStatusAndAssignment objStatus, SafeDataReader dr)
        {
            objStatus.WOCode = dr.GetString("WOCode");
            objStatus.StatusCode = dr.GetString("StatusCode");
            objStatus.Comment = dr.GetString("Comment");
            objStatus.DateForHistory = dr.GetDateTime("StatusDate");
            objStatus.StatusDate = objStatus.DateForHistory.ToString("dd MMM yyyy hh:mm:ss tt");
            objStatus.StatusName = dr.GetString("Name");
            objStatus.AssignedBy = dr.GetString("AssignedBy");
            return objStatus;
        }

        private WOStatusAndAssignment FetchStatus(WOStatusAndAssignment objStatus, SafeDataReader dr)
        {
            objStatus.StatusCode = dr.GetString("Code");
            objStatus.StatusName = dr.GetString("Name");
            return objStatus;
        }
        private WOStatusAndAssignment FetchUsersForAssignedTo(WOStatusAndAssignment Users, SafeDataReader dr)
        {
            Users.UserID = dr.GetInt32("UserID");
            Users.UserName = dr.GetString("Name");
            return Users;
        }


        /// <summary>
        /// Description  : To do all events in same List
        /// Created By   : Shiva
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class WOStatusAndAssignmentInfo
        {
            public List<WOStatusAndAssignment> WOStatusAndAssignmentInfoList { get; set; }
        }
        /// <summary>
        /// Description  : GetMWOStatus from MWOStatus table 
        /// Created By   : Shiva
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WOStatusAndAssignmentInfo GetMWOStatus()
        {
            var data = new WOStatusAndAssignmentInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var getWorkOrderStatus = new List<WOStatusAndAssignment>();
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMWOStatus");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var WOStatus = new WOStatusAndAssignment();
                    WOStatus.FetchStatus(WOStatus, safe);
                    getWorkOrderStatus.Add(WOStatus);
                }
                data.WOStatusAndAssignmentInfoList = getWorkOrderStatus;
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
        /// Description  : Get UserID, Name from CSS1 for AssignedTo dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date : 15 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WOStatusAndAssignmentInfo GetCSS1UserDetailsForWOAssignment()
        {
            var WOAssignment = new WOStatusAndAssignmentInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWOAssignment = new List<WOStatusAndAssignment>();
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCSS1UserDetailsForAssignedTo");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getWOAssignment = new WOStatusAndAssignment();
                    getWOAssignment.FetchUsersForAssignedTo(getWOAssignment, safe);
                    lstWOAssignment.Add(getWOAssignment);
                }
                WOAssignment.WOStatusAndAssignmentInfoList = lstWOAssignment;
                return WOAssignment;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOAssignment;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : To Save the Group in WO and WOAssignment.
        /// Created By   : SHIVA
        /// Created Date : 10 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Saved status</returns>
        public static string SaveWOAssignmentGroup(int WOID, string WOCode, string AssignedGroup, int AssignedBy, bool Billable)
        {
            string strWOCode = string.Empty;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@WOCode", WOCode);
                sqlParams[1] = new SqlParameter("@WOID", WOID);
                sqlParams[2] = new SqlParameter("@AssignedToGroup", AssignedGroup);
                sqlParams[3] = new SqlParameter("@AssignedBy", AssignedBy);
                sqlParams[4] = new SqlParameter("@Billable", Billable);
                var WOCodeReader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPSaveWOGroup", sqlParams);
                while (WOCodeReader.Read())
                {
                    strWOCode = Convert.ToString(WOCodeReader["WOCode"]);

                }
                return strWOCode;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return strWOCode;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Save AssignedTo WOAssignment and WO.
        /// Created By   : SHIVA
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Saved status</returns>
        public static string SaveWOAssignmentAssignedTo(int WorkOrderID, string AssignedTo, int AssignedBy, bool Billable)
        {
            string result = string.Empty;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WorkOrderID);
                sqlParams[1] = new SqlParameter("@AssignedTo", AssignedTo);
                sqlParams[2] = new SqlParameter("@AssignedBy", AssignedBy);
                sqlParams[3] = new SqlParameter("@Billable", Billable);
                var WOCodeReader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPInsertOrUpdateWOAssignmentAssignedTo", sqlParams);
                while (WOCodeReader.Read())
                {
                    result = Convert.ToString(WOCodeReader["AssignedTo"]);
                }
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
        /// Description  : To Insert or Update the WOAssignment
        /// Created By   : SHIVA
        /// Created Date : 18 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Inserted or Updated status</returns>
        public static int InsertWOStatus(string StatusCode, string WorkOrderID, string Comment, int StatusBy)
        {
            int reader = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@WOID", WorkOrderID);
                sqlParams[1] = new SqlParameter("@StatusCode", StatusCode);
                sqlParams[2] = new SqlParameter("@Comment", Comment);
                sqlParams[3] = new SqlParameter("@StatusBy", @StatusBy);

                reader = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPInsertWOStatus", sqlParams);
                return reader;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return reader;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Insert or Update the WO IsAdhoc
        /// Created By   : Pavan
        /// Created Date : 30 Jan 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Inserted or Updated IsAdhoc status</returns>
        public static int SaveWOAdhoc(int WorkOrderID, bool IsAdhoc, int CreatedBy)
        {
            int reader = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WorkOrderID);
                sqlParams[1] = new SqlParameter("@IsAdhoc", IsAdhoc);
                sqlParams[2] = new SqlParameter("@CreatedBy", CreatedBy);

                reader = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdateWOAdhoc", sqlParams);
                return reader;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return reader;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Update Billing Party By WOID in WO table
        /// Created By   : Sudheer
        /// Created Date : 14th Oct 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Updated Billing Party status</returns>
        public static int UpdateBillingPartyByWOID(int WOID, int ClientOrCustomer, string SourceID)
        {
            int updatedStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@ClientOrCustomer", ClientOrCustomer);
                sqlParams[2] = new SqlParameter("@SourceID", SourceID);

                updatedStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdateBillingPartyByWOID", sqlParams);
                return updatedStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return updatedStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get WOStatus History by WOID
        /// Created By   : SHIVA
        /// Created Date : 25 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOStatus History by WOID</returns>
        public static WOStatusAndAssignmentInfo GetWOStatusHistoryByWOID(int WOID)
        {
            var WOStatus = new WOStatusAndAssignmentInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWOStatus = new List<WOStatusAndAssignment>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOStatusHistoryByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getWOAssignment = new WOStatusAndAssignment();
                    getWOAssignment.FetchStatusHistory(getWOAssignment, safe);
                    lstWOStatus.Add(getWOAssignment);
                }
                WOStatus.WOStatusAndAssignmentInfoList = lstWOStatus;
                return WOStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get WOAssignment History by WOID
        /// Created By   : SHIVA
        /// Created Date : 25 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WOAssignment History by WOID</returns>
        public static WOStatusAndAssignmentInfo GetWOAssignmentHistoryByWOID(int WOID)
        {
            var WOAssignment = new WOStatusAndAssignmentInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWOAssignment = new List<WOStatusAndAssignment>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOAssignmentHistoryByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getWOAssignment = new WOStatusAndAssignment();
                    getWOAssignment.FetchAssignmentHistory(getWOAssignment, safe);
                    lstWOAssignment.Add(getWOAssignment);
                }
                WOAssignment.WOStatusAndAssignmentInfoList = lstWOAssignment;
                return WOAssignment;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOAssignment;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get WO Group History by WOID
        /// Created By   : SHIVA
        /// Created Date : 22 Dec 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>WO Group History by WOID</returns>
        public static WOStatusAndAssignmentInfo GetWOGroupHistoryByWOID(int WOID)
        {
            var WOGroupHistory = new WOStatusAndAssignmentInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstWOGroupHistory = new List<WOStatusAndAssignment>();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetWOGroupHistoryByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getWOGroupHistory = new WOStatusAndAssignment();
                    getWOGroupHistory.FetchGroupHistory(getWOGroupHistory, safe);
                    lstWOGroupHistory.Add(getWOGroupHistory);
                }
                WOGroupHistory.WOStatusAndAssignmentInfoList = lstWOGroupHistory;
                return WOGroupHistory;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WOGroupHistory;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
    }

    #endregion

    #region GroupInfo
    public class GroupInfo
    {
        private static ILog log = LogManager.GetLogger(typeof(GroupInfo));

        public int GroupID { set; get; }
        public string GroupName { set; get; }
        public List<GroupInfo> GroupInfoList { get; set; }
        public GroupInfo()
        {
            GroupInfoList = new List<GroupInfo>();
        }


        private GroupInfo FetchGroupInfo(GroupInfo getGroupInfo, SafeDataReader dr)
        {
            getGroupInfo.GroupID = dr.GetInt32("GroupID");
            getGroupInfo.GroupName = dr.GetString("GroupName");
            return getGroupInfo;
        }


        /// <summary>
        /// Description  : Get the Group information from CSS1
        /// Created By   : Shiva
        /// Created Date : 10 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static GroupInfo GetCSS1GroupDetails()
        {
            var data = new GroupInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var lstGroupInfo = new List<GroupInfo>();
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetCSS1GroupDetails");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var getGroupInfo = new GroupInfo();
                    getGroupInfo.FetchGroupInfo(getGroupInfo, safe);
                    lstGroupInfo.Add(getGroupInfo);
                }
                data.GroupInfoList = lstGroupInfo;
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

    public class WOIncorpValidation
    {
        private static ILog log = LogManager.GetLogger(typeof(WOIncorpValidation));

        public string ManditoryField { get; set; }

        private WOIncorpValidation FetchingIncorpFields(WOIncorpValidation WOINCORP, SafeDataReader dr)
        {
            WOINCORP.ManditoryField = dr.GetString("MandatoryField");
            return WOINCORP;
        }

        /// <summary>
        /// Description  : To get WO INCORP Manditory Fields.
        /// Created By   : Pavan  
        /// Created Date : 15th MAY 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static List<WOIncorpValidation> GetValidationForIncorp(int WOID)
        {
            var data = new List<WOIncorpValidation>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpValidateMandatoryFields", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var WorkOrderList = new WOIncorpValidation();
                    WorkOrderList.FetchingIncorpFields(WorkOrderList, safe);
                    data.Add(WorkOrderList);
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

}
