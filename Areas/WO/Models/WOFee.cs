# region Document Header
//Created By       : Shiva 
//Created Date     : 12 Sep 2014
//Description      : For all Fee Actions
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

using CSS2.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSS2.Areas.WO.Models
{
    public class WOFee
    {
        private static ILog log = LogManager.GetLogger(typeof(WOFee));

        #region Properties

        public int ID { set; get; }
        public string WOCode { set; get; }
        public string ClientName { set; get; }
        public int WOID { set; get; }
        public int FeeType { set; get; }
        public int Units { set; get; }
        public decimal Amount { set; get; }
        public string ItemNumber { set; get; }
        public string Code { set; get; }
        public decimal? UnitPrice { set; get; }
        public string Description { set; get; }
        public string FeeDescriptionToolTip { set; get; }
        public bool IsAdhoc { set; get; }
        public bool IsArchived { set; get; }
        public bool IsBilled { set; get; }
        public string NoteType { set; get; }
        public int NoteCount { set; get; }
        public int RefID { set; get; }
        public int SavedBy { set; get; }
        public string CreatedDate { set; get; }
        public string FeeItemToolTip { set; get; }
        public string FeeInhouseComment { set; get; }
        public int ACCPACStatus { set; get; }
        public string ACCPACDescription { get; set; }
        public string ACCPACExplanation { get; set; }
        public string InvoiceNumber { get; set; }

        #endregion

        #region Fetching Data

        private WOFee FetchFeeType(WOFee FeeType, SafeDataReader dr)
        {
            FeeType.ID = dr.GetInt32("ID");
            FeeType.ItemNumber = dr.GetString("ItemNumber");
            FeeType.Code = dr.GetString("Code");
            FeeType.FeeItemToolTip = dr.GetString("Name");

            return FeeType;
        }
        private WOFee FetchFeeDetailsByItemNumber(WOFee Fee, SafeDataReader dr)
        {
            Fee.Code = dr.GetString("Code");
            if (dr["UnitPrice"] != null)
            {
                Fee.UnitPrice = dr.GetDecimal("UnitPrice");
            }
            else
            {
                Fee.UnitPrice = null;
            }
            Fee.Description = dr.GetString("Description");
            Fee.Units = dr.GetInt32("Units");
            return Fee;
        }
        private WOFee FetchFeeItemsByWOID(WOFee Item, SafeDataReader dr)
        {
            Item.ID = dr.GetInt32("ID");
            Item.WOCode = dr.GetString("WOCode");
            Item.ClientName = dr.GetString("ClientName");
            Item.ItemNumber = dr.GetString("ItemNumber");
            Item.Code = dr.GetString("Code");
            Item.Units = dr.GetInt32("Units");
            Item.Amount = dr.GetDecimal("Amount");
            Item.IsAdhoc = dr.GetBoolean("IsAdhoc");
            Item.IsArchived = dr.GetBoolean("IsArchived");
            Item.IsBilled = dr.GetBoolean("IsBilled");
            Item.Description = dr.GetString("Description");
            string toolTip = dr.GetString("Description");
            if (toolTip.Contains("~^"))
            {
                toolTip = toolTip.Replace("~^", Environment.NewLine);
            }
            Item.FeeDescriptionToolTip = toolTip;
            int descLength = Item.Description.Length;
            if (descLength > 10)
            {
                string strNewDesc = Item.Description.Substring(0, 10);
                strNewDesc = strNewDesc + "...";
                Item.Description = strNewDesc;
            }
            Item.UnitPrice = dr.GetDecimal("UnitPrice");
            Item.NoteCount = dr.GetInt32("NoteCount");
            Item.NoteType = dr.GetString("NoteType");
            Item.RefID = Item.ID;
            Item.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            Item.FeeItemToolTip = dr.GetString("FeeToolTip");
            Item.ACCPACStatus = dr.GetInt32("ACCPACStatus");
            Item.ACCPACDescription = dr.GetString("ACCPACDescription");
            Item.ACCPACExplanation = dr.GetString("Explaination");
            return Item;
        }

        private WOFee FetchFeeItemByFeeID(WOFee Item, SafeDataReader dr)
        {
            Item.ID = dr.GetInt32("ID");
            Item.WOID = dr.GetInt32("WOID");
            Item.WOCode = dr.GetString("WOCode");
            Item.ClientName = dr.GetString("ClientName");
            Item.Code = dr.GetString("Code");
            Item.ItemNumber = dr.GetString("ItemNumber");
            Item.FeeType = dr.GetInt32("FeeType");
            Item.Units = dr.GetInt32("Units");
            Item.UnitPrice = dr.GetDecimal("UnitPrice");
            Item.Amount = dr.GetDecimal("Amount");
            Item.IsAdhoc = dr.GetBoolean("IsAdhoc");
            Item.IsArchived = dr.GetBoolean("IsArchived");
            Item.IsBilled = dr.GetBoolean("IsBilled");
            Item.Description = dr.GetString("Description");
            Item.NoteCount = dr.GetInt32("NoteCount");
            Item.NoteType = dr.GetString("NoteType");
            Item.RefID = Item.ID;
            Item.ACCPACStatus = dr.GetInt32("ACCPACStatus");
            Item.ACCPACDescription = dr.GetString("ACCPACDescription");
            Item.ACCPACExplanation = dr.GetString("Explaination");
            Item.InvoiceNumber = dr.GetString("InvoiceNumber");
            return Item;
        }
        #endregion

        #region Database Fee Actions

        /// <summary>
        /// Description  : Get FeeType from database(MFeeType table). 
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        /// 
        public static FeeInfo GetMFeeType()
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMFeeType");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var FeeItem = new WOFee();
                    FeeItem.FetchFeeType(FeeItem, safe);
                    MFeeData.FeeList.Add(FeeItem);
                }
                return MFeeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MFeeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get the MDIType Details By ItemNumber for the partial view.
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        /// 
        public static FeeInfo GetMDITypeDetailsByItemNumber(int FeeID)
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", FeeID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMFeeTypeDetailsByID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new WOFee();
                    Item.FetchFeeDetailsByItemNumber(Item, safe);
                    MFeeData.FeeList.Add(Item);
                }
                return MFeeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MFeeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To Insert or Update Fee Item.
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee saved Status</returns>
        /// 
        public int InsertOrUpdateFeeItem()
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[11];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@FeeType", this.FeeType);
                sqlParams[2] = new SqlParameter("@Units", this.Units);
                sqlParams[3] = new SqlParameter("@Amount", this.Amount);
                sqlParams[4] = new SqlParameter("@Description", this.Description);
                sqlParams[5] = new SqlParameter("@IsAdhoc", this.IsAdhoc);
                sqlParams[6] = new SqlParameter("@IsArchived", this.IsArchived);
                sqlParams[7] = new SqlParameter("@UnitPrice", this.UnitPrice);
                sqlParams[8] = new SqlParameter("@SavedBy", this.SavedBy);
                sqlParams[9] = new SqlParameter("@WOID", this.WOID);
                sqlParams[10] = new SqlParameter("@InHouseComment", this.FeeInhouseComment);

                return ID = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPInsertOrUpdateFeeItem", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return ID;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }



        /// <summary>
        /// Description  : To get Fee Items by WOID.
        /// Created By   : Shiva
        /// Created Date : 12 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee data</returns>
        /// 
        public static FeeInfo GetFeeItemsByWOID(int WOID, int startPage, int resultPerPage)
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WOID", WOID);
                sqlParams[1] = new SqlParameter("@startPage", startPage);
                sqlParams[2] = new SqlParameter("@resultPerPage", resultPerPage);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetFeeItemDetailsByWOID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new WOFee();
                    Item.FetchFeeItemsByWOID(Item, safe);
                    MFeeData.FeeList.Add(Item);
                    MFeeData.FeeCount = Convert.ToInt32(reader["FeeCount"]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var Instate = new FeeInstate();
                    Instate.FetchFeeInstate(Instate, safe);
                    MFeeData.FEEInstate.Add(Instate);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var ActionRules = new FeeActionRules();
                    ActionRules.FetchFeeActionRules(ActionRules, safe);
                    MFeeData.FEEActionRules.Add(ActionRules);
                }

                return MFeeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MFeeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get All Fee Items.
        /// Created By   : Pavan
        /// Created Date : 23 Apr 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee data</returns>
        /// 
        public static FeeInfo GetAllFeeItems(int startPage, int resultPerPage, string clientID, string SourceID, string WO, string Type, string IsBilled, string IsArchived, string IsAdhoc, string OrderBy, string FromDate, string ToDate, int ACCPACStatus)
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@clientID", clientID);
                sqlParams[3] = new SqlParameter("@SourceID", SourceID);
                sqlParams[4] = new SqlParameter("@WO", WO);
                sqlParams[5] = new SqlParameter("@Type", Type);
                sqlParams[6] = new SqlParameter("@IsBilled", IsBilled);
                sqlParams[7] = new SqlParameter("@IsArchived", IsArchived);
                sqlParams[8] = new SqlParameter("@IsAdhoc", IsAdhoc);
                sqlParams[9] = new SqlParameter("@OrderBy", OrderBy);
                sqlParams[10] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[11] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));
                sqlParams[12] = new SqlParameter("@ACCPACStatus", ACCPACStatus);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetAllFeeItems", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new WOFee();
                    Item.FetchFeeItemsByWOID(Item, safe);
                    MFeeData.FeeList.Add(Item);
                    MFeeData.FeeCount = Convert.ToInt32(reader["FeeCount"]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var Instate = new FeeInstate();
                    Instate.FetchFeeInstate(Instate, safe);
                    MFeeData.FEEInstate.Add(Instate);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var ActionRules = new FeeActionRules();
                    ActionRules.FetchFeeActionRules(ActionRules, safe);
                    MFeeData.FEEActionRules.Add(ActionRules);
                }

                return MFeeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MFeeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Get Fee Item by ID.
        /// Created By   : Shiva
        /// Created Date : 15 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Data</returns>
        /// 
        internal static object GetFeeItemByFeeID(int FeeID)
        {
            var MFeeData = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@FeeID", FeeID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetWOFeeDetailsByFeeID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new WOFee();
                    Item.FetchFeeItemByFeeID(Item, safe);
                    MFeeData.FeeList.Add(Item);
                }
                return MFeeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MFeeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Delete Fee Item by ID.
        /// Created By   : Shiva
        /// Created Date : 15 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>Fee Delete status</returns>
        internal static int DeleteFeeItemByFeeID(string FeeIDs)
        {
            int FeeStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@FeeIDs", FeeIDs);
                return FeeStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPDeleteFeeItemByID", sqlParams);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return FeeStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Update Archived To Active or In-Active For Checked Id's Of Fee Items 
        /// Created By   : Shiva
        /// Created Date : 16 Sep 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="FeeIds"></param>
        /// <returns></returns>
        public static int ArchivedActionOnFeeItems(string FeeIds, int Archived, string ForState, int SavedBy, string Comment)
        {
            int returnStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@IDs", FeeIds);
                sqlParams[1] = new SqlParameter("@IsArchive", Archived);
                sqlParams[2] = new SqlParameter("@ForState", ForState);
                sqlParams[3] = new SqlParameter("@SavedBy", SavedBy);
                sqlParams[4] = new SqlParameter("@InHouseComment", Comment);
                sqlParams[5] = new SqlParameter("@ActionStatus", 0);
                sqlParams[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPArchivedActionRulesOnFeeItems", sqlParams);
                returnStatus = Convert.ToInt32(sqlParams[5].Value);
                return returnStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Update IsAdhoc To Active or In-Active For Checked Id's Of Fee Items 
        /// Created By   : Shiva
        /// Created Date : 16 Sep 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="FeeIds"></param>
        /// <returns>Updated status</returns>
        public static int AdhocActionOnFeeItems(string FeeIDs, int Adhoc, string ForState, int SavedBy)
        {
            int returnStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@IDs", FeeIDs);
                sqlParams[1] = new SqlParameter("@IsAdhoc", Adhoc);
                sqlParams[2] = new SqlParameter("@ForState", ForState);
                sqlParams[3] = new SqlParameter("@SavedBy", SavedBy);
                sqlParams[4] = new SqlParameter("@ActionStatus", 0);
                sqlParams[4].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPAdhocActionRulesOnFeeItems", sqlParams);
                returnStatus = Convert.ToInt32(sqlParams[4].Value);
                return returnStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Update ACCPAC Status for Fee Items 
        /// Created By   : Pavan
        /// Created Date : 13 March 2015
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        public static int UpdateFeeAccpacStatusByFeeID(int FeeID, int SavedBy)
        {
            int returnStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@FeeID", FeeID);
                sqlParams[1] = new SqlParameter("@SavedBy", SavedBy);
                returnStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdateFeeItemACCPACStatus", sqlParams);
                return returnStatus;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return returnStatus;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        #endregion

        #region FeeActionRules
        public class FeeInstate
        {
            public int FeeID { get; set; }
            public string InState { get; set; }

            public FeeInstate FetchFeeInstate(FeeInstate Di, SafeDataReader dr)
            {
                Di.FeeID = dr.GetInt32("FeeID");
                Di.InState = dr.GetString("InState");
                return Di;
            }
        }
        public class FeeActionRules
        {
            public int FeeID { get; set; }
            public string ForState { get; set; }
            public int DO { get; set; }
            public int UNDO { get; set; }


            public FeeActionRules FetchFeeActionRules(FeeActionRules Di, SafeDataReader dr)
            {
                Di.FeeID = dr.GetInt32("FeeID");
                Di.ForState = dr.GetString("ForState");
                Di.DO = dr.GetInt32("do");
                Di.UNDO = dr.GetInt32("undo");
                return Di;
            }

        }
        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events.
        /// Created By   : Shiva
        /// Created Date : 19 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class FeeInfo
        {
            public List<WOFee> FeeList { get; set; }
            public List<FeeInstate> FEEInstate { get; set; }
            public List<FeeActionRules> FEEActionRules { get; set; }
            public int FeeCount { get; set; }
            public FeeInfo()
            {
                FeeList = new List<WOFee>();
                FEEInstate = new List<FeeInstate>();
                FEEActionRules = new List<FeeActionRules>();

            }
        }
        #endregion

    }
}