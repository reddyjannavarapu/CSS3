# region Document Header
//Created By       : Pavan Kumar 
//Created Date     : 30 June 2014
//Description      : To Manage Fee
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

    [MetadataType(typeof(FeeMetadata))]
    public partial class Fee
    {
        private static ILog log = LogManager.GetLogger(typeof(Fee));

        #region Properties

        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ItemNumber { get; set; }
        public string ACCPACCode { get; set; }
        public string Description { get; set; }
        public string DescriptionWithBreak { get; set; }
        public bool NeedSecurityDeposit { set; get; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public string FeeType { get; set; }
        public int IsUsed { get; set; }

        #endregion

        #region Fetching Data
        private Fee FetchFee(Fee Fee, SafeDataReader dr)
        {
            Fee.ID = dr.GetInt32("ID");
            Fee.Code = dr.GetString("Code");
            Fee.Name = dr.GetString("Name");
            Fee.ItemNumber = dr.GetString("ItemNumber");
            Fee.ACCPACCode = dr.GetString("ACCPACCode");
            Fee.Description = dr.GetString("Description");
            Fee.FeeType = dr.GetString("FeeType");
            string data = dr.GetString("Description");
            if (data.Contains("~^"))
            {
                data = data.Replace("~^", Environment.NewLine);
            }
            Fee.DescriptionWithBreak = data;

            Fee.Status = dr.GetBoolean("Status");
            Fee.NeedSecurityDeposit = dr.GetBoolean("NeedSecurityDeposit");
            Fee.IsUsed = dr.GetInt32("isUsed");
            return Fee;
        }

        private Fee FetchMFee(Fee Fee, SafeDataReader dr)
        {
            Fee.Code = dr.GetString("Code");
            Fee.Name = dr.GetString("Name");
            return Fee;
        }

        #endregion

        #region DataBase Methods

        /// <summary>
        /// Description  : To Show all the Fee Details in View
        /// Created By   : Pavan Kumar
        /// Created Date : 30 June 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns>it will give all the Fee details available in database</returns>
        public static FeeInfo GetAllFees(int startPage, int resultPerPage, string code, string OrderBy, int status)
        {
            var data = new FeeInfo();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@startPage", startPage);
                sqlParams[1] = new SqlParameter("@resultPerPage", resultPerPage);
                sqlParams[2] = new SqlParameter("@code", code);
                sqlParams[3] = new SqlParameter("@Status", status);
                sqlParams[4] = new SqlParameter("@OrderBy", OrderBy);

                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetAllMFee", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Fees = new Fee();
                    Fees.FetchFee(Fees, safe);
                    data.FeeList.Add(Fees);
                    data.FeeCount = Convert.ToInt32(reader["feeCount"]);
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
        /// Description  : To Insert Fee
        /// Created By   : Pavan Kumar
        /// Created Date : 4 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public int InsertFee()
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[10];
                sqlParams[0] = new SqlParameter("@ID", this.ID);
                sqlParams[1] = new SqlParameter("@Code", this.Code);
                sqlParams[2] = new SqlParameter("@Name", this.Name);
                sqlParams[3] = new SqlParameter("@ItemNumber", this.ItemNumber);
                sqlParams[4] = new SqlParameter("@ACCPACCode", this.ACCPACCode);
                sqlParams[5] = new SqlParameter("@Description", this.Description);
                sqlParams[6] = new SqlParameter("@Status", this.Status);
                sqlParams[7] = new SqlParameter("@NeedSecurityDeposit", this.NeedSecurityDeposit);
                sqlParams[8] = new SqlParameter("@CreatedBy", this.CreatedBy);
                sqlParams[9] = new SqlParameter("@FeeType", this.FeeType);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpInsertMFee", sqlParams);
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
        /// Description  : To Delete Fee
        /// Created By   : Pavan Kumar
        /// Created Date : 4 July 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static int DeleteFeeById(int Id)
        {
            int result = -2;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@ID", Id);
                result = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpDeleteMFeeById", sqlParams);
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
        /// Description  : To Get FeeDetails by FeeType
        /// Created By   : Pavan Kumar
        /// Created Date : 03 July 2015
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        /// <returns></returns>
        public static List<Fee> GetFeeDetailsByFeeType(string FeeType)
        {
            List<Fee> data = new List<Fee>();

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@FeeType", FeeType);
                var reader = SqlHelper.ExecuteReader(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpGetMFeesByFeeType", sqlParams);
                var safe = new SafeDataReader(reader);
                while (reader.Read())
                {
                    var Fees = new Fee();
                    Fees.FetchMFee(Fees, safe);
                    data.Add(Fees);
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

        #endregion

    }

    #region InfoClass
    /// <summary>
    /// Description  : To do all events in same view
    /// Created By   : Pavan Kumar
    /// Created Date : 30 June 2014
    /// Modified By  :
    /// Modified Date:
    /// </summary>
    public class FeeInfo
    {
        public List<Fee> FeeList { get; set; }
        public int FeeCount { get; set; }

        public FeeInfo()
        {
            FeeList = new List<Fee>();
        }
    }
    #endregion

}