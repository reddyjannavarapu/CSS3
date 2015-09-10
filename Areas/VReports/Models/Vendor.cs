
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

namespace CSS2.Areas.VReports.Models
{
    #region Usings
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    #endregion

    [MetadataType(typeof(VendorMetadata))]
    public class Vendor
    {
        private static ILog log = LogManager.GetLogger(typeof(Vendor));

        #region Properties
        [Display(Name = "Report #")]
        public int VRID { get; set; }
        [Display(Name = "Vender Ref")]
        public string VenderReferenceID { get; set; }

        [Display(Name = "Records")]
        public int RecordCount { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "UploadedOn")]
        public string UploadedOn { get; set; }

        [Display(Name = "UploadedBy")]
        public int UploadedBy { get; set; }

        [Display(Name = "VendorAmount")]
        public decimal VendorAmount { set; get; }

        [Display(Name = "DI")]
        public string DI { set; get; }

        [Display(Name = "DIAmount")]
        public decimal DIAmount { set; get; }

        [Display(Name = "DIRef")]
        public string DIRef { set; get; }

        [Display(Name = "IsVerified")]
        public bool IsVerified { set; get; }

        [Display(Name = "VRDID")]
        public int VRDID { get; set; }

        [Display(Name = "Billed")]
        public bool IsBilled { set; get; }

        [Display(Name = "Status")]
        public string Status { set; get; }

        [Display(Name = "IsDiscVisible")]
        public bool IsDiscVisible { set; get; }

        [Display(Name = "RowNo")]
        public int RowNo { set; get; }

        [Display(Name = "MVenderId")]
        public int MVenderId { set; get; }

        [Display(Name = "ItemCode")]
        public string ItemCode { set; get; }

        [Display(Name = "ItemNumber")]
        public string ItemNumber { set; get; }

        [Display(Name = "Discrepancy")]
        public decimal Discrepancy { set; get; }

        [Display(Name = "VerifyAmount")]
        public decimal VerifyAmount { set; get; }

        public DataTable dtvendor { set; get; }

        public DataTable dtErrorRec { set; get; }

        public string Username { set; get; }

        public int WOID { set; get; }

        public int ID { set; get; }

        public int IsAdhoc { set; get; }

        public string Name { set; get; }

        public string Code { set; get; }

        public string WOCode { get; set; }

        public string InvoiceNumber { get; set; }

        public string ClientName { get; set; }

        public string Date { get; set; }

        public string UsedBy { get; set; }

        public string Description { get; set; }

        public string GroupName { get; set; }
        
        #endregion

        #region Fetching Data
        private Vendor FetchService(Vendor vendor, SafeDataReader dr)
        {
            vendor.Type = dr.GetString("Type");
            vendor.UploadedOn = dr.GetString("UploadedOn");
            vendor.Username = dr.GetString("Name");
            vendor.VRID = dr.GetInt32("VRID");
            vendor.Name =  dr.GetString("FileName");
            return vendor;
        }
        private Vendor FetchUnmatchedVendorDetails(Vendor vendor, SafeDataReader dr)
        {
            vendor.VenderReferenceID = dr.GetString("VENDERREFID");
            vendor.VendorAmount = dr.GetDecimal("VENDORAMOUNT");
            vendor.DI = dr.GetString("DI");
            vendor.DIAmount = dr.GetDecimal("DIAMOUNT");
            vendor.IsBilled = dr.GetBoolean("BILLED");
            vendor.Type = dr.GetString("TYPE");
            vendor.VerifyAmount = dr.GetDecimal("VERIFYAMOUNT");
            vendor.WOID = dr.GetInt32("WOID");
            vendor.ID = dr.GetInt32("ID");
            vendor.IsAdhoc = dr.GetInt32("IsAdhoc");         
            return vendor;
        }

        private Vendor FetchMatchedVendorDetails(Vendor vendor, SafeDataReader dr)
        {
            vendor.VenderReferenceID = dr.GetString("VENDORREF");
            vendor.VendorAmount = dr.GetDecimal("VENDORAMOUNT");
            vendor.DI = dr.GetString("DI");
            vendor.DIRef = dr.GetString("DIREF");
            vendor.DIAmount = dr.GetDecimal("DIAMOUNT");
            vendor.IsBilled = dr.GetBoolean("BILLED");
            vendor.Type = dr.GetString("TYPE");
            vendor.IsVerified = dr.GetBoolean("ISVERIFIED");
            vendor.VRID = dr.GetInt32("VRID");
            vendor.VRDID = dr.GetInt32("VRDID");
            vendor.ItemCode = dr.GetString("ItemCode");
            vendor.ItemNumber = dr.GetString("ItemNumber");
            vendor.WOCode = dr.GetString("WOCode");
            vendor.InvoiceNumber = dr.GetString("InvoiceNumber");
            vendor.ClientName = dr.GetString("ClientName");
            vendor.Date = dr.GetString("DATE");
            vendor.UsedBy = dr.GetString("SIC");
            vendor.GroupName = dr.GetString("GroupName");
            return vendor;
        }

        private Vendor FetchUnMatchedVendorDetails(Vendor vendor, SafeDataReader dr)
        {
            vendor.Date = dr.GetString("TransactionDate");
            vendor.UsedBy = dr.GetString("UsedBy");
            vendor.Description = dr.GetString("Description");
            vendor.VenderReferenceID = dr.GetString("VENDORREF");
            vendor.VendorAmount = dr.GetDecimal("VENDORAMOUNT");
            vendor.VRID = dr.GetInt32("VRID");
            vendor.VRDID = dr.GetInt32("VRDID");           
            return vendor;
        }
        private Vendor FetchDiscrepancyVendorDetails(Vendor vendor, SafeDataReader dr, bool IsUnbilled)
        {
            vendor.VenderReferenceID = dr.GetString("VENDORREF");
            vendor.VendorAmount = dr.GetDecimal("VENDORAMOUNT");
            vendor.DI = dr.GetString("DI");
            vendor.DIRef = dr.GetString("DIREF");
            vendor.DIAmount = dr.GetDecimal("DIAMOUNT");
            vendor.IsBilled = dr.GetBoolean("BILLED");
            vendor.Type = dr.GetString("TYPE");
            vendor.Discrepancy = (IsUnbilled ? dr.GetDecimal("UnBilledDiscrepancy") : dr.GetDecimal("Discrepancy"));
            vendor.VRID = dr.GetInt32("VRID");
            vendor.VRDID = dr.GetInt32("VRDID");
            vendor.ItemCode = dr.GetString("ItemCode");
            vendor.ItemNumber = dr.GetString("ItemNumber");
            vendor.WOCode = dr.GetString("WOCode");
            vendor.InvoiceNumber = dr.GetString("InvoiceNumber");
            vendor.ClientName = dr.GetString("ClientName");
            vendor.Date = dr.GetString("DATE");
            vendor.UsedBy = dr.GetString("SIC");
            vendor.GroupName = dr.GetString("GroupName");
            return vendor;
        }

        private Vendor FetchMultiDiscrepancyVendorDetails(Vendor vendor, SafeDataReader dr, bool IsUnbilled)
        {
            vendor.VenderReferenceID = dr.GetString("VENDORREF");
            vendor.VendorAmount = dr.GetDecimal("VENDORAMOUNT");
            vendor.DI = dr.GetString("DI");
            vendor.DIRef = dr.GetString("DIREF");
            vendor.DIAmount = dr.GetDecimal("DIAMOUNT");
            vendor.IsBilled = dr.GetBoolean("BILLED");
            vendor.Type = dr.GetString("TYPE");
            vendor.Discrepancy = (IsUnbilled ? dr.GetDecimal("UnBilledDiscrepancy") : dr.GetDecimal("Discrepancy"));
            vendor.VRID = dr.GetInt32("VRID");
            vendor.VRDID = dr.GetInt32("VRDID");
            vendor.ItemCode = dr.GetString("ItemCode");
            vendor.ItemNumber = dr.GetString("ItemNumber");
            vendor.Status = dr.GetString("STATUS");
            vendor.RowNo = dr.GetInt32("RowNo");
            vendor.IsDiscVisible = false;
            vendor.WOCode = dr.GetString("WOCode");
            vendor.InvoiceNumber = dr.GetString("InvoiceNumber");
            vendor.ClientName = dr.GetString("ClientName");
            vendor.Date = dr.GetString("DATE");
            vendor.UsedBy = dr.GetString("SIC");
            vendor.GroupName = dr.GetString("GroupName");
            return vendor;
        }

        private Vendor FetchErrorList(Vendor vendor, SafeDataReader dr)
        {
            vendor.VRID = dr.GetInt32("VRID");
            vendor.UploadedOn = dr.GetString("TransationDate");
            vendor.Code = dr.GetString("Description");
            vendor.VenderReferenceID = dr.GetString("ReferenceNo");
            vendor.ItemNumber = dr.GetString("Amount");
            return vendor;
        }


        private Vendor FetchVendorReportType(Vendor vendor, SafeDataReader dr)
        {
            vendor.Code = dr.GetString("Code");
            vendor.Name = dr.GetString("Name");

            return vendor;
        }
        private Vendor FetchVendorReportType1(Vendor vendor, SafeDataReader dr)
        {
            vendor.Code = dr.GetString("Code");
            vendor.Name = dr.GetString("Name");
            vendor.Username = dr.GetString("Username");
            vendor.UploadedOn = dr.GetString("UploadedOn");
            return vendor;
        }
        #endregion

        public static VendorInfo GetAllVendorDetails(int startPage, int resultPerPage, string OrderBy,string FromDate,string ToDate)
        {
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@OrderBy", OrderBy);
                sqlParams[3] = new SqlParameter("@FromDate", HelperClasses.ConvertDateFormat(FromDate));
                sqlParams[4] = new SqlParameter("@ToDate", HelperClasses.ConvertDateFormat(ToDate));

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetAllVendors]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var vendor = new Vendor();
                    vendor.FetchService(vendor, safe);
                    data.VendorList.Add(vendor);
                    data.VendorCount = Convert.ToInt32(reader["VendorCount"]);
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

        public static VendorInfo GetVendorReportType()
        {
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetVendorReportType]");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var vendor = new Vendor();
                    vendor.FetchVendorReportType(vendor, safe);
                    data.VendorList.Add(vendor);
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
        public static VendorInfo GetVendorUploadedDetails()
        {
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetVendorUploadedDetails]");
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var vendor = new Vendor();
                    vendor.FetchVendorReportType(vendor, safe);
                    data.VendorList.Add(vendor);
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
        /// Description  : To Get Vendor Upload Details By Type
        /// Created By   : Pavan  
        /// Created Date : 6 FEB 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public static VendorInfo SpGetVendorUploadedDetailsByType(string Type)
        {
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Type", Type);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetVendorUploadedDetailsByType]", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var vendor = new Vendor();
                    vendor.FetchVendorReportType1(vendor, safe);
                    data.VendorList.Add(vendor);
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


        public VendorInfo InsertVendorRecords(Vendor ObjVendor)
        {
            //int result = 0;
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@tblVendor", SqlDbType.Structured)
                {
                    Value = ObjVendor.dtvendor
                };

                sqlParams[1] = new SqlParameter("@TBLErrorVENDOR", SqlDbType.Structured)
                {
                    Value = ObjVendor.dtErrorRec
                };

                sqlParams[2] = new SqlParameter("@Type", ObjVendor.Type);
                sqlParams[3] = new SqlParameter("@UploadedBy", ObjVendor.UploadedBy);
                sqlParams[4] = new SqlParameter("@RecordCount", ObjVendor.RecordCount);
                sqlParams[5] = new SqlParameter("@FileName", ObjVendor.Name);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertVendorDetails", sqlParams);
                // reader.NextResult();
                var safe = new SafeDataReader(reader);

                while (reader.Read())
                {
                    data.IsAlreadyUploaded = Convert.ToString(reader["IsAlreadyUplaoded"]);
                }
                reader.NextResult();
                FetchVendorDetails(data, reader, safe);
                return data;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                data.ExceptionMessage = "exception";
                return data;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }
        }

        private void FetchVendorDetails(VendorInfo data, SqlDataReader reader, SafeDataReader safe)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                //Matched DI Records 
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchMatchedVendorDetails(ObjGetVendor, safe);
                    data.MatchedDIList.Add(ObjGetVendor);
                }

                //UnMatched DI Records
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchUnMatchedVendorDetails(ObjGetVendor, safe);
                    data.UnMatchedDIList.Add(ObjGetVendor);
                }

                //Over Billed
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchMatchedVendorDetails(ObjGetVendor, safe);
                    data.OverBilledList.Add(ObjGetVendor);
                }

                //Billed Discrepancy
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchDiscrepancyVendorDetails(ObjGetVendor, safe, false);
                    data.BilledDiscrepancyList.Add(ObjGetVendor);
                }

                //Unbilled Discrepancy
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchDiscrepancyVendorDetails(ObjGetVendor, safe, true);
                    data.UnbilledDiscrepancyList.Add(ObjGetVendor);

                }

                //Multple Discrepancy
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchMultiDiscrepancyVendorDetails(ObjGetVendor, safe, false);
                    data.MultpleDiscrepancy.Add(ObjGetVendor);

                }

                //Method is Use to Calculate Multiple Discripancy
                ProcessMultipleDiscrepancy(data.MultpleUpdatedDiscrepancy, data.MultpleDiscrepancy);


                //Error List
                reader.NextResult();
                while (reader.Read())
                {
                    var ObjGetVendor = new Vendor();
                    ObjGetVendor.FetchErrorList(ObjGetVendor, safe);
                    data.ErrorList.Add(ObjGetVendor);
                }

                //File Name
                reader.NextResult();
                while (reader.Read())
                {
                    data.FileName = Convert.ToString(reader["FileName"]);
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

        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 20 June 2014        
        /// Method is used for Multiple Discrepancy Calculations
        /// </summary>
        /// <param name="MainList"></param>
        /// <param name="MultiBILLEDList"></param>
        private static object ProcessMultipleDiscrepancy(List<Vendor> MainList, List<Vendor> MultipleDiscList)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var vrdids = (from s in MultipleDiscList select s.VRDID).Distinct().ToList();

                var MULTIPLE = (from s in MultipleDiscList where s.Status == "MULTIPLE" select s).ToList();

                var BILLED = (from s in MultipleDiscList where s.Status == "BILLED" select s).ToList();

                var UnBILLED = (from s in MultipleDiscList where s.Status == "UNBILLED" select s).ToList();


                foreach (var vrdid in vrdids)
                {
                    var BILLEDList = (from s in BILLED where s.VRDID == vrdid select s).ToList();
                    if (BILLEDList.Count > 0)
                    {
                        ProcessBilledList(MainList, BILLEDList);
                    }
                }

                foreach (var vrdid in vrdids)
                {
                    var UnBILLEDList = (from s in UnBILLED where s.VRDID == vrdid select s).ToList();
                    if (UnBILLEDList.Count > 0)
                    {
                        ProcessUnBilledList(MainList, UnBILLEDList);
                    }
                }

                foreach (var vrdid in vrdids)
                {
                    var MultiBILLEDList = (from s in MULTIPLE where s.VRDID == vrdid select s).ToList();

                    if (MultiBILLEDList.Count > 0)
                    {
                        ProcessMultiBilledList(MainList, MultiBILLEDList);
                    }
                }

                return MainList;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
                return MainList;
            }
            finally
            {
                log.Debug("End: " + methodBase.Name);
            }

        }

        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 20 June 2014        
        /// Multiple MultiBilled Discrepancy Calculations
        /// </summary>
        /// <param name="MainList"></param>
        /// <param name="MultiBILLEDList"></param>
        private static void ProcessMultiBilledList(List<Vendor> MainList, List<Vendor> MultiBILLEDList)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                var LastUnbilled = (from s in MultiBILLEDList where s.IsBilled == false orderby s.RowNo descending select s.RowNo).ToList().FirstOrDefault();

                decimal DIAmount = 0; decimal VendorAmount = 0;
                int index = 0; int UpdateIndex = 0; int MVID = 0;

                List<Vendor> SubList = new List<Vendor>();

                foreach (var item in MultiBILLEDList)
                {
                    Vendor ObjMVL = new Vendor();
                    VendorAmount = MultiBILLEDList[0].VendorAmount;

                    ObjMVL = item;

                    if (LastUnbilled != item.RowNo)
                    {
                        DIAmount += item.DIAmount;
                        ObjMVL.Discrepancy = item.DIAmount;
                    }
                    else if (LastUnbilled == item.RowNo)
                    {
                        ObjMVL.IsDiscVisible = true;
                        UpdateIndex = index;
                        MVID = Convert.ToInt32(item.DI);
                    }
                    SubList.Add(ObjMVL);
                    index++;
                }

                SubList[UpdateIndex].Discrepancy = VendorAmount - DIAmount;
                SubList[0].MVenderId = MVID;
                foreach (var item in SubList)
                {
                    Vendor objMainList = new Vendor();
                    objMainList = item;
                    MainList.Add(objMainList);
                }

                SubList.Clear();
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

        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 20 June 2014        
        /// Multiple UnBilled Discrepancy Calculations
        /// </summary>
        /// <param name="MainList"></param>
        /// <param name="UnBILLEDList"></param>
        private static void ProcessUnBilledList(List<Vendor> MainList, List<Vendor> UnBILLEDList)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int lastRecord = UnBILLEDList.Count() - 1;
                decimal DIAmount = 0;
                int MVId = 0;

                List<Vendor> SubList = new List<Vendor>();

                for (int i = 0; i < UnBILLEDList.Count(); i++)
                {
                    Vendor ObjMVL = new Vendor();
                    decimal VendorAmount = UnBILLEDList[0].VendorAmount;

                    ObjMVL = UnBILLEDList[i];

                    if (i != lastRecord)
                    {
                        DIAmount += UnBILLEDList[i].DIAmount;
                        ObjMVL.Discrepancy = UnBILLEDList[i].DIAmount;
                    }
                    else if (i == lastRecord)
                    {
                        ObjMVL.Discrepancy = VendorAmount - DIAmount;
                        ObjMVL.IsDiscVisible = true;
                        MVId = Convert.ToInt32(UnBILLEDList[i].DI);
                    }

                    SubList.Add(ObjMVL);
                }

                SubList[0].MVenderId = MVId;

                foreach (var item in SubList)
                {
                    MainList.Add(item);
                }
                SubList.Clear();
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

        /// <summary>
        /// Created By    : SUDHEER
        /// Created Date  : 20 June 2014        
        /// Multiple Billed Discrepancy Calculations
        /// </summary>
        /// <param name="MainList"></param>
        /// <param name="MultiBILLEDList"></param>
        private static void ProcessBilledList(List<Vendor> MainList, List<Vendor> BILLEDList)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                int lastRecord = BILLEDList.Count() - 1;
                decimal DIAmount = 0;
                int MVId = 0;

                List<Vendor> SubList = new List<Vendor>();

                for (int i = 0; i < BILLEDList.Count(); i++)
                {
                    Vendor ObjMVL = new Vendor();
                    decimal VendorAmount = BILLEDList[0].VendorAmount;

                    ObjMVL = BILLEDList[i];

                    DIAmount += BILLEDList[i].DIAmount;

                    if (i != lastRecord)
                        ObjMVL.Discrepancy = BILLEDList[i].DIAmount;
                    else if (i == lastRecord)
                    {
                        ObjMVL.Discrepancy = VendorAmount - DIAmount;
                        ObjMVL.IsDiscVisible = true;
                        ObjMVL.MVenderId = BILLEDList[i].VRDID;
                        MVId = Convert.ToInt32(BILLEDList[i].DI);
                    }

                    SubList.Add(ObjMVL);
                }

                SubList[0].MVenderId = MVId;

                foreach (var item in SubList)
                {
                    MainList.Add(item);
                }
                SubList.Clear();
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

        public VendorInfo UplateBilledUnBilledDiscrepancy(Vendor objVd)
        {
            //int result = 0;
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@tblVendor", SqlDbType.Structured)
                {
                    Value = objVd.dtvendor
                };
                sqlParams[1] = new SqlParameter("@VRID", objVd.VRID);
                sqlParams[2] = new SqlParameter("@CreatedBy", objVd.UploadedBy);
                sqlParams[3] = new SqlParameter("@Action", objVd.Name);
                sqlParams[4] = new SqlParameter("@DiscType", objVd.Type);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdateBilled_UnBilled_Discrepancy", sqlParams);
                // reader.NextResult();
                var safe = new SafeDataReader(reader);
                FetchVendorDetails(data, reader, safe);
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

        public VendorInfo UplateMultipleDiscrepancy(Vendor objVd)
        {
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@tblVendor", SqlDbType.Structured)
                {
                    Value = objVd.dtvendor
                };
                sqlParams[1] = new SqlParameter("@VRID", objVd.VRID);
                sqlParams[2] = new SqlParameter("@CreatedBy", objVd.UploadedBy);
                sqlParams[3] = new SqlParameter("@Action", objVd.Name);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpUpdate_MultiBilled_Discrepancy", sqlParams);
                // reader.NextResult();
                var safe = new SafeDataReader(reader);
                FetchVendorDetails(data, reader, safe);
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
        /// Created By   : pavan
        /// Created Date : 6 June 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : GetVendorReport Details By VRID to Download
        /// </summary>
        /// <returns></returns>
        public static DataSet GetVendorReportDetailsByVRID(int VRID)
        {
            DataSet ds = new DataSet();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@VRID", VRID);

                ds = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "[SpGetVendorReportDetailsByVRID]", sqlParams);

                ////dt.Columns.Add("VRDID");
                ////dt.Columns.Add("VRID");
                //dt.Columns.Add("TransactionDate");
                //dt.Columns.Add("SIC");
                //dt.Columns.Add("Description");
                //dt.Columns.Add("ReferenceNo");
                //dt.Columns.Add("Amount");

                //while (reader.Read())
                //{
                //    DataRow dr = dt.NewRow();
                //    //dr["VRDID"] = reader["VRDID"].ToString();
                //    //dr["VRID"] = reader["VRID"].ToString();
                //    dr["TransactionDate"] = reader["Transaction Date"].ToString();
                //    dr["SIC"] = reader["SIC"].ToString();
                //    dr["Description"] = reader["Description"].ToString();
                //    dr["ReferenceNo"] = reader["Reference No."].ToString();
                //    dr["Amount"] = reader["Amount"].ToString();
                //    dt.Rows.Add(dr);
                //}
                //ds.Tables.Add(dt);
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

        public VendorInfo getVendorUploadedDetails(Vendor ObjVendor)
        {
            //int result = 0;
            var data = new VendorInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@VRID", ObjVendor.VRID);
                sqlParams[1] = new SqlParameter("@GroupName", ObjVendor.GroupName);
                sqlParams[2] = new SqlParameter("@VENDORREF", ObjVendor.VenderReferenceID);
                sqlParams[3] = new SqlParameter("@ClientName", ObjVendor.ClientName);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetVendorUploadDetails", sqlParams);
                // reader.NextResult();
                var safe = new SafeDataReader(reader);

                FetchVendorDetails(data, reader, safe);
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

    #region Events
    /// <summary>
    /// Description  : To do all events in same view
    /// Created By   : Raj
    /// Created Date : 19 May 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class VendorInfo
    {
        public List<Vendor> VendorList { get; set; }
        public int VendorCount { get; set; }
        public List<Vendor> MatchedDIList { get; set; }
        public List<Vendor> UnMatchedDIList { get; set; }
        public List<Vendor> OverBilledList { get; set; }
        public List<Vendor> BilledDiscrepancyList { get; set; }
        public List<Vendor> UnbilledDiscrepancyList { get; set; }
        public List<Vendor> MultpleDiscrepancy { get; set; }
        public List<Vendor> MultpleUpdatedDiscrepancy { get; set; }
        public List<Vendor> ErrorList { get; set; }

        public int SecondVendorCount { get; set; }
        public string IsAlreadyUploaded { get; set; }
        public string ExceptionMessage { get; set; }
        public string FileName { get; set; }


        public VendorInfo()
        {
            VendorList = new List<Vendor>();
            MatchedDIList = new List<Vendor>();
            UnMatchedDIList = new List<Vendor>();
            OverBilledList = new List<Vendor>();
            BilledDiscrepancyList = new List<Vendor>();
            UnbilledDiscrepancyList = new List<Vendor>();
            MultpleDiscrepancy = new List<Vendor>();
            MultpleUpdatedDiscrepancy = new List<Vendor>();
            ErrorList = new List<Vendor>();
        }
    }
    #endregion
}