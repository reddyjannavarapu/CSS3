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
    #endregion

    public class DisbursementItem
    {
        private static ILog log = LogManager.GetLogger(typeof(DisbursementItem));

        #region Properties

        public int ID { set; get; }
        public int WOID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool NeedVerification { set; get; }
        public string DescriptionToolTip { set; get; }
        public bool Status { set; get; }
        public string Code { set; get; }
        public string ItemNumber { set; get; }
        public string Type { set; get; }
        public string ClientId { set; get; }
        public int Units { set; get; }
        public string ItemCode { set; get; }
        public string VenderRefID { set; get; }
        public decimal Amount { set; get; }
        public decimal? UnitPrice { set; get; }
        public string DiItemToolTip { set; get; }
        public bool IsAdhoc { set; get; }
        public bool IsVerified { set; get; }
        public bool IsArchived { set; get; }
        public bool IsBilled { set; get; }
        public int ACCPACStatus { set; get; }
        public int CreatedBy { get; set; }
        public string WorkOrderNumber { get; set; }
        public string ClientNumber { get; set; }
        public string NoteType { set; get; }
        public string DateIncurred { set; get; }
        public int NoteCount { set; get; }
        public int DICount { set; get; }
        public int RefID { set; get; }
        public string WOCode { set; get; }
        public string ClientCode { set; get; }
        public string GroupCode { set; get; }
        public int ClientOrCustomerID { set; get; }
        public string SourceID { set; get; }
        public string StatusCode { set; get; }
        public bool IsPostedToCss1 { set; get; }
        public string PostedTOCss1Date { set; get; }
        public bool Billable { set; get; }
        public string ClientName { set; get; }
        public decimal VendorAmount { set; get; }
        public int VRID { set; get; }
        public string CreatedDate { set; get; }
        public int Quantity { get; set; }
        public string ACCPACDescription { get; set; }
        public string ACCPACExplanation { get; set; }
        public string InvoiceNumber { get; set; }
        public bool IsMatched { get; set; }

        #endregion

        #region Fetching Data

        #region Fetching Data For Disbursement partial view

        private DisbursementItem FetchDIType(DisbursementItem dIType, SafeDataReader dr)
        {
            dIType.ID = dr.GetInt32("ID");
            dIType.ItemNumber = dr.GetString("ItemNumber");
            dIType.Code = dr.GetString("Code");
            dIType.Name = dr.GetString("Name");
            return dIType;
        }
        private DisbursementItem FetchCurrencyType(DisbursementItem CurrencyType, SafeDataReader dr)
        {
            CurrencyType.ID = dr.GetInt32("ID");
            CurrencyType.Code = dr.GetString("Code");
            return CurrencyType;
        }
        private DisbursementItem FetchDITypeDetailsByItemNumber(DisbursementItem dIType, SafeDataReader dr)
        {
            dIType.Code = dr.GetString("Code");
            if (dr["UnitPrice"] != null)
            {
                dIType.UnitPrice = dr.GetDecimal("UnitPrice");
            }
            else
            {
                dIType.UnitPrice = null;
            }
            dIType.Description = dr.GetString("Description");
            dIType.NeedVerification = dr.GetBoolean("NeedVerification");
            dIType.Quantity = dr.GetInt32("Units");
            return dIType;
        }

        #endregion

        private DisbursementItem FetchIDAndNameFromWOType(DisbursementItem WOType, SafeDataReader dr)
        {
            WOType.ID = dr.GetInt32("ID");
            WOType.Name = dr.GetString("Name");
            return WOType;
        }

        private DisbursementItem FetchInsertedWO(DisbursementItem InsertedWO, SafeDataReader dr)
        {
            InsertedWO.ID = dr.GetInt32("ID");
            InsertedWO.WOCode = dr.GetString("WOCode").Trim();
            InsertedWO.ClientName = dr.GetString("ClientName");
            return InsertedWO;
        }

        private DisbursementItem FetchDisbursementItems(DisbursementItem disbursementItems, SafeDataReader dr)
        {
            disbursementItems.ID = dr.GetInt32("ID");
            disbursementItems.WOID = dr.GetInt32("WOID");
            disbursementItems.Amount = dr.GetDecimal("Amount");
            disbursementItems.ItemCode = dr.GetString("ItemCode");
            disbursementItems.Description = dr.GetString("Description");
            disbursementItems.VenderRefID = dr.GetString("VenderRefId");
            disbursementItems.Units = dr.GetInt32("Units");
            disbursementItems.Code = dr.GetString("Code");
            disbursementItems.Type = dr.GetString("DIType");
            disbursementItems.IsBilled = dr.GetBoolean("IsBilled");
            disbursementItems.IsAdhoc = dr.GetBoolean("IsAdhoc");
            disbursementItems.IsArchived = dr.GetBoolean("IsArchived");
            disbursementItems.IsVerified = dr.GetBoolean("IsVerified");
            return disbursementItems;
        }

        //Need to check 
        private DisbursementItem FetchDisbursementItemsByID(DisbursementItem SearchDisbursementItem, SafeDataReader dr)
        {
            SearchDisbursementItem.WorkOrderNumber = dr.GetString("WOCode").Trim();
            SearchDisbursementItem.ClientNumber = dr.GetString("ClientName");
            SearchDisbursementItem.ID = dr.GetInt32("ID");
            SearchDisbursementItem.RefID = SearchDisbursementItem.ID;
            SearchDisbursementItem.WOID = dr.GetInt32("WOID");
            SearchDisbursementItem.Type = dr.GetString("DIType");
            SearchDisbursementItem.Units = dr.GetInt32("Units");
            SearchDisbursementItem.Code = dr.GetString("ItemCode");
            SearchDisbursementItem.ItemNumber = dr.GetString("ItemNumber");
            SearchDisbursementItem.Amount = dr.GetDecimal("Amount");
            SearchDisbursementItem.Description = dr.GetString("Description");
            SearchDisbursementItem.DateIncurred = dr.GetString("DateIncurred");
            SearchDisbursementItem.VenderRefID = dr.GetString("VenderRefId");
            SearchDisbursementItem.IsVerified = dr.GetBoolean("IsVerified");
            SearchDisbursementItem.IsAdhoc = dr.GetBoolean("IsAdhoc");
            SearchDisbursementItem.IsArchived = dr.GetBoolean("IsArchived");
            SearchDisbursementItem.IsBilled = dr.GetBoolean("IsBilled");
            SearchDisbursementItem.UnitPrice = dr.GetDecimal("UnitPrice");
            SearchDisbursementItem.NeedVerification = dr.GetBoolean("NeedVerification");
            SearchDisbursementItem.NoteCount = dr.GetInt32("NoteCount");
            SearchDisbursementItem.NoteType = dr.GetString("NoteType");
            SearchDisbursementItem.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt");
            SearchDisbursementItem.ACCPACStatus = dr.GetInt32("ACCPACStatus");
            SearchDisbursementItem.ACCPACDescription = dr.GetString("ACCPACDescription");
            SearchDisbursementItem.ACCPACExplanation = dr.GetString("Explaination");
            SearchDisbursementItem.InvoiceNumber = dr.GetString("InvoiceNumber");
            SearchDisbursementItem.IsMatched = dr.GetBoolean("IsMatched");
            return SearchDisbursementItem;
        }
        //need to check
        private DisbursementItem FetchDisbursementItemsForSearch(DisbursementItem SearchDisbursementItem, SafeDataReader dr)
        {
            SearchDisbursementItem.ID = dr.GetInt32("ID");
            SearchDisbursementItem.RefID = SearchDisbursementItem.ID;
            SearchDisbursementItem.WOID = dr.GetInt32("WOID");
            SearchDisbursementItem.Code = dr.GetString("Code");
            SearchDisbursementItem.ItemNumber = dr.GetString("ItemNumber");
            SearchDisbursementItem.DiItemToolTip = dr.GetString("DIItemToolTip");
            SearchDisbursementItem.Name = dr.GetString("Name");
            SearchDisbursementItem.Units = dr.GetInt32("Units");
            SearchDisbursementItem.ClientName = dr.GetString("ClientName");
            SearchDisbursementItem.UnitPrice = dr.GetDecimal("UnitPrice");
            SearchDisbursementItem.Amount = dr.GetDecimal("Amount");
            SearchDisbursementItem.WorkOrderNumber = dr.GetString("WOCode").Trim();
            //SearchDisbursementItem.Currency = dr.GetString("Currency");
            SearchDisbursementItem.VenderRefID = dr.GetString("VenderRefId");
            SearchDisbursementItem.DateIncurred = dr.GetString("DateIncurred");
            SearchDisbursementItem.Description = dr.GetString("Description");
            SearchDisbursementItem.NoteCount = dr.GetInt32("NoteCount");
            SearchDisbursementItem.NoteType = dr.GetString("NoteType");

            string toolTip = dr.GetString("Description");
            if (toolTip.Contains("~^"))
            {
                toolTip = toolTip.Replace("~^", Environment.NewLine);
            }
            SearchDisbursementItem.DescriptionToolTip = toolTip;
            int descLength = SearchDisbursementItem.Description.Length;
            if (descLength > 10)
            {
                string strNewDesc = SearchDisbursementItem.Description.Substring(0, 10);
                strNewDesc = strNewDesc + "...";
                SearchDisbursementItem.Description = strNewDesc;
            }

            SearchDisbursementItem.ACCPACStatus = dr.GetInt32("ACCPACStatus");
            SearchDisbursementItem.ACCPACDescription = dr.GetString("ACCPACDescription");
            SearchDisbursementItem.ACCPACExplanation = dr.GetString("Explaination");
            SearchDisbursementItem.IsAdhoc = dr.GetBoolean("IsAdhoc");
            SearchDisbursementItem.IsArchived = dr.GetBoolean("IsArchived");
            SearchDisbursementItem.IsBilled = dr.GetBoolean("IsBilled");
            SearchDisbursementItem.IsVerified = dr.GetBoolean("IsVerified");
            SearchDisbursementItem.CreatedDate = dr.GetDateTime("CreatedDate").ToString("dd MMM yyyy hh:mm:ss tt");

            return SearchDisbursementItem;
        }



        #endregion
        public class DIFeeInvoicePreview
        {

            #region Properties

            public decimal Amount { set; get; }
            public string ACCPACCode { set; get; }
            public string Description { set; get; }
            public string Name { set; get; }
            public string AddressLine1 { set; get; }
            public string AddressLine2 { set; get; }
            public string AddressLine3 { set; get; }

            #endregion

            #region FetchMethod
            public DIFeeInvoicePreview FetchInvoicePreview(DIFeeInvoicePreview getDIInvoicePreview, SafeDataReader dr)
            {
                getDIInvoicePreview.Amount = dr.GetDecimal("Amount");
                getDIInvoicePreview.ACCPACCode = dr.GetString("ItemNumber");
                getDIInvoicePreview.Description = dr.GetString("Description");
                return getDIInvoicePreview;
            }
            public DIFeeInvoicePreview FetchInvoicePreviewAddess(DIFeeInvoicePreview getDIInvoicePreviewAddress, SafeDataReader dr)
            {
                getDIInvoicePreviewAddress.Name = dr.GetString("Name");
                getDIInvoicePreviewAddress.AddressLine1 = dr.GetString("AddressLine1");
                getDIInvoicePreviewAddress.AddressLine2 = dr.GetString("AddressLine2");
                getDIInvoicePreviewAddress.AddressLine3 = dr.GetString("AddressLine3");
                return getDIInvoicePreviewAddress;
            }

            #endregion

        }
        #region DataBase Methods

        #region WorkOrder Actions

        /// <summary>
        /// Description  : Get MWorkOrderType from database for Type dropdown in WorkOrder. 
        /// Created By   : Shiva
        /// Created Date : 20 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static WorkOrdersAndDItemsInfo GetWorkOrderType()
        {
            var data = new WorkOrdersAndDItemsInfo();

            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetMWorkOrderType");
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var getIDFromDIType = new DisbursementItem();
                getIDFromDIType.FetchIDAndNameFromWOType(getIDFromDIType, safe);
                data.OrdersList.Add(getIDFromDIType);
            }
            return data;
        }

        /// <summary>
        /// Created By    : Pavan
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Insert the Type, Description, Status to Work order  </returns>
        /// </summary>
        public WorkOrdersAndDItemsInfo InsertWorkOrder()
        {

            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@Type", this.Type);
            sqlParams[1] = new SqlParameter("@Billable", this.Billable);
            sqlParams[2] = new SqlParameter("@Description", this.Description);
            //sqlParams[4] = new SqlParameter("@Status", this.Status);
            sqlParams[3] = new SqlParameter("@CreatedBy", this.CreatedBy);
            //  sqlParams[5] = new SqlParameter("@ID", this.ID);
            sqlParams[4] = new SqlParameter("@GroupCode", this.GroupCode);
            sqlParams[5] = new SqlParameter("@IsAdhoc", this.IsAdhoc);
            sqlParams[6] = new SqlParameter("@ClientOrCustomerID", this.ClientOrCustomerID);
            sqlParams[7] = new SqlParameter("@SourceID", this.SourceID);
            sqlParams[8] = new SqlParameter("@StatusCode", this.StatusCode);
            sqlParams[9] = new SqlParameter("@IsPostedToCss1", this.IsPostedToCss1);
            if (this.IsPostedToCss1 == true)
                sqlParams[10] = new SqlParameter("@PostedTOCss1Date", DateTime.Now);
            else
                sqlParams[10] = new SqlParameter("@PostedTOCss1Date", DBNull.Value);

            //sqlParams[12] = new SqlParameter("@Output", 0);
            //sqlParams[12].Direction = ParameterDirection.Output;

            var data = new WorkOrdersAndDItemsInfo();
            var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertWorkOrder", sqlParams);
            var safe = new SafeDataReader(reader);
            while (reader.Read())
            {
                var getWO = new DisbursementItem();
                FetchInsertedWO(getWO, safe);
                data.OrdersList.Add(getWO);
            }
            return data;
            // int output = Convert.ToInt32(sqlParams[12].Value);
            // return output;
        }




        #endregion

        #region Partial DisbursementItems Actions

        /// <summary>
        /// Description  : Get ID and Code from MDIType from database for Type dropdown in Partial view. 
        /// Created By   : Shiva
        /// Created Date : 19 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        /// 
        public static WorkOrdersAndDItemsInfo GetMDIType(string wocode)
        {
            var MDITypeData = new WorkOrdersAndDItemsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@wocode", wocode);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetMDIType]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new DisbursementItem();
                    Item.FetchDIType(Item, safe);
                    MDITypeData.OrdersList.Add(Item);
                }
                return MDITypeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MDITypeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : To get the MDIType Details By ItemNumber for the partial view.
        /// Created By   : Shiva
        /// Created Date : 4 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        /// 
        public static WorkOrdersAndDItemsInfo GetMDITypeDetailsByItemNumber(int DIID)
        {
            var MCurrencyTypeData = new WorkOrdersAndDItemsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", DIID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMDITypeDetailsByID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new DisbursementItem();
                    Item.FetchDITypeDetailsByItemNumber(Item, safe);
                    MCurrencyTypeData.OrdersList.Add(Item);
                }
                return MCurrencyTypeData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MCurrencyTypeData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        /// <summary>
        /// Description  : Save disbursement items to database.
        /// Created By   : Shiva
        /// Created Date : 19 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        /// 
        public static int InsertAndUpdateDisbursementItemsData(int ID, int ItemNumber, int Quantity, int WOID, decimal Amount, bool IsAdhocBilling, bool IsArchived, string VenderRefID, string Description, decimal UnitPrice, string DateInoccured, int CreatedBy, bool NeedVerification, string InHouseComment)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[15];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@WOID", WOID);
                //sqlParams[2] = new SqlParameter("@ClientID", ClientID);
                sqlParams[2] = new SqlParameter("@Type", ItemNumber);
                sqlParams[3] = new SqlParameter("@Units", Quantity);
                // sqlParams[4] = new SqlParameter("@ItemCode", Code);
                sqlParams[4] = new SqlParameter("@Amount", Amount);
                sqlParams[5] = new SqlParameter("@Description", Description);
                sqlParams[6] = new SqlParameter("@VenderRefId", VenderRefID);
                sqlParams[7] = new SqlParameter("@IsAdhoc", IsAdhocBilling);
                sqlParams[8] = new SqlParameter("@IsArchived", IsArchived);
                sqlParams[9] = new SqlParameter("@UnitPrice", UnitPrice);

                if (DateInoccured == string.Empty)
                    sqlParams[10] = new SqlParameter("@DateInoccured", DBNull.Value);
                else
                    sqlParams[10] = new SqlParameter("@DateInoccured", HelperClasses.ConvertDateFormat(DateInoccured));
                sqlParams[11] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[12] = new SqlParameter("@NeedVerification", NeedVerification);
                sqlParams[13] = new SqlParameter("@InHouseComment", InHouseComment);
                sqlParams[14] = new SqlParameter("@RETUNVALUE", 0);
                sqlParams[14].Direction = ParameterDirection.Output;

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertAndUpdateDIItems", sqlParams);

                return Convert.ToInt16(sqlParams[14].Value);
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
        /// Description  : Save disbursement for Client items to database.
        /// Created By   : Pavan
        /// Created Date : 12 September 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int InsertDIForClient(int ItemNumber, int Quantity, decimal Amount, bool IsAdhocBilling, bool IsArchived, string VenderRefID, string Description, decimal UnitPrice, string DateIncurred, bool NeedVerification, int CompanyID, string CompanySource, int CreatedBy, string InHouseComment)
        {
            int output = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[15];
                sqlParams[0] = new SqlParameter("@Type", ItemNumber);
                sqlParams[1] = new SqlParameter("@Units", Quantity);
                sqlParams[2] = new SqlParameter("@Amount", Amount);
                sqlParams[3] = new SqlParameter("@Description", Description);
                sqlParams[4] = new SqlParameter("@VenderRefId", VenderRefID);
                sqlParams[5] = new SqlParameter("@IsAdhoc", IsAdhocBilling);
                sqlParams[6] = new SqlParameter("@IsArchived", IsArchived);
                sqlParams[7] = new SqlParameter("@UnitPrice", UnitPrice);
                sqlParams[8] = new SqlParameter("@NeedVerification", NeedVerification);
                if (DateIncurred == string.Empty)
                    sqlParams[9] = new SqlParameter("@DateIncurred", DBNull.Value);
                else
                    sqlParams[9] = new SqlParameter("@DateIncurred", HelperClasses.ConvertDateFormat(DateIncurred));
                sqlParams[10] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[11] = new SqlParameter("@CompanySource", CompanySource);
                sqlParams[12] = new SqlParameter("@CreatedBy", CreatedBy);
                sqlParams[13] = new SqlParameter("@InHouseComment", InHouseComment);
                sqlParams[14] = new SqlParameter("@RETUNVALUE", 0);
                sqlParams[14].Direction = ParameterDirection.Output;

                output = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertDIForClient", sqlParams);
                return Convert.ToInt16(sqlParams[14].Value);
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
        /// Created By    : Shiva
        /// Created Date  : 19 May 2014
        /// Modified By   :  
        /// Modified Date :  
        /// <returns> Get the current WorkOrderID Disbursement Items data </returns>
        /// </summary>
        public static WorkOrdersAndDItemsInfo GetDisbursementItemsData(int WOID, int startpage, int rowsperpage)
        {
            var disbursementItemsData = new WorkOrdersAndDItemsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@WorkOrderID", WOID);
                sqlParams[1] = new SqlParameter("@startpage", startpage);
                sqlParams[2] = new SqlParameter("@rowsPerPage", rowsperpage);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[GetDisbursementItemsByWOID]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Item = new DisbursementItem();
                    Item.FetchDisbursementItemsForSearch(Item, safe);
                    disbursementItemsData.OrdersList.Add(Item);
                    disbursementItemsData.OrdersCount = Convert.ToInt32(reader["DICount"]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var Instate = new DIInstate();
                    Instate.FetchDIInstate(Instate, safe);
                    disbursementItemsData.DiInstate.Add(Instate);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var Rules = new DIActionRules();
                    Rules.FetchDIActionRules(Rules, safe);
                    disbursementItemsData.DiActionRules.Add(Rules);
                }
                return disbursementItemsData;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return disbursementItemsData;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        #endregion

        #region Partial DisbursementItems List Actions

        /// <summary>
        /// Description  : Get Disbursement details by ID  
        /// Created By   : Shiva
        /// Created Date : 6 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static WorkOrdersAndDItemsInfo GetDisbursementItemsByID(int ID, string VenderRefID)
        {
            var data = new WorkOrdersAndDItemsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@ID", ID);
                sqlParams[1] = new SqlParameter("@VenderRefID", VenderRefID);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPGetDisbursementDataByID", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var SearchDisbursementItem = new DisbursementItem();
                    SearchDisbursementItem.FetchDisbursementItemsByID(SearchDisbursementItem, safe);
                    data.OrdersList.Add(SearchDisbursementItem);
                }
                safe.NextResult();
                while (reader.Read())
                {
                    data.VendorAmount = Convert.ToDecimal(reader["Amount"]);
                    data.VendorReport = Convert.ToString(reader["VRID"]);
                }
                safe.NextResult();
                while (reader.Read())
                {
                    data.OrdersCount = Convert.ToInt32(reader["RelatedDICount"]);
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
        /// Description  : Update IsArchived To Active or In-Active For Checked Id's Of Disburesement Items 
        /// Created By   : Pavan
        /// Created Date : 26 May 2014
        /// Modified By  : SHIVA
        /// Modified Date: 24 June 2014
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        public int ArchivedActionOnDisbursementItems(string DisbursementIds, string InHouseComment, int Archived, string ForState, int SavedBy)
        {
            int returnStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@IDs", DisbursementIds);
                sqlParams[1] = new SqlParameter("@IsArchive", Archived);
                sqlParams[2] = new SqlParameter("@ForState", ForState);
                sqlParams[3] = new SqlParameter("@InHouseComment", InHouseComment);
                sqlParams[4] = new SqlParameter("@SavedBy", SavedBy);
                sqlParams[5] = new SqlParameter("@ActionStatus", 0);
                sqlParams[5].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPArchivedActionRulesOnDisbursementItems", sqlParams);
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
        /// Description  : Update IsArchived To Active or In-Active For Checked Id's Of Disburesement Items 
        /// Created By   : SHIVA
        /// Created Date : 26 May 2014
        /// Modified By  : SHIVA
        /// Modified Date: 24 June 2014
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        public int AdhocActionOnDisbursementItems(string DisbursementIds, int Adhoc, string ForState, int SavedBy)
        {
            int returnStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@IDs", DisbursementIds);
                sqlParams[1] = new SqlParameter("@IsAdhoc", Adhoc);
                sqlParams[2] = new SqlParameter("@ForState", ForState);
                sqlParams[3] = new SqlParameter("@SavedBy", SavedBy);
                sqlParams[4] = new SqlParameter("@ActionStatus", 0);
                sqlParams[4].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SPAdhocActionRulesOnDisbursementItems", sqlParams);
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
        /// Description  : Update ACCPAC Status for Disburesement Items 
        /// Created By   : Pavan
        /// Created Date : 13 March 2015
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="DisbursementIds"></param>
        /// <returns></returns>
        public static int UpdateAccpacStatusByDIID(int DIID, int SavedBy)
        {
            int returnStatus = -2;
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@DIID", DIID);
                sqlParams[1] = new SqlParameter("@SavedBy", SavedBy);
                returnStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdateDisbursementItemACCPACStatus", sqlParams);
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
        /// Description  : DeleteDisbursement by ID  
        /// Created By   : hussain
        /// Created Date : 11 June 2014
        /// Modified By  : Shiva
        /// Modified Date: 16 June 2014
        /// </summary>
        /// <param name="ID">Delete Disbursement details based on DID</param>
        /// <returns>Returns deleted status.</returns>
        public static int DeleteDisbursementItemsByID(string DIDs)
        {
            int returnStatus = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", DIDs);

                returnStatus = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteDisbursementItems", sqlParams);
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
        /// Description  : Get DisburesementItems as per search Results. 
        /// Created By   : pavan
        /// Created Date : 19 May 2014
        /// Modified By  : Shiva
        /// Modified Date: 11 Aug 2014
        /// </summary>
        /// <returns></returns>
        public static WorkOrdersAndDItemsInfo GetAllSearchDisbursementItems(string clientID, string SourceID, string WO, string venderRefId, string Type, string IsVerified, string IsBilled, string IsArchived, string IsAdhoc, string OrderBy, int startpage, int rowsperpage, string FromDate, string ToDate,int ACCPACStatus)
        {
            var data = new WorkOrdersAndDItemsInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[15];
                //sqlParams[0] = new SqlParameter("@ItemNumber", clientName);
                sqlParams[0] = new SqlParameter("@WO", WO);
                sqlParams[1] = new SqlParameter("@VenderRefId", venderRefId);
                sqlParams[2] = new SqlParameter("@Type", Type);
                sqlParams[3] = new SqlParameter("@IsVerified", IsVerified);
                sqlParams[4] = new SqlParameter("@IsBilled", IsBilled);
                sqlParams[5] = new SqlParameter("@IsArchived", IsArchived);
                sqlParams[6] = new SqlParameter("@IsAdhoc", IsAdhoc);
                sqlParams[7] = new SqlParameter("@OrderBy", OrderBy);
                sqlParams[8] = new SqlParameter("@startPage", startpage);
                sqlParams[9] = new SqlParameter("@resultPerPage", rowsperpage);
                sqlParams[10] = new SqlParameter("@ClientID", clientID);
                sqlParams[11] = new SqlParameter("@SourceID", SourceID);
                sqlParams[12] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[13] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));
                sqlParams[14] = new SqlParameter("@ACCPACStatus", ACCPACStatus);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAllDisbursementItems", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var SearchDisbursementItem = new DisbursementItem();
                    SearchDisbursementItem.FetchDisbursementItemsForSearch(SearchDisbursementItem, safe);
                    data.OrdersList.Add(SearchDisbursementItem);
                    data.OrdersCount = Convert.ToInt32(reader["DICount"]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var Instate = new DIInstate();
                    Instate.FetchDIInstate(Instate, safe);
                    data.DiInstate.Add(Instate);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    var ActionRules = new DIActionRules();
                    ActionRules.FetchDIActionRules(ActionRules, safe);
                    data.DiActionRules.Add(ActionRules);
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
        /// Description  : Get DisbursementItems Invoice preview by ID  
        /// Created By   : SHIVA
        /// Created Date : 30 Oct 2014
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="ID">Get DisbursementItems Invoice preview details based on DID</param>
        /// <returns></returns>
        public static WorkOrdersAndDItemsInfo GetInvoicePreviewDataByDI(string DIDs)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var WorkOrdersAndDItemsInfo = new WorkOrdersAndDItemsInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@DIS", DIDs);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPInvoicePreviewDataByDI]", sqlParams);
                var safe = new SafeDataReader(reader);
                //while (reader.Read())
                //{
                //    var InvoicePreview = new DIInvoicePreview();
                //    InvoicePreview.FetchInvoicePreviewAddess(InvoicePreview, safe);
                //    WorkOrdersAndDItemsInfo.DiInvoicePreviewAddresses.Add(InvoicePreview);
                //}
                //reader.NextResult();
                while (reader.Read())
                {
                    var InvoicePreview = new DIFeeInvoicePreview();
                    InvoicePreview.FetchInvoicePreview(InvoicePreview, safe);
                    WorkOrdersAndDItemsInfo.DiInvoicePreview.Add(InvoicePreview);
                }
                return WorkOrdersAndDItemsInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WorkOrdersAndDItemsInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }


        /// <summary>
        /// Description  : Get FeeItems Invoice preview by ID  
        /// Created By   : SHIVA
        /// Created Date : 4 Feb 2015
        /// Modified By  : 
        /// Modified Date: 
        /// </summary>
        /// <param name="ID">Get FeeItems Invoice preview details based on FeeID's</param>
        /// <returns></returns>
        public static WorkOrdersAndDItemsInfo GetInvoicePreviewDataByFeeID(string FeeIDs)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            var WorkOrdersAndDItemsInfo = new WorkOrdersAndDItemsInfo();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@FeeIDs", FeeIDs);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SPInvoicePreviewDataByFee]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var InvoicePreview = new DIFeeInvoicePreview();
                    InvoicePreview.FetchInvoicePreview(InvoicePreview, safe);
                    WorkOrdersAndDItemsInfo.DiInvoicePreview.Add(InvoicePreview);
                }
                return WorkOrdersAndDItemsInfo;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return WorkOrdersAndDItemsInfo;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }
        #endregion

        #endregion

        #region Events
        /// <summary>
        /// Description  : To do all events in same view
        /// Created By   : Shiva
        /// Created Date : 19 May 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public class WorkOrdersAndDItemsInfo
        {
            public List<DisbursementItem> OrdersList { get; set; }
            public List<DIInstate> DiInstate { get; set; }
            public List<DIActionRules> DiActionRules { get; set; }
            public List<DIFeeInvoicePreview> DiInvoicePreview { set; get; }
            public List<DIFeeInvoicePreview> DiInvoicePreviewAddresses { set; get; }

            public int OrdersCount { get; set; }
            public decimal VendorAmount { get; set; }
            public string VendorReport { get; set; }
            public WorkOrdersAndDItemsInfo()
            {
                OrdersList = new List<DisbursementItem>();
                DiInstate = new List<DIInstate>();
                DiActionRules = new List<DIActionRules>();
                DiInvoicePreview = new List<DIFeeInvoicePreview>();
                DiInvoicePreviewAddresses = new List<DIFeeInvoicePreview>();
            }
        }

        public class DIInstate
        {
            public int DIID { get; set; }
            public string InState { get; set; }

            public DIInstate FetchDIInstate(DIInstate Di, SafeDataReader dr)
            {
                Di.DIID = dr.GetInt32("DIID");
                Di.InState = dr.GetString("InState");
                return Di;
            }
        }

        public class DIActionRules
        {
            public int DIID { get; set; }
            public string ForState { get; set; }
            public int DO { get; set; }
            public int UNDO { get; set; }


            public DIActionRules FetchDIActionRules(DIActionRules Di, SafeDataReader dr)
            {
                Di.DIID = dr.GetInt32("DIID");
                Di.ForState = dr.GetString("ForState");
                Di.DO = dr.GetInt32("do");
                Di.UNDO = dr.GetInt32("undo");
                return Di;
            }

        }
        #endregion
    }
}